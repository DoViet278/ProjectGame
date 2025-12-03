using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public bool endGame;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button goHomeBtn;

    private void OnEnable()
    {
        AddListeners();   
    }

    
    private void AddListeners()
    {
        goHomeBtn.onClick.AddListener(GoHome);
    }

    private void RemoveListener()
    {
        goHomeBtn.onClick.RemoveListener(GoHome);  
    }

    private void Update()
    {
        if (endGame)
        {
            losePanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) HotbarManager.Instance.UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) HotbarManager.Instance.UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) HotbarManager.Instance.UseItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) HotbarManager.Instance.UseItem(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) HotbarManager.Instance.UseItem(4);
    }

    private void GoHome()
    {
        SceneManager.LoadScene("MenuScene");
        InventoryManager.Instance.ApplyHotbarResultToInventory();
        InventoryManager.Instance.LoadInventory();
    }

    private void OnDisable()
    {
        RemoveListener();
    }
}
