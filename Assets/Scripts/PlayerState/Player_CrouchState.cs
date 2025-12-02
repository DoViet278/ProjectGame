using UnityEngine;

public class Player_CrouchState : Player_GroundedState
{
    public Player_CrouchState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);

    }

    public override void Update()
    {
        base.Update();

        if(player.moveInput.y > 0)
        {
            stateMachine.ChangeState(player.idleState);
            player.HandleCollider();
        }

        if (player.moveInput.x != 0)
        {
            stateMachine.ChangeState(player.crouchWalkState);
        }
    }

}
