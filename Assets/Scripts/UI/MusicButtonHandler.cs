using UnityEngine;
using UnityEngine.EventSystems;

public class MusicButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("!!! MusicButtonHandler clicked !!!");
        
        if (AudioManager.Instance != null)
        {
            Debug.Log($"Trước: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            AudioManager.Instance.ToggleMusic();
            Debug.Log($"Sau: IsMusicOn = {AudioManager.Instance.IsMusicOn}");
            
            // Update icon manually
            UpdateIcon();
        }
        else
        {
            Debug.LogError("AudioManager.Instance is null!");
        }
    }
    
    private void UpdateIcon()
    {
        SettingsPopup popup = GetComponentInParent<SettingsPopup>();
        if (popup != null)
        {
            // Force UpdateUI by disabling and re-enabling
            popup.gameObject.SetActive(false);
            popup.gameObject.SetActive(true);
        }
    }
}
