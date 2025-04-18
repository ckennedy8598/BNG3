using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class demo_script : MonoBehaviour
    {
        private float counter;
        void Update()
        {
            counter += Time.deltaTime;

            if (counter > 20)
            {
                Application.Quit();
            }
        }
    }
}
