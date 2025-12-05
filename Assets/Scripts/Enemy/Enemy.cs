using UnityEngine;
using System.Collections;
using System;
public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState atkState;
    public Enemy_BattleState battleState;
    public Enemy_DeadState deadState;

    [Header("Battle details")]
    public float battleMoveSpeed = 1.6f;
    public float atkDistance = 2;
    public float battleTimeDuration = 5;
    public float minRetreatDistance = 1;
    public Vector2 retreatVelocity;

    [Header("Movement details")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0f, 2f)]
    public float moveAnimSpeed;

    [Header("Player Detection")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform playerCheckPos;
    [SerializeField] private float playerDistance;
    public Transform player { get; private set; } 


    public override void EntityDealth()
    {
        base.EntityDealth();
        stateMachine.ChangeState(deadState);
    }

    private void HandlePlayerDead()
    {
        stateMachine.ChangeState(idleState);
    }

    public void TryEnterBattleState(Transform player)
    {
        if (stateMachine.currentState == battleState) return;
        if (stateMachine.currentState == atkState) return;

        this.player = player;
        stateMachine.ChangeState(battleState);
    }

    public Transform GetReferencePlayer()
    {
        if(player == null)
        {
            player = PlayerDetection().transform;
        }
        return player;
    }

    public RaycastHit2D PlayerDetection()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(playerCheckPos.position, Vector2.right * facingDir, playerDistance, whatIsPlayer | whatIsGround);
        if(hit.collider == null || hit.collider.gameObject.layer != LayerMask.NameToLayer("Player") || GamePlayController.Instance.isInvisible)
        {
            return default;
        }
        return hit; 
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheckPos.position , new Vector3(playerCheckPos.position.x + (facingDir * playerDistance), playerCheckPos.position.y, playerCheckPos.position.z));
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(playerCheckPos.position, new Vector3(playerCheckPos.position.x + (facingDir * atkDistance), playerCheckPos.position.y, playerCheckPos.position.z));
    }

    private void OnEnable()
    {
        Player.onPlayerDead += HandlePlayerDead;
    }

    private void OnDisable()
    {
        Player.onPlayerDead -= HandlePlayerDead;
    }
}
