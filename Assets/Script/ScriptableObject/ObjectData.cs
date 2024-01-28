using UnityEngine;
// using DialogueEditor;

[CreateAssetMenu(fileName = "NewObjectData", menuName = "Object Data")]
public class ObjectData : ScriptableObject
{
    public string ObjectName;
    public string description;
    public float positionX;
    public float positionZ;
    public GameObject interactivObject;
    // public NPCConversation dialogue;
}
