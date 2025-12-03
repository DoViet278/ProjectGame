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

    void OnDrawGizmos()
    {
        Color c = playerDetected ? alertColor : normalColor;
        Gizmos.color = c;

        Vector3 forward = Vector3.down; 
        float halfAngle = scanAngle * 0.5f;

        int segments = 30;
        float step = scanAngle / segments;

        Vector3 startDir = Quaternion.Euler(0, 0, -halfAngle) * forward;

        Vector3 oldPoint = transform.position + startDir * scanDistance;

        for (int i = 1; i <= segments; i++)
        {
            Vector3 newDir = Quaternion.Euler(0, 0, -halfAngle + step * i) * forward;
            Vector3 newPoint = transform.position + newDir * scanDistance ;

            Gizmos.DrawLine(transform.position, newPoint);
            Gizmos.DrawLine(oldPoint, newPoint);

            oldPoint = newPoint;
        }
    }

    bool IsPlayerInScan()
    {
        if (player == null) return false;

        Vector3 dir = player.position - transform.position;
        float dist = dir.magnitude;
        if (dist > scanDistance) return false;

        Vector3 forward = Vector3.down;
        float angle = Vector3.Angle(forward, dir);

        return angle < scanAngle * 0.5f;
    }
}
