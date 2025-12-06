using UnityEngine;

public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(stateMachine, animBoolName)
    {
        this.enemy = enemy;

        rb = enemy.rb;
        anim = enemy.anim;
    }
    protected override void UpdateAnimationParameter()
    {
        base.UpdateAnimationParameter();
        float battleAnimSpeedMultiplier = enemy.battleMoveSpeed / enemy.moveSpeed;

        anim.SetFloat("battleAnimSpeed", battleAnimSpeedMultiplier);
        anim.SetFloat("animMoveSpeed", enemy.moveAnimSpeed);
        anim.SetFloat("xVelocity", rb.linearVelocity.x);
    }
}
