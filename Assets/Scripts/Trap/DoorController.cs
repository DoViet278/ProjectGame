using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform door;
    public Transform player;   
    public float openDistance = 10f;
    public float detectDistance = 1.5f; 
    public float speed = 2f;

    private bool isOpen = false;

    private Vector3 closedPos;
    private Vector3 openPos;

    void Start()
    {
        closedPos = door.position;
        openPos = closedPos + new Vector3(-openDistance, 0, 0);
    }

    void Update()
    {
        float dist = Vector2.Distance(player.position, door.position);
        bool isPlayerNear = dist <= detectDistance;

        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
        }

        if (isOpen)
        {
            door.position = Vector3.MoveTowards(
                door.position,
                openPos,
                speed * Time.deltaTime
            );
        }
    }
}
