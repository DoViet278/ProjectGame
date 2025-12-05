using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button tutorialBtn;
    [SerializeField] private GameObject selectLevelPopup;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject popupPlayerName;
    
    private void OnEnable()
    {
        AddListeners();
        if(DataManager.IsFirstPlayTime) popupPlayerName.SetActive(true);
    }


    private void AddListeners()
    {
        playBtn.onClick.AddListener(onClickPlay);
        settingBtn.onClick.AddListener(onClickSetting);
        tutorialBtn.onClick.AddListener(onClickTutorial);   
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
        settingBtn.onClick.RemoveListener(onClickSetting);
        tutorialBtn.onClick.RemoveListener(onClickTutorial);
    }

    private void onClickPlay()
    {
        selectLevelPopup.SetActive(true);
    }

    private void onClickSetting()
    {
        settings.SetActive(true);
    }

    private void onClickTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    private void OnDisable()
    {
        RemoveListeners();
    }





}
