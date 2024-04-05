using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManagerM : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue;
    public Image speakerSprite;

    public Sprite SophieSprite;

    private int currentIndex;
    private static DialogueManagerM instance;
    private Conversation currentConvo;
    private Animator animator;
    private bool finishTyping = false;
    private bool conversationFinished = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
                instance.dialogue.text = currentConvo.GetLineByIndex(currentIndex-1).dialogue;
                finishTyping = true;
            }
        }
    }
    public static void StartConversation(Conversation convo)
    {
        instance.animator.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        // instance.speakerName.text = "";
        instance.dialogue.text = "";

        instance.ReadNext();
    }

    public void ReadNext()
    {
        if (currentIndex > currentConvo.GetLength())
        {
            instance.animator.SetBool("isOpen", false);
            // Set sprite to Sophie Mouth Closed Sprite
            speakerSprite.sprite = SophieSprite;
            conversationFinished = true;
            return;
        }
        // speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        finishTyping = false;
        instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        currentIndex++;
    }

    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";

        foreach (char c in text)
        {
            dialogue.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        finishTyping = true;
    }

    public static bool IsConversationFinished()
    {
        return instance.conversationFinished;
    }
}