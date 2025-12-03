using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<ItemStack> inventory = new List<ItemStack>();
    public List<ItemStack> preMatchInventoryBackup = new List<ItemStack>();
    public int maxSlots = 10; 

    private string savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/inventory.json";
        LoadInventory();
    }

    public void AddItem(ItemData item, int quantity = 1)
    {
        var stack = inventory.Find(x => x.item.id == item.id);
        if (stack != null)
        {
            stack.quantity += quantity;
        }
        else
        {
            if (inventory.Count < maxSlots)
                inventory.Add(new ItemStack(item, quantity));
            else
                Debug.Log("Inventory Full!");
        }
        SaveInventory(inventory);
    }

    public void RemoveItem(ItemData item, int quantity = 1)
    {
        var stack = inventory.Find(x => x.item.id == item.id);
        if (stack != null)
        {
            stack.quantity -= quantity;
            if (stack.quantity <= 0)
                inventory.Remove(stack);
        }
        SaveInventory(inventory);
    }

    public void SaveInventory(List<ItemStack> items)
    {
        InventorySaveData data = new InventorySaveData();
        data.items = items;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadInventory()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            InventorySaveData data = JsonUtility.FromJson<InventorySaveData>(json);
            inventory = data.items;
            Debug.LogError(inventory.Count);
            foreach(var item in inventory){
                Debug.LogError(item.item.displayName +" "+ item.quantity);
            }
        }
        else
        {
            inventory = new List<ItemStack>();
        }
    }

    public void BackupInventoryBeforeMatch()
    {
        preMatchInventoryBackup = new List<ItemStack>();

        foreach (var stack in inventory)
        {
            preMatchInventoryBackup.Add(new ItemStack(stack.item, stack.quantity));
        }
    }

    public void ApplyHotbarResultToInventory()
    {
        List<ItemStack> result = new List<ItemStack>();

        foreach (var before in preMatchInventoryBackup)
        {
            result.Add(new ItemStack(before.item, before.quantity));
        }

        foreach (var h in HotbarManager.Instance.hotbar)
        {
            if (h == null || h.item == null) continue;

            var exist = result.Find(x => x.item.id == h.item.id);
            if (exist != null)
            {
                exist.quantity += h.quantity; 
            }
            else
            {
                result.Add(new ItemStack(h.item, h.quantity));
            }
        }

        SaveInventory(result);

        HotbarManager.Instance.hotbar.Clear();
    }

}
