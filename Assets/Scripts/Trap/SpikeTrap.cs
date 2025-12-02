using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    
    public Transform spikes;        
   
    public float raiseHeight = 1f;  
    public float speed = 3f;          
    public float detectionRange = 3f;

    private Vector3 downPos;        
    private Vector3 upPos;          

    private Transform playerTransform;
    private Player player;
    private EntityHealth entityHealth;

    void Start()
    {
        downPos = spikes.localPosition;

        upPos = downPos + Vector3.up * raiseHeight;
    }
 
}
