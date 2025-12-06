using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(openDoorEndGame());
        }
    }

    private IEnumerator openDoorEndGame()
    {
        animator.SetBool("open",true);
        yield return new WaitForSeconds(1);
        GamePlayController.Instance.winPlay = true;
    }
}
