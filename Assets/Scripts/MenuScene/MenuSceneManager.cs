using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private GameObject selectLevelPopup;
    [SerializeField] private GameObject settings;
    private void OnEnable()
    {
        AddListeners();
    }


    private void AddListeners()
    {
        playBtn.onClick.AddListener(onClickPlay);
        settingBtn.onClick.AddListener(onClickSetting);
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
        settingBtn.onClick.RemoveListener(onClickSetting);
    }

    private void onClickPlay()
    {
        selectLevelPopup.SetActive(true);
    }

    private void onClickSetting()
    {
        settings.SetActive(true);
    }
    private void OnDisable()
    {
        RemoveListeners();
    }





}
