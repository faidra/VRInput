using UnityEngine;
using System;
using System.IO;
using YamlDotNet.Serialization;

public static class KeyMasterIO
{
    const string DefaultFileName = "DefaultKeyMaster.yml";
    const string FileName = "KeyMaster.yml";

    public static KeyMaster Read()
    {
        var deserializer = new Deserializer();
        try
        {
            return Read(deserializer, FileName);
        }
        catch (Exception e)
        {
            Debug.LogError($"{FileName}の読み込みに失敗しました:{e}");
            return Read(deserializer, DefaultFileName);
        }
    }

    static KeyMaster Read(Deserializer deserializer, string name)
    {
        using (var reader = new StreamReader(name))
            return deserializer.Deserialize<KeyMaster>(reader);
    }

    public static void Write(KeyMaster data)
    {
        var serializer = new SerializerBuilder().EmitDefaults().Build();
        using (var writer = new StreamWriter(FileName))
            serializer.Serialize(writer, data);
    }
}