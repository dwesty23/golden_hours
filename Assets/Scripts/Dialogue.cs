using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{

public TextMeshProUGUI textDisplay;
public string[] sentences;
public float typingSpeed;
private int index;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameObject.SetActive(false);
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void NextLine()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
