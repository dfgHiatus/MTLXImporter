using Elements.Assets;
using Elements.Core;
using FrooxEngine;
using SkyFrost.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTLXImporter;

internal class MaterialHelper
{
    internal static async Task SetupMetallic(Slot slot, Dictionary<TextureType, DataTransferObject<Uri, float2>> assetDict)
    {
        await default(ToWorld);
        var resoniteMat = slot.CreateMaterialOrb<PBS_Metallic>();
        ProgressBarInterface pbi = await SetupProgressBar(slot);

        int count = 1;
        int num = assetDict.Count;
        foreach (var texture in assetDict)
        {
            var type = texture.Key;
            var childSlot = slot.AddSlot(type.ToString());
            var tex = childSlot.AttachComponent<StaticTexture2D>();
            tex.URL.Value = assetDict[type].key;
            await PerformResize(pbi, count, num, type, tex);

            //var scale = assetDict[type].value;

            pbi.UpdateProgress(count / num, $"Applying texture: {type} ({count}/{num})", string.Empty);

            switch (type)
            {
                case TextureType.Albedo:
                    resoniteMat.AlbedoTexture.Target = tex;
                    break;
                case TextureType.Normal:
                    tex.IsNormalMap.Value = true;
                    resoniteMat.NormalMap.Target = tex;
                    //resoniteMat.NormalScale.Value = scale.x;
                    break;
                case TextureType.Height:
                    resoniteMat.HeightMap.Target = tex;
                    //resoniteMat.HeightScale.Value = scale.x;
                    break;
                case TextureType.Emissive:
                    resoniteMat.EmissiveMap.Target = tex;
                    break;
                case TextureType.Gloss:
                    if (!MTLXImporter.Config.GetValue(MTLXImporter.ApplyMetallic)) break;
                    resoniteMat.MetallicMap.Target = tex;
                    break;
                case TextureType.Roughness:
                    if (!MTLXImporter.Config.GetValue(MTLXImporter.ApplyMetallic)) break;
                    else if (resoniteMat.MetallicMap.Target != null) break;
                    else if (MTLXImporter.Config.GetValue(MTLXImporter.ConvertRoughnessToMetallic))
                    {
                        pbi.UpdateProgress(count / num, "Converting roughness to metallic!\nThis might take a while (60s~ for a 1k texture)", string.Empty);
                        await tex.AlphaFromIntensity();
                    }
                    resoniteMat.MetallicMap.Target = tex;
                    break;
                case TextureType.AmbientOcclusion:
                    resoniteMat.OcclusionMap.Target = tex;
                    break;
                case TextureType.Specular:
                case TextureType.UNKNOWN:
                default:
                    break;
            }

            count++;
        }

        pbi.ProgressDone("Material setup complete!");
        pbi.Slot.RunInSeconds(2.5f, pbi.Slot.Destroy);
    }

    internal static async Task SetupSpecular(Slot slot, Dictionary<TextureType, DataTransferObject<Uri, float2>> assetDict)
    {
        await default(ToWorld);
        var resoniteMat = slot.CreateMaterialOrb<PBS_Specular>();
        ProgressBarInterface pbi = await SetupProgressBar(slot);

        int count = 1;
        int num = assetDict.Count;
        foreach (var texture in assetDict)
        {
            var type = texture.Key;
            var childSlot = slot.AddSlot(type.ToString());
            var tex = childSlot.AttachComponent<StaticTexture2D>();
            tex.URL.Value = assetDict[type].key;
            await PerformResize(pbi, count, num, type, tex);
            //var scale = assetDict[type].value;

            pbi.UpdateProgress(count / num, $"Applying texture: {type} ({count}/{num})", string.Empty);

            switch (type)
            {
                case TextureType.Albedo:
                    resoniteMat.AlbedoTexture.Target = tex;
                    break;
                case TextureType.Normal:
                    tex.IsNormalMap.Value = true;
                    resoniteMat.NormalMap.Target = tex;
                    //resoniteMat.NormalScale.Value = scale.x;
                    break;
                case TextureType.Height:
                    resoniteMat.HeightMap.Target = tex;
                    //resoniteMat.HeightScale.Value = scale.x;
                    break;
                case TextureType.Emissive:
                    resoniteMat.EmissiveMap.Target = tex;
                    break;
                case TextureType.Specular:
                    if (!MTLXImporter.Config.GetValue(MTLXImporter.ApplyMetallic)) break;
                    else if (MTLXImporter.Config.GetValue(MTLXImporter.ConvertRoughnessToMetallic))
                    {
                        pbi.UpdateProgress(count / num, "Converting specular!\nThis might take a while (60s~ for a 1k texture)", string.Empty);
                        await tex.AlphaFromIntensity();
                    }
                    resoniteMat.SpecularMap.Target = tex;
                    break;
                case TextureType.AmbientOcclusion:
                    resoniteMat.OcclusionMap.Target = tex;
                    break;
                case TextureType.Roughness:
                case TextureType.UNKNOWN:
                default:
                    break;
            }

            count++;
        }

        pbi.ProgressDone("Material setup complete!");
        pbi.Slot.RunInSeconds(2.5f, pbi.Slot.Destroy);
    }

    private static async Task<ProgressBarInterface> SetupProgressBar(Slot slot)
    {
        var root = Engine.Current.WorldManager.FocusedWorld.RootSlot;
        ProgressBarInterface pbi = await root.World.
            AddSlot("Import Indicator").
            SpawnEntity<ProgressBarInterface, LegacySegmentCircleProgress>
                (FavoriteEntity.ProgressBar);
        pbi.Slot.SetParent(slot, false);
        pbi.Slot.PersistentSelf = false;
        pbi.Slot.LocalPosition += new float3(0, -0.15f, 0);
        pbi.Initialize(canBeHidden: true);
        pbi.UpdateProgress(0.0f, "Got Metallic Material! Starting conversion...", string.Empty);
        return pbi;
    }


    private static async Task PerformResize(ProgressBarInterface pbi, int count, int num, TextureType type, StaticTexture2D tex)
    {
        if (MTLXImporter.Config.GetValue(MTLXImporter.Resize))
        {
            var maxTextureSize = MTLXImporter.Config.GetValue(MTLXImporter.MaxTextureSize);
            pbi.UpdateProgress(count / num, $"Resizing texture: {type} to {maxTextureSize}px.\nThis might take a while (60s~ for a 1k texture)", string.Empty);
            await tex.Rescale(maxTextureSize, Filtering.Bilinear);
        }
    }
}
