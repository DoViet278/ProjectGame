using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DroneScaner : MonoBehaviour
{
    private float scanDistance = 20f;
    private float scanAngle = 30f;
    private float scanTime = 2f;
    private Color normalColor = Color.yellow;
    private Color alertColor = Color.red;

    public Transform player;
    public DroneShooter shooter;

    private float scanTimer;
    private bool playerDetected = false;

    void Update()
    {
        scanTimer += Time.deltaTime;
        if (scanTimer >= scanTime)
        {
            scanTimer = 0;
            playerDetected = IsPlayerInScan();
        }

        if (playerDetected)
        {
            shooter.Shoot(player.position);
        }
    }

    bool IsPlayerInScan()
    {
        if (player == null) return false;

        if (GamePlayController.Instance.isInvisible) return false;
        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;
        if (dist > scanDistance) return false;

        Vector3 forward = Vector3.down;
        float angle = Vector3.Angle(forward, dir);

        return angle < scanAngle * 0.5f;
    }
}
