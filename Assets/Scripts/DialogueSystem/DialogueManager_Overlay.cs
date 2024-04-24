using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManagerOverlay : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue;
    public Image speakerSprite;
    public Sprite SophieSprite;

    private int currentIndex;
    private static DialogueManagerOverlay instance;
    private Conversation currentConvo;
    private Animator animator;
    private bool finishTyping = false;
    private bool conversationFinished = true;
    private bool UsingUpperCase = false;
    private bool UsingLowerCase = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            conversationFinished = false;
            animator = GetComponent<Animator>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (finishTyping)
            {
                ReadNext();
            }
            else if (!finishTyping)
            {
                StopAllCoroutines();
                if (UsingUpperCase)
                {
                    instance.dialogue.text = currentConvo.GetLineByIndex(currentIndex-1).dialogue.ToUpper();
                }
                else if (UsingLowerCase)
                {
                    instance.dialogue.text = currentConvo.GetLineByIndex(currentIndex-1).dialogue.ToLower();
                }
                finishTyping = true;
            }
        }
    }
    public static void StartConversation(Conversation convo)
    {
        Debug.Log("StartConversation called");
        instance.animator.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        // instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.conversationFinished = false;

        instance.ReadNext();
    }

    public void ReadNext()
    {
        Debug.Log("ReadNext called");
        if (currentIndex > currentConvo.GetLength())
        {
            instance.animator.SetBool("isOpen", false);
            Debug.Log("Conversation Finished");
            // Set sprite to Sophie Mouth Closed Sprite
            speakerSprite.sprite = SophieSprite;
            conversationFinished = true;
            return;
        }
        // speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        finishTyping = false;
        instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue, currentConvo.GetLineByIndex(currentIndex).speaker.GetFont()));
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        instance.dialogue.font = currentConvo.GetLineByIndex(currentIndex).speaker.GetFont();
        currentIndex++;
    }

    private IEnumerator TypeText(string text, TMP_FontAsset currentFont)
    {

        dialogue.text = "";
        foreach (char c in text)
        {
            if (currentFont.name == "Font Diner Boss SDF")
            {
                UsingUpperCase = true;
                UsingLowerCase = false;
                dialogue.text += c.ToString().ToUpper();
            }
            else 
            {
                UsingUpperCase = false;
                UsingLowerCase = true;
                dialogue.text += c.ToString().ToLower();
            }
            yield return new WaitForSeconds(0.05f);
        }
        finishTyping = true;
    }

    public static bool IsConversationFinished()
    {
        return instance.conversationFinished;
    }
}