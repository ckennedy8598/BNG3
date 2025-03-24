using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// inspired by this tut https://youtu.be/8oTYabhj248?si=sNnmE4SlLYG3s7JO
// some of this isnt used actually bc I dont currently want text to appear as soon as you spawn, if i change my mind, uncomment the stuff for something
// like "Infernus" or the name of the level to always appear?

namespace Platformer
{
    public class Dialogue : MonoBehaviour
    {

        public TextMeshProUGUI textComponent;
        public string[] lines;
        public float textSpeed;

        private int index;


        // Start is called before the first frame update
        void Start()
        {
            textComponent.text = string.Empty;
            gameObject.SetActive(false); // deactivate immediately on spawning
            //StartDialogue();
        }

        // Update is called once per frame
        void Update() //commented out bc I dont want mouse click progression and ending for now
        {
           // if(Input.GetMouseButtonDown(0))
            {
               // if (textComponent.text == lines[index])
                {
               //     NextLine();
                }
               // else
                {
                //    StopAllCoroutines();
                 //   textComponent.text = lines[index];
                }
            }
        }

        public void StartDialogue()
        {
            index = 0;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            yield return new WaitForSeconds(2f); // delay x secs before moving on
            NextLine();
        }

        void NextLine()
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
