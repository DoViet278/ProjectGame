using UnityEngine;

public class Player_AnimationTrigger : MonoBehaviour
{
    private Player player;
    private EntityCombat entityCombat;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
        entityCombat = GetComponentInParent<EntityCombat>();    
    }

    public void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }

    public void PerformAtk()
    {
        entityCombat.Attack();
    }
}
