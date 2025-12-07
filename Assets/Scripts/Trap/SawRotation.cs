using UnityEngine;

public class SawRotation : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float detectDistance = 3f;
    private void FixedUpdate()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        if(dist > detectDistance - 5 && dist < detectDistance + 5)
        {
            SoundManager.Instance.PlaySound("saw");
        }
        else
        {
            SoundManager.Instance.StopLoop("saw");
        }
    }
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 5f);
    }
}
