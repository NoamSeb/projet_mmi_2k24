using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ObjectDisplay : MonoBehaviour
{
    public List<ObjectData> interactiveObject;
    [SerializeField] GameObject dialog;

    void Start()

    {
        if (interactiveObject != null && interactiveObject.Count > 0)
        {
            foreach (ObjectData interactivObject in interactiveObject)
            {
                if (interactivObject != null && interactivObject.interactivObject != null)
                {
                    GameObject obj = Instantiate(interactivObject.interactivObject);
                    obj.tag = "interactiv";
                }
                else
                {
                    Debug.LogWarning("One of the interactiveObjects has a null or missing GameObject. No object instantiated in the scene.");
                }
            }
        }
        else
        {
            Debug.LogError("interactiveObjects is not defined or empty");
        }
    }

    // If the object has a dialogue, start the conversation
    public void TriggerDialog()
    {
        foreach (ObjectData objData in interactiveObject)
        {
            if (objData != null && objData.dialogue != null)
            {

                Debug.Log("Conversation started");
                Debug.Log(objData);
                Debug.Log(objData.dialogue);
                // ConversationManager.Instance.StartConversation(objData.dialogue);

            }
            else
            {
                Debug.LogWarning("One of the interactiveObjects has a null or missing dialogue. No conversation started.");
            }
        }
    }
}
