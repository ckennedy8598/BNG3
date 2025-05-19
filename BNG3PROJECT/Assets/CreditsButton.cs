using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class CreditsButton : MonoBehaviour
    {
        public GameObject button;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(CreditsButtonTimer());
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private IEnumerator CreditsButtonTimer()
        {
            yield return new WaitForSeconds(115f);
            button.SetActive(true);
        }

        public void MainMenuPress()
        {
            SceneManager.LoadScene(0);
        }
    }
}
