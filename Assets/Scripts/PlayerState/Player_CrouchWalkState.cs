using UnityEngine;

public class Player_CrouchWalkState : Player_GroundedState
{
    public Player_CrouchWalkState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.moveInput.x * player.crouchWalkSpeed, rb.linearVelocity.y);

        if( player.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.crouchState);   
        }

        if (player.moveInput.y > 0)
        {
            stateMachine.ChangeState(player.idleState);
            player.HandleCollider();
        }
    }
}
