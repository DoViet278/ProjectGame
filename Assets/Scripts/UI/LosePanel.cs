using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button btnTryAgain;
    [SerializeField] private Button btnBackHome;

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        btnTryAgain.onClick.AddListener(onClickTryAgain);
        btnBackHome.onClick.AddListener(onClickBackHome);   
    }

    private void onClickTryAgain()
    {
        SceneManager.LoadScene("MainScene");
        gameObject.SetActive(false);
    }

    private void onClickBackHome()
    {
        SceneManager.LoadScene("MenuScene");
        gameObject.SetActive(false);
    }

    private void RemoveListeners()
    {
        btnBackHome?.onClick.RemoveListener(onClickBackHome);   
        btnTryAgain?.onClick.RemoveListener(onClickBackHome);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }
}
