using UnityEngine;
using YamlDotNet.Serialization;
using System.IO;
using System.Linq;

public class AdvancedSettingsStore : MonoBehaviour
{
    const string AdvancedSettingsInfoPath = "AdvancedSettings.yml";

    AdvancedSettings advancedSettings;
    public AdvancedSettings AdvancedSettings
    {
        get
        {
            if (advancedSettings == null) advancedSettings = GenerateSettings();
            return advancedSettings;
        }
    }

    AdvancedSettings GenerateSettings()
    {
        var info = DeserializeFromFile<AdvancedSettingsInfo>(AdvancedSettingsInfoPath);
        var mappings = info.KeyMappingPaths.Select(DeserializeFromFile<KeyMapping>).ToArray();
        var replacing = string.IsNullOrEmpty(info.KeyReplacingPath) ?
            KeyReplacing.Empty :
            DeserializeFromFile<KeyReplacing>(info.KeyReplacingPath);
        return new AdvancedSettings { KeyMappings = mappings, KeyReplacing = replacing };
    }

    T DeserializeFromFile<T>(string path)
    {
        var deserializer = new Deserializer();
        using (var reader = new StreamReader(path))
            return deserializer.Deserialize<T>(reader);
    }
}