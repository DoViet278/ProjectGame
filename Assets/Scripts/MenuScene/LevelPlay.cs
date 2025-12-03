using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelPlay : MonoBehaviour
{
    [SerializeField] private Button btnLevel;
    private int index;

    private void Awake()
    {
        index = gameObject.transform.GetSiblingIndex();
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
        SceneManager.LoadScene(index+3);
    }
}
