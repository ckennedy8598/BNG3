using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class Cerb3AnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update
        public Cerb3 C3;

        public GameObject Icicle;

        public Animator animator;

        public Transform cerbSpawnPoint;

        public GameObject AR;

        public NavMeshAgent agent;


        void Start()
        {
            C3 = GetComponentInParent<Cerb3>();
            animator = GetComponent<Animator>();
            cerbSpawnPoint = GameObject.FindWithTag("CerbSpawnPoint").transform;
            AR = GameObject.FindWithTag("CerberusAttackRange");
            agent = GetComponentInParent<NavMeshAgent>();



        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }
        public void IcicleShoot()
        {
            Instantiate(Icicle, cerbSpawnPoint.position, Quaternion.identity);
            C3.isFiring = false;
            C3.ResetFireTime();
            animator.SetTrigger("endShot");
        }
        public void AttackActivate()
        {
            AR.SetActive(true);
        }

        public void AttackDeactivate()
        {
            AR.SetActive(false);
        }
        public void Cerb3ResetWalk()
        {
            C3.State = Cerb3.CerbState.Walk;
        }
        public void Cerb3StartDeath()
        {
            C3.isDead = true;
            C3.State = Cerb3.CerbState.Die;
            gameObject.GetComponentInParent<NavMeshAgent>().isStopped = true;
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
