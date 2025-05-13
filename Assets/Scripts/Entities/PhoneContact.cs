using UnityEngine;

[CreateAssetMenu(fileName = "NewContact", menuName = "Data/Entities/New Contact")]

public class PhoneContact : ScriptableObject
{
    public string contactName;
    public StoryScene scene;
}
