using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Jukebox_Script : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            // Rotate Jukebox
            transform.Rotate(0f, 30 * Time.deltaTime, 0f, Space.Self);
        
        }
    }
}
