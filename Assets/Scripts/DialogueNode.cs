using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
    public string condition;    //test impact des choix
    public string dialogueText;
    public List<DialogueResponse> responses;

    internal bool IsLastNode()
    {
        return responses.Count <= 0;
    }
}