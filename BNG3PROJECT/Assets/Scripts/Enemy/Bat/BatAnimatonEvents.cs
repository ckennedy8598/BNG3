using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BatAnimatonEvents : MonoBehaviour
    {
        // Start is called before the first frame update

        public BatFinal BF;

        public Animator animator;

        public GameObject player;
        void Start()
        {
            BF = GetComponentInParent<BatFinal>();
            animator = GetComponent<Animator>();
        }

        public void ResetMelee()
        {
            animator.SetTrigger("endAttack");
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
