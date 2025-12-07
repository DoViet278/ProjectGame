using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance;

    public bool isInvisible = false;
    public bool losePlay = false;
    public bool winPlay = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnterToInvisible()
    {
        StartCoroutine(InvisibleRoutine());
    }
    public void SetInvisible(bool value)
    {
        isInvisible = value;
    }

    private IEnumerator InvisibleRoutine()
    {
        SetInvisible(true);
        yield return new WaitForSeconds(3f);
        SetInvisible(false);
    }
}
