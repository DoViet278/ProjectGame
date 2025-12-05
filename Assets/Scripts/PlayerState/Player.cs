using System;
using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public static event Action onPlayerDead;
    public PlayerInputSet input { get; private set; }
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; } 
    public Player_CrouchState crouchState { get; private set; }
    public Player_CrouchWalkState crouchWalkState { get; private set; }
    public Player_AtkState atkState { get; private set; }
    public Player_DeadState deadState { get; private set; }

    public GameObject losePanel;
    [Header("Movement details")]
    public float moveSpeed;
    public float crouchWalkSpeed;
    public float jumpForce = 5;
    public Vector2 wallJumpForce;
    public Vector2 moveInput { get; private set; }

    [Range(0, 1)]
    public float inAirMoveMultiplier = .7f;

    private CapsuleCollider2D col;
    private Vector2 offsetCollider;
    private Vector2 sizeCollider;
    
    protected override void Awake()
    {
        base.Awake();

        col = gameObject.GetComponent<CapsuleCollider2D>();
        offsetCollider = col.offset;
        sizeCollider = col.size;

        input = new PlayerInputSet();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall");
        fallState = new Player_FallState(this, stateMachine, "jumpFall");
        atkState = new Player_AtkState(this, stateMachine, "atk");
        crouchState = new Player_CrouchState(this, stateMachine, "crouch");
        crouchWalkState = new Player_CrouchWalkState(this, stateMachine, "crouch_walk");
        deadState = new Player_DeadState(this, stateMachine, "dead");
    }

    protected override void Start()
    {
        stateMachine.Init(idleState);
    }

    public override void EntityDealth()
    {
        base.EntityDealth();
        stateMachine.ChangeState(deadState);
        losePanel.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    public void HandleCollider()
    {
        if(stateMachine.currentState == crouchState || stateMachine.currentState == crouchWalkState)
        {
            col.offset = new Vector2(col.offset.x, col.offset.y - 0.3f);
            col.size = new Vector2(col.size.x, col.size.y - col.size.y*1/3);
        }
        else
        {
            col.offset = offsetCollider;
            col.size = sizeCollider;
        }
    }

}