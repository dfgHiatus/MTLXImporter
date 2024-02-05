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

        var root = Engine.Current.WorldManager.FocusedWorld.RootSlot;
        ProgressBarInterface pbi = await root.World.
            AddSlot("Import Indicator").
            SpawnEntity<ProgressBarInterface, LegacySegmentCircleProgress>
                (FavoriteEntity.ProgressBar);
        pbi.Slot.PositionInFrontOfUser();
        pbi.Initialize(canBeHidden: true);
        pbi.UpdateProgress(0.0f, "Got Metallic Material! Starting conversion...", string.Empty);

        int count = 1;
        int num = assetDict.Count;
        foreach (var texture in assetDict)
        {
            var type = texture.Key;
            var childSlot = slot.AddSlot(type.ToString());
            var tex = childSlot.AttachComponent<StaticTexture2D>();
            tex.URL.Value = assetDict[type].key;
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
                case TextureType.Roughness:
                    if (MTLXImporter.Config.GetValue(MTLXImporter.ConvertRoughnessToMetallic))
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

        var root = Engine.Current.WorldManager.FocusedWorld.RootSlot;
        ProgressBarInterface pbi = await root.World.
            AddSlot("Import Indicator").
            SpawnEntity<ProgressBarInterface, LegacySegmentCircleProgress>
                (FavoriteEntity.ProgressBar);
        pbi.Slot.PositionInFrontOfUser();
        pbi.Initialize(canBeHidden: true);
        pbi.UpdateProgress(0.0f, "Got Specular Material! Starting conversion...", string.Empty);

        int count = 1;
        int num = assetDict.Count;
        foreach (var texture in assetDict)
        {
            var type = texture.Key;
            var childSlot = slot.AddSlot(type.ToString());
            var tex = childSlot.AttachComponent<StaticTexture2D>();
            tex.URL.Value = assetDict[type].key;
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
                    if (MTLXImporter.Config.GetValue(MTLXImporter.ConvertRoughnessToMetallic))
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
}
