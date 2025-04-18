using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class MeleeAnimatonEvents : MonoBehaviour
    {
        // Start is called before the first frame update

        public MeleeEnemyFinal MF;

        public Animator animator;

        public GameObject player;
        void Start()
        {
            MF = GetComponentInParent<MeleeEnemyFinal>();
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
