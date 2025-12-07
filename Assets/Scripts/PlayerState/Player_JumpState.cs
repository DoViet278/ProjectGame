using UnityEngine;

public class Player_JumpState : Player_AirState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
        SoundManager.Instance.PlaySound("jump");
        player.SetVelocity(rb.linearVelocity.x,player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);    
        }
    }
}
