using UnityEngine;

public class Enemy_DeadState : EnemyState
{
    private CapsuleCollider2D cap;
    public Enemy_DeadState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        cap = enemy.GetComponent<CapsuleCollider2D>();
    }

    public override void Enter()
    {
        base.Enter();
        anim.enabled = true;
        cap.enabled = false; 
    }
}
