using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject[] lasers; 
    public float laserDuration = 2f;
    public float midDelay = 1f; 

    private void Start()
    {
        StartCoroutine(ControlLasers());
    }

    private IEnumerator ControlLasers()
    {
        while (true)
        {
            SetLaser(0, true);   
            SetLaser(4, true); 

            SetLaser(1, false);  
            SetLaser(2, false);
            SetLaser(3, false);  

            yield return new WaitForSeconds(laserDuration);

            SetLaser(0, false);
            SetLaser(4, false);

            yield return new WaitForSeconds(midDelay);

            SetLaser(1, true);
            SetLaser(2, true);
            SetLaser(3, true);

            yield return new WaitForSeconds(laserDuration);

        }
    }

    private void SetLaser(int index, bool state)
    {
        var sprite = lasers[index].GetComponent<SpriteRenderer>();
        var coll = lasers[index].GetComponent<Collider2D>();

        sprite.enabled = state;
        coll.enabled = state;
    }

}
