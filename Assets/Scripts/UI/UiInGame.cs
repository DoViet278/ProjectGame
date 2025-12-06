using UnityEngine;

public class UiInGame : MonoBehaviour
{
    [SerializeField] private GameObject settingPopup;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    private bool showSetting = false;
    private void Update()
    {
        if (GamePlayController.Instance.losePlay)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            showSetting = !showSetting;
            settingPopup.SetActive(showSetting);
            if(showSetting )
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;    
            }
        }
        if (GamePlayController.Instance.winPlay)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
