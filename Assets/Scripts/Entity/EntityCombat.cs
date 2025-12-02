using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    public Collider2D[] targetCollider;

    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetRadius = 1f;
    [SerializeField] private LayerMask whatIsTarget;

    public void Attack()
    {
        GetDetectCollider();

        foreach(var collider  in targetCollider)
        {
            EntityHealth targetHealth = collider.GetComponent<EntityHealth>();
            if(targetHealth != null)
            {
                targetHealth.TakeDamage(15);
            }
        }
    }

    private void GetDetectCollider()
    {
        targetCollider = Physics2D.OverlapCircleAll(targetCheck.position, targetRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetRadius);  
    }

}
