using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

        public GameObject AR;

        public GameObject SR;
        void Start()
        {
            BF = GetComponentInParent<BatFinal>();
            animator = GetComponent<Animator>();
            AR = GameObject.FindWithTag("CerberusAttackRange");
            SR = GameObject.FindWithTag("SightRange");
        }

        public void AttackActivate()
        {
            AR.SetActive(true);
        }

        public void BatResetMelee()
        {
            animator.SetTrigger("endAttack");
        }

        public void AttackDeactivate()
        {
            AR.SetActive(false);
        }

        public void SightActivate()
        {
            SR.SetActive(true);
        }

        public void SightDeactivate()
        {
            SR.SetActive(false);
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
