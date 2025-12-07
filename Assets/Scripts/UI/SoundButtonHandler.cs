using UnityEngine;
using UnityEngine.EventSystems;

public class SoundButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("!!! SoundButtonHandler clicked !!!");
        
        if (AudioManager.Instance != null)
        {
            //Debug.Log($"Trước: IsSoundOn = {AudioManager.Instance.IsSoundOn}");
            //AudioManager.Instance.ToggleSound();
            //Debug.Log($"Sau: IsSoundOn = {AudioManager.Instance.IsSoundOn}");
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null!");
        }
    }
}
