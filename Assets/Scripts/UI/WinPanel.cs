using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button btnBackHome;
    private List<ItemData> rewardItem = new List<ItemData>(); 
    private void OnEnable()
    {
        AddListeners();
        rewardItem = new List<ItemData>()
         {
            new ItemData(){ id="cloak", displayName="cloak", iconPath="cloak" },
            new ItemData(){ id="health", displayName="health", iconPath="health" },
        };
    }

    private void AddListeners()
    {
        btnBackHome.onClick.AddListener(onClickBackHome);
    }


    private void onClickBackHome()
    {
        SceneManager.LoadScene("MenuScene");
        InventoryManager.Instance.ApplyHotbarResultToInventory();
        gameObject.SetActive(false);
        GamePlayController.Instance.winPlay = false;
        DataManager.CoinInGame += 500;
        foreach (ItemData item in rewardItem)
        {
            InventoryManager.Instance.AddItem(item);
        }
        Time.timeScale = 1f;
        if (DataManager.LevelPlayUnlocked == DataManager.LevelPlaying)
        {
            DataManager.LevelPlayUnlocked++;
        }
    }

    private void RemoveListeners()
    {
        btnBackHome?.onClick.RemoveListener(onClickBackHome);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
}
