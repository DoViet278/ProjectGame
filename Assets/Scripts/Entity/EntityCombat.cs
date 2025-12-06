using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public Collider2D[] targetCollider;

    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetRadius = 1f;
    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsObstales;

    public void Attack()
    {
        GetDetectCollider();

        foreach(var collider  in targetCollider)
        {
            EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
            Player player = collider.GetComponent<Player>();
            if(targetHealth != null && player == null)
            {
                targetHealth.TakeDamage(15);
            }
            else
            {
                targetHealth.TakeDamage(5);
            }
        }
    }

    private void GetDetectCollider()
    {
        targetCollider = Physics2D.OverlapCircleAll(targetCheck.position, targetRadius, whatIsEnemy | whatIsObstales);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetRadius);  
    }

}
