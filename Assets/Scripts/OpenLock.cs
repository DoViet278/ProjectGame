using UnityEngine;

public class OpenLock : MonoBehaviour
{
    [SerializeField] private GameObject stair;
    [SerializeField] private GameObject goback;
    private Animator _animator;
    private bool isOpen = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetBool("unlock", true);
                stair.SetActive(true);
                goback.SetActive(true);
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
}
