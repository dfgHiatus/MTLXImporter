using Elements.Assets;
using Elements.Core;
using FrooxEngine;
using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MTLXImporter;

public partial class MTLXImporter : ResoniteMod
{
    public override string Name => "MTLXImporter";
    public override string Author => "dfgHiatus";
    public override string Version => "1.0.0";
    public override string Link => "https://github.com/dfgHiatus/MTLXImporter/";

    [AutoRegisterConfigKey]
    public static readonly ModConfigurationKey<bool> Enabled =
        new("enabled", "Enabled", () => true);

    [AutoRegisterConfigKey]
    public static readonly ModConfigurationKey<bool> ConvertRoughnessToMetallic =
        new("convertRoughnessToMetallic", "Convert roughness to metallic (alpha from intensity)", () => true);

    [AutoRegisterConfigKey]
    public static readonly ModConfigurationKey<bool> ConvertSpecular =
        new("convertSpecular", "Convert specular (alpha from intensity)", () => true);

    internal static ModConfiguration Config;
    internal static readonly HashSet<string> MTLX_FILE_EXTENSIONS = new() { ".mtlx" };

    public override void OnEngineInit()
    {
        new Harmony("net.dfgHiatus.MTLXImporter").PatchAll();
        Config = GetConfiguration();
        Engine.Current.RunPostInit(() => AssetPatch());
    }

    private static void AssetPatch()
    {
        var aExt = Traverse.
            Create(typeof(AssetHelper)).
            Field<Dictionary<AssetClass, List<string>>>("associatedExtensions");
        foreach (var ext in MTLX_FILE_EXTENSIONS)
            aExt.Value[AssetClass.Special].Add(ext);
    }

    [HarmonyPatch(typeof(UniversalImporter),
        "ImportTask",
        typeof(AssetClass),
        typeof(IEnumerable<string>),
        typeof(World),
        typeof(float3),
        typeof(floatQ),
        typeof(float3),
        typeof(bool))]
    public class UniversalImporterPatch
    {
        public static bool Prefix(ref IEnumerable<string> files, ref Task __result)
        {
            if (!Config.GetValue(Enabled))
                return true;

            List<string> notHasMTLX = new();
            List<string> hasMTLX = new();
            foreach (var file in files)
            {
                if (MTLX_FILE_EXTENSIONS.Contains(Path.GetExtension(file)))
                    hasMTLX.Add(file);
                else
                    notHasMTLX.Add(file);
            }

            if (hasMTLX.Count > 0)
            {
                __result = ProcessMTLXImport(hasMTLX);
                files = notHasMTLX;
                return true;
            }

            return true;
        }
    }

    private static async Task ProcessMTLXImport(List<string> files)
    {
        // This could be made parallel in theory, but it overwhelms background jobs in an instant
        // So, the best thing we can do process one material at a time
        for (int i = 0; i < files.Count; i++)
        {
            await default(ToBackground);
            var file = files[i];

            var matX = XMLHelper.Deserialize<materialx>(file);
            var name = ((materialxStandard_surface)matX.Items.First()).name;

            await default(ToWorld);
            var slot = Engine.Current.WorldManager.FocusedWorld.AddSlot(name);
            slot.PositionInFrontOfUser();
            slot.GlobalPosition += new float3(i * 0.2f, 0f, 0f);

            await default(ToBackground);
            var assetDict = await GetTextureURLs(file, matX, slot);

            await default(ToWorld);
            if (assetDict.ContainsKey(TextureType.Specular))
                await MaterialHelper.SetupSpecular(slot, assetDict);
            else
                await MaterialHelper.SetupMetallic(slot, assetDict);
        }
    }

    private static async Task<Dictionary<TextureType, DataTransferObject<Uri, float2>>> GetTextureURLs(string file, materialx material, Slot slot)
    {
        await default(ToBackground);
        var filePath = Path.GetDirectoryName(file);
        Dictionary<TextureType, DataTransferObject<Uri, float2>> assetDict = new();

        foreach (var unknownTexture in material.Items)
        {
            var dto = new DataTransferObject<Uri, float2>();
            switch (unknownTexture)
            {
                case materialxNormalmap:
                    var mNM = unknownTexture as materialxNormalmap;
                    var normalMapName = mNM.input.First().nodename;
                    var img1 = await ImageHelper.ImportImage(filePath, normalMapName, slot.World);
                    dto.key = img1;
                    var value1 = (float)mNM.input.First(i => i.name.ToLower() == "scale").value;
                    dto.value = new float2(value1, 0);
                    if (img1 == null || assetDict.ContainsKey(TextureType.Normal))
                        break;
                    else
                        assetDict.Add(TextureType.Normal, dto);
                    break;
                case materialxDisplacement:
                    var mD = unknownTexture as materialxDisplacement;
                    var heightMapName = mD.input.First().nodename;
                    var img2 = await ImageHelper.ImportImage(filePath, heightMapName, slot.World);
                    dto.key = img2;
                    var value2 = (float)mD.input.First(i => i.name.ToLower() == "scale").value;
                    dto.value = new float2(value2, 0);
                    if (img2 == null || assetDict.ContainsKey(TextureType.Height))
                        break;
                    else
                        assetDict.Add(TextureType.Height, dto);
                    break;
                case materialxTiledimage:
                    var mTi = unknownTexture as materialxTiledimage;
                    var tiledTextureName = mTi.input.First().value;
                    var determination = ImageHelper.DetermineTiledImageType(tiledTextureName);
                    var img3 = await ImageHelper.ImportImage(filePath, tiledTextureName, slot.World);
                    dto.key = img3;
                    var value3 = mTi.input.First(i => i.name.ToLower() == "uvtiling").value;
                    dto.value = Utils.ExtractFloatsFromString(value3);
                    if (img3 == null)
                        break;
                    if (assetDict.ContainsKey(determination.key))
                        assetDict[determination.key] = dto;
                    else
                        assetDict.Add(determination.key, dto);
                    break;
                default:
                    break;
            }
        }

        return assetDict;
    }  
}
