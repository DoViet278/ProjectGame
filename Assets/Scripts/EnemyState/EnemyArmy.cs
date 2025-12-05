using UnityEngine;

public class EnemyArmy : Enemy
{
    //public bool canBeCountered { get => canbeStunned; }
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this,stateMachine,"idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        atkState = new Enemy_AttackState(this, stateMachine, "atk");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        deadState = new Enemy_DeadState(this, stateMachine, "dead");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Init(idleState);
    }
   
    public void HandleCounter()
    {
        //if (canbeStunned == false) return;
        //stateMachine.ChangeState(idleState);
    }
}
