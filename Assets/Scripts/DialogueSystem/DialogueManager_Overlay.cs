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
    public Sprite momSprite1;
    public Sprite momSprite2;
    public Sprite momSprite3;
    private string currentSpeaker;

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
        StopAllCoroutines();
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
        currentSpeaker = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite().name;
        if (currentSpeaker == "1MysteryMom1_v2")
        {
            StartCoroutine(CycleMomSprites());
        }
        else
        {
            speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        }
        instance.dialogue.font = currentConvo.GetLineByIndex(currentIndex).speaker.GetFont();
        currentIndex++;
    }

    private IEnumerator CycleMomSprites()
    {
        while (true)
        {
            speakerSprite.sprite = momSprite1;
            yield return new WaitForSeconds(0.1f);
            speakerSprite.sprite = momSprite2;
            yield return new WaitForSeconds(0.1f);
            speakerSprite.sprite = momSprite3;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator TypeText(string text, TMP_FontAsset currentFont)
    {

        dialogue.text = "";
        foreach (char c in text)
        {
            if (currentFont.name == "Font Diner Boss SDF" || currentFont.name == "Font Mayas Mom v2 SDF" || currentFont.name == "Font Maya SDF" || currentFont.name == "Font Cop SDF" || currentFont.name == "Font Kid Customer SDF")
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
            yield return new WaitForSeconds(0.07f);
        }
        finishTyping = true;
    }

    public static bool IsConversationFinished()
    {
        return instance.conversationFinished;
    }
}