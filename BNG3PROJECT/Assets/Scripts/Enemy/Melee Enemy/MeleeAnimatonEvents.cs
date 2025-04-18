using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class MeleeAnimatonEvents : MonoBehaviour
    {
        // Start is called before the first frame update

        public MeleeEnemyFinal MF;

        public Animator animator;

        public GameObject player;

        public AudioClip AU;
        void Start()
        {
            MF = GetComponentInParent<MeleeEnemyFinal>();
            animator = GetComponent<Animator>();
            
        }
        public void StartDeath()
        {
            MF.isDead = true;
            MF.State = MeleeEnemyFinal.MeleeState.Die;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
        }
        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
        public void ResetMelee()
        {
            animator.SetTrigger("endAttack");
        }

        public void AttackSound()
        {
            AudioSource.PlayClipAtPoint(AU, transform.position, 0.2f);
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
