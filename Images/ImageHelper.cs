using Elements.Assets;
using FrooxEngine;
using FrooxEngine.Store;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MTLXImporter;

internal static class ImageHelper
{
    internal static async Task<Uri> ImportImage(string path, string file, World world)
    {
        await default(ToBackground);
        var candidateFile = Path.Combine(path, file);
        if (!File.Exists(candidateFile))
        {
            // Careful - Directory.GetFiles/EnumerateFiles causes issues in async mode. Just skip this :>
            //var texture = Directory.
            //    EnumerateFiles(candidateFile).
            //    First(f => f.ToLower().Contains(file.ToLower()) && f.EndsWith(PNG_EXTENSION)); 
            //if (string.IsNullOrEmpty(texture))
            //    return null;
            //else
            //    candidateFile = Path.Combine(path, texture);

            return null;
        }

        Uri uri = new(candidateFile);
        if (!uri.IsWellFormedOriginalString())
        {
            var localDB = world.Engine.LocalDB;
            uri = await localDB.ImportLocalAssetAsync(candidateFile, LocalDB.ImportLocation.Copy).ConfigureAwait(continueOnCapturedContext: false);
            return uri;
        }

        return null;
    }

    internal static DataTransferObject<TextureType, string> DetermineTiledImageType(string mTi)
    {
        var loweredName = mTi.ToLower();

        var dto = new DataTransferObject<TextureType, string>();
        dto.value = loweredName;

        if (loweredName.Contains("color") || loweredName.Contains("albedo"))
            dto.key = TextureType.Albedo;
        else if (loweredName.Contains("roughness"))
            dto.key = TextureType.Roughness;
        else if (loweredName.Contains("specular"))
            dto.key = TextureType.Specular;
        else if (loweredName.Contains("normal"))
            dto.key = TextureType.Normal;
        else if (loweredName.Contains("height") || loweredName.Contains("displacement"))
            dto.key = TextureType.Height;
        else if (loweredName.Contains("ambientocclusion") || loweredName.Contains("occlusion"))
            dto.key = TextureType.AmbientOcclusion;
        else
            dto.key = TextureType.UNKNOWN;

        return dto;
    }
}
