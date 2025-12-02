using UnityEngine;

public class Player_AtkState : Player_GroundedState
{
    public Player_AtkState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled )
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
