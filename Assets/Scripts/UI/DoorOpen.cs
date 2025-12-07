using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    private Animator animator;
    private bool isOpen = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(openDoorEndGame());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private IEnumerator openDoorEndGame()
    {
        animator.SetBool("open",true);
        yield return new WaitForSeconds(0.5f);
        GamePlayController.Instance.winPlay = true;
    }
}
