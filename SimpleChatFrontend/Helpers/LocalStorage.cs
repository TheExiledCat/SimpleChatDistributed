using System;
using dotenv.net;

namespace SimpleChatFrontend.Helpers;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia.Controls.Converters;

public static class LocalStorage
{
    public static string StoragePath = DotEnv.Read()["CACHE"];

    public static void Set(string key, object value)
    {
        Dictionary<string, string> storage = ReadKeys();
        if (storage.ContainsKey(key))
        {
            storage[key] = JsonSerializer.Serialize(value);
        }
        else
        {
            storage.Add(key, JsonSerializer.Serialize(value));
        }
        WriteKeys(storage);
    }

    public static Dictionary<string, string> GetAll()
    {
        return ReadKeys();
    }

    public static void Clear()
    {
        File.WriteAllText(StoragePath, "");
    }

    public static void Remove(string key)
    {
        Dictionary<string, string> keys = ReadKeys();
        if (keys.ContainsKey(key))
        {
            keys.Remove(key);
        }
    }

    public static string? Get(string key)
    {
        Dictionary<string, string> keys = ReadKeys();
        return keys.ContainsKey(key) ? keys[key] : null;
    }

    public static T? Get<T>(string key)
    {
        Dictionary<string, string> keys = ReadKeys();
        return keys.ContainsKey(key) ? JsonSerializer.Deserialize<T>(keys[key]) : default(T);
    }

    public static void Initialize()
    {
        if (!File.Exists(Path.Combine(StoragePath, ".localstorage")))
        {
            DirectoryInfo dir = Directory.CreateDirectory(StoragePath);
            dir.Attributes = FileAttributes.Normal;

            FileStream stream = File.Create(Path.Combine(StoragePath, ".localstorage"));
            stream.Close();
        }
    }

    private static Dictionary<string, string> ReadKeys()
    {
        string[] lines = File.ReadAllLines(Path.Combine(StoragePath, ".localstorage"));
        Dictionary<string, string> kvps = [];
        foreach (string line in lines)
        {
            if (!line.Contains("="))
            {
                continue;
            }
            int equalsIndex = line.IndexOf("=");
            string key = line.Substring(0, equalsIndex);
            string value = line.Substring(equalsIndex + 1);
            kvps.Add(key, value);
        }
        return kvps;
    }

    private static void WriteKeys(Dictionary<string, string> kvps)
    {
        File.WriteAllText(
            Path.Combine(StoragePath, ".localstorage"),
            string.Join('\n', kvps.Select(kvp => $"{kvp.Key}={kvp.Value}").ToArray())
        );
    }
}
