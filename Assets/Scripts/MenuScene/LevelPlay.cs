using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPlay : MonoBehaviour
{
    [SerializeField] private Button btnLevel;
    [SerializeField] private GameObject popupSelectItem;
    private int index;

    private void Awake()
    {
        index = gameObject.transform.GetSiblingIndex();

        if(index < DataManager.LevelPlayUnlocked)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        AddListeners();
    }
    private void AddListeners()
    {
        btnLevel.onClick.AddListener(OnClickLevel);
    }

    private void RemoveListeners()
    {
        btnLevel.onClick.RemoveListener(OnClickLevel);
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    public void OnClickLevel()
    {
        DataManager.LevelPlaying = index + 1;
        popupSelectItem.SetActive(true);
    }
}
