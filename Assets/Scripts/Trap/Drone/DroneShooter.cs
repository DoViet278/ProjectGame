using UnityEngine;

public class DroneShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private float bulletSpeed = 15f;
    private float shootCooldown = 1f;

    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;
    }
    public void Shoot(Vector3 target)
    {
        if (shootTimer < shootCooldown) return;
        shootTimer = 0;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 dir = (target - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;
    }
}
