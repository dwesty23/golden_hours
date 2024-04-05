using UnityEngine;

public class Tester : MonoBehaviour
{
    public Conversation convo;
    public void StartConvo()
    {
        DialogueManagerM.StartConversation(convo);
    }
    public bool IsConversationFinished() 
    {
        return DialogueManagerM.IsConversationFinished();
    }
}