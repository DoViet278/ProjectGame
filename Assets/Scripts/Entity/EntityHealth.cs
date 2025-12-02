using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    private Slider healthBar;
    private Entity entity;

    [SerializeField] protected float currentHp;
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected bool isDead;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();
        currentHp = maxHp; 
        UpdateHealthBar(); 
      
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        ReduceHp(damage);
    }

    public void ReduceHp(float damage)
    {
        currentHp -= damage;    
        UpdateHealthBar();
        if(currentHp < 0)  Die();
    }

    public void Die()
    {
        isDead = true;
    }

    private void UpdateHealthBar() => healthBar.value = currentHp/maxHp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            TakeDamage(10f);
        }
        else if (collision.CompareTag("Stone"))
        {
            TakeDamage(15f);
        }
        else if(collision.CompareTag("Laser"))
        {
            TakeDamage(10f);
        }
    }
}
