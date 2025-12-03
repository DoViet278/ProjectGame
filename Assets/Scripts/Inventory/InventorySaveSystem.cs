using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class InventorySaveSystem
{
    static string path => Application.persistentDataPath + "/inventory.json";

    public static void Save(List<ItemStack> items)
    {
        InventorySaveData data = new InventorySaveData();
        data.items = items;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public static List<ItemStack> Load()
    {
        if (!File.Exists(path))
            return new List<ItemStack>();

        string json = File.ReadAllText(path);
        InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);

        return data.items;
    }
}
