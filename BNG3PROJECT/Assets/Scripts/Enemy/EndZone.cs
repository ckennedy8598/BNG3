using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EndZone : MonoBehaviour
    {
        // Start is called before the first frame update
        public SlimeScriptfornow slime;
        void Start()
        {
            slime = FindAnyObjectByType<SlimeScriptfornow>();
            
        }

        // Update is called once per frame
        void Update()
        {
            float dist = Vector3.Distance(this.transform.position, slime.transform.position);

            if (dist <= 0)
            {
                Destroy(slime.gameObject);
            }
        }
    }
}
