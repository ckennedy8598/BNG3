using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Title : MonoBehaviour
    {
        public GameObject Storyboard;

        public void _setInactiveAndSetStoryboard()
        {
            this.gameObject.SetActive(false);
            Storyboard.SetActive(true);
        }
    }
}
