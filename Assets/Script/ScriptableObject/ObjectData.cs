using UnityEngine;
using DialogueEditor;

[CreateAssetMenu(fileName = "NewObjectData", menuName = "interactivObject")]
public class ObjectData : ScriptableObject
{
    public string ObjectName;
    public GameObject interactivObject;
    public NPCConversation dialogue;
}
