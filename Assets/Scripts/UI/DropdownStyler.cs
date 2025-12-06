using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Tự động style TMP_Dropdown để match với UI theme hiện tại
/// Chạy ngay trong Editor, không cần chạy game!
/// </summary>
[ExecuteInEditMode]
public class DropdownStyler : MonoBehaviour
{
    [Header("Style Settings")]
    [SerializeField] private Color textColor = new Color(1f, 0.92f, 0.016f); // Màu vàng
    [SerializeField] private Color backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.8f); // Màu tối
    [SerializeField] private Color highlightColor = new Color(0.2f, 0.2f, 0.2f, 0.9f);
    
    [Header("Auto Apply")]
    [SerializeField] private bool autoApply = true;
    
    private TMP_Dropdown dropdown;

    private void OnEnable()
    {
        if (autoApply)
        {
            ApplyStyle();
        }
    }

    [ContextMenu("Apply Style Now")]
    public void ApplyStyle()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        if (dropdown == null)
        {
            Debug.LogError("DropdownStyler: Không tìm thấy TMP_Dropdown!");
            return;
        }
        
        // 1. Ẩn nền dropdown chính
        Image mainImage = GetComponent<Image>();
        if (mainImage != null)
        {
            Color transparent = mainImage.color;
            transparent.a = 0;
            mainImage.color = transparent;
        }

        // 2. Style Label
        TextMeshProUGUI label = transform.Find("Label")?.GetComponent<TextMeshProUGUI>();
        if (label != null)
        {
            label.color = textColor;
        }

        // 3. Style Arrow
        Image arrow = transform.Find("Arrow")?.GetComponent<Image>();
        if (arrow != null)
        {
            arrow.color = textColor;
        }

        // 4. Style Template
        Transform template = transform.Find("Template");
        if (template != null)
        {
            Transform item = template.Find("Viewport/Content/Item");
            if (item != null)
            {
                // Item Background
                Image itemBg = item.Find("Item Background")?.GetComponent<Image>();
                if (itemBg != null)
                {
                    itemBg.color = backgroundColor;
                }

                // Item Label
                TextMeshProUGUI itemLabel = item.Find("Item Label")?.GetComponent<TextMeshProUGUI>();
                if (itemLabel != null)
                {
                    itemLabel.color = textColor;
                }

                // Item Checkmark
                Image checkmark = item.Find("Item Checkmark")?.GetComponent<Image>();
                if (checkmark != null)
                {
                    checkmark.color = textColor;
                }
            }

            // Scrollbar
            Scrollbar scrollbar = template.GetComponentInChildren<Scrollbar>();
            if (scrollbar != null)
            {
                ColorBlock colors = scrollbar.colors;
                colors.normalColor = backgroundColor;
                colors.highlightedColor = highlightColor;
                scrollbar.colors = colors;
            }
        }

        Debug.Log($"✅ Đã style {gameObject.name}!");
    }
}
