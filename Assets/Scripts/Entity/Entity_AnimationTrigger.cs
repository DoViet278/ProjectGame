using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    private Entity entity;
    private EntityCombat entityCombat;
    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entityCombat = GetComponentInParent<EntityCombat>();    
    }

    public void CurrentStateTrigger()
    {
        entity.CallAnimationTrigger();
    }

    public void PerformAtk()
    {
        entityCombat.Attack();
    }
}
