using UnityEngine;

[CreateAssetMenu(fileName = "NewContact", menuName = "Data/New Contact")]

public class PhoneContact : ScriptableObject
{
    public string name;
    public StoryScene scene;
}
