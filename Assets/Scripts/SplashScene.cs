using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScene : MonoBehaviour
{
    private const float timeToLoad = 5.0f;

    [SerializeField] private Image loadingImg;
    [SerializeField] private Transform personTransform;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    private float elapsedTime = 0f;

    private void Awake()
    {
        
    }

    private void Start()
    {
        personTransform = startTransform;
        StartCoroutine(LoadAsynchronously());
    }

    private IEnumerator LoadAsynchronously()
    {
        while (elapsedTime < timeToLoad)
        {
            loadingImg.fillAmount = elapsedTime / timeToLoad;
            personTransform.position = Vector3.Lerp(startTransform.position, endTransform.position, loadingImg.fillAmount);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        GotoScene();
    }

    private void GotoScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
