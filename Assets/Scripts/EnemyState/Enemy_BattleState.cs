using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    private Transform player;
    private float lastTimeBattle;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        lastTimeBattle = Time.time;
        player ??= enemy.GetReferencePlayer();

        if (ShouldRetreat())
        {
            rb.linearVelocity = new Vector2(enemy.retreatVelocity.x * -DirectionPlayer(), enemy.retreatVelocity.y);
            enemy.HandleFlip(DirectionPlayer());
        }
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerDetection())
        {
            lastTimeBattle = Time.time;
        }

        if (BattleIsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        if (InAtkRange() && enemy.PlayerDetection())
        {
            stateMachine.ChangeState(enemy.atkState);
        }
        else
        {
            enemy.SetVelocity(enemy.battleMoveSpeed * DirectionPlayer(), rb.linearVelocity.y);
        }
    }

    private bool InAtkRange() => DistanceToPlayer() < enemy.atkDistance;

    private bool BattleIsOver() => Time.time > lastTimeBattle + enemy.battleTimeDuration;

    private bool ShouldRetreat() => DistanceToPlayer() < enemy.minRetreatDistance;

    private float DistanceToPlayer()
    {
        if (player == null) return float.MaxValue;

        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirectionPlayer()
    {
        if (player == null) return 0;

        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
