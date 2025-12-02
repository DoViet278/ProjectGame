using UnityEngine;

public class SawRotation : MonoBehaviour
{
    private void Update()
    {
        gameObject.transform.Rotate(0, 0, 5f);
    }
}
