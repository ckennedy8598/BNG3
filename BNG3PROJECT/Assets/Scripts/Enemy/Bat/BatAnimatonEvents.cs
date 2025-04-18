using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        public void StartDeath()
        {
            BF.isDead = true;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
            BF.state = BatFinal.BatState.Die;
            
        }
        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
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
