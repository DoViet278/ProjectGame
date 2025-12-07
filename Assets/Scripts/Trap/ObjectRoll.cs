using UnityEngine;

public class ObjectRoll : MonoBehaviour
{
    public Transform player;
    public float detectDistance = 5f;   
    public float rollSpeed = 3f;     
    public float rotateSpeed = 300f;
    public Collider2D playerCollider;
    private bool isRolling = false;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

    }
    void Update()
    {
        if (!isRolling)
        {
            CheckPlayerBelow();
        }
        else
        {
            Roll();
        }
        if(transform.position.x + detectDistance < player.position.x)
        {
            SoundManager.Instance.StopLoop("stone");
        }
    }

    void CheckPlayerBelow()
    {
        if (player == null) return;

        bool isBelow = player.position.y < transform.position.y - 0.2f;

        float dist = Vector2.Distance(player.position, transform.position);

        if (isBelow && dist <= detectDistance)
        {
            isRolling = true;
            SoundManager.Instance.PlaySound("stone");
        }
    }

    void Roll()
    {
        rb.linearVelocity = new Vector2(-rollSpeed, rb.linearVelocity.y);

        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(col, playerCollider, true);
        }
    }
    
}
