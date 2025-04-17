using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    public class CerbAnimationEvents : MonoBehaviour
    {
        // Start is called before the first frame update

        public CerberusFinal CF;

        public GameObject CerbFire;

        public Animator animator;

        public Transform cerbSpawnPoint;

        public GameObject AR;

        
        void Start()
        {
            CF = GetComponentInParent<CerberusFinal>();
            animator = GetComponent<Animator>();
            cerbSpawnPoint = GameObject.FindWithTag("CerbSpawnPoint").transform;
            AR = GameObject.FindWithTag("CerberusAttackRange");
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }

        public void Fireball()
        {

            Instantiate(CerbFire, cerbSpawnPoint.position, Quaternion.identity);
            CF.isFiring = false;
            CF.ResetFireTime();
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

        public void ResetWalk()
        {
            CF.State = CerberusFinal.CerbState.Walk;
        }

        

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
