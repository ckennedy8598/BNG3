using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// remember to assign everything you need to
// put this on the triggers for dialogue
// remember to write out the text you want for each line / page

namespace Platformer
{
    public class DialogueTrigger : MonoBehaviour
    {
        public string[] dialogueLines;
        public Dialogue dialogueScript;
        public bool triggerOnce = true;
        public TextMeshProUGUI textComponent;

        private bool hasTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!hasTriggered && other.CompareTag("Player"))
            {
                dialogueScript.lines = dialogueLines;
                dialogueScript.gameObject.SetActive(true); // Reactivate in case it's disabled
                dialogueScript.SendMessage("StartDialogue"); // Call StartDialogue indirectly

                if (triggerOnce)
                    hasTriggered = true;
                Debug.Log("Custom Text from trigger was successfully sent");
            }
        }
    }
}
