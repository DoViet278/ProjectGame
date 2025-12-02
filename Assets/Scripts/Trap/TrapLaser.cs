using System.Collections;
using UnityEngine;

public class TrapLaser : MonoBehaviour
{

    public float laserDuration = 2f;

    private SpriteRenderer sprite;
    private Collider2D coll;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        StartCoroutine(LaserLoop());
    }

    private IEnumerator LaserLoop()
    {
        while (true)
        {
            // Bật laser
            sprite.enabled = true;
            coll.enabled = true;

            yield return new WaitForSeconds(laserDuration);

            // Tắt laser
            sprite.enabled = false;
            coll.enabled = false;

            yield return new WaitForSeconds(laserDuration);
        }
    }
}
