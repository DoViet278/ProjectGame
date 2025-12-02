using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private TextMeshProUGUI txtCoin;

    private void OnEnable()
    {
        AddListeners();
    }

    private void Update()
    {
        txtCoin.text = $"{DataManager.CoinInGame}";
    }

    private void AddListeners()
    {
        playBtn.onClick.AddListener(onClickPlay);
    }

    private void RemoveListeners()
    {
        playBtn.onClick.RemoveListener(onClickPlay);
    }

    private void onClickPlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnDisable()
    {
        RemoveListeners();
    }





}
