using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public Image speakerImage;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;

    public DialogLine[] lines;
    public float typingSpeed = 0.03f;

    private int index = 0;
    private bool isTyping = false;        
    private bool textFullyShown = false;  

    private Coroutine typingCoroutine;

    void Start()
    {
        ShowLine(index);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[index].text;
                isTyping = false;
                textFullyShown = true;
            }
            else
            {
                NextLine();
            }
        }
    }

    void ShowLine(int i)
    {
        speakerImage.sprite = lines[i].speakerSprite;
        speakerNameText.text = lines[i].speakerName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(lines[i].text));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        textFullyShown = false;

        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        textFullyShown = true;
    }

    void NextLine()
    {
        if (!textFullyShown) return;

        index++;

        if (index < lines.Length)
        {
            ShowLine(index);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
