using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupPlayerName : MonoBehaviour
{
    private const int PlayerNameMinLength = 3;

    [SerializeField] private Button closeBtn;
    [SerializeField] private Button okBtn;
    [SerializeField] private TMP_InputField nameInput;

    private void OnEnable()
    {
        nameInput.text = "";  

        AddListeners();   
    }

    private void Update()
    {
        EnableInteractableMode();
    }

    private void AddListeners()
    {
        closeBtn.onClick.AddListener(OnClickCloseBtn);
        okBtn.onClick.AddListener(OnOkBtnClicked);
    }

    private void RemoveListeners()
    {
        closeBtn.onClick.RemoveAllListeners();
        okBtn.onClick.RemoveAllListeners(); 
    }
    private void OnClickCloseBtn()
    {
        gameObject.SetActive(false);
    }

    private void OnOkBtnClicked()
    {
        DataManager.PlayerName = nameInput.text; 
        gameObject.SetActive(false );
    }


    private void EnableInteractableMode()
    {
        nameInput.interactable = true;
        CheckInteractableForOkBtn();
    }

    private void CheckInteractableForOkBtn()
    {
        okBtn.interactable = !string.IsNullOrEmpty(nameInput.text) && nameInput.text.Length >= PlayerNameMinLength && nameInput.text != DataManager.PlayerName;
    }

    private void OnDisable()
    {
        RemoveListeners();  
        DataManager.IsFirstPlayTime = false;    
    }
}
