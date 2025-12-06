using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Button btnBackHome;

    private void OnEnable()
    {
        AddListeners();
    }

    private void AddListeners()
    {
        btnBackHome.onClick.AddListener(onClickBackHome);
    }


    private void onClickBackHome()
    {
        SceneManager.LoadScene("MenuScene");
        gameObject.SetActive(false);
        if(DataManager.LevelPlayUnlocked == DataManager.LevelPlaying)
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
