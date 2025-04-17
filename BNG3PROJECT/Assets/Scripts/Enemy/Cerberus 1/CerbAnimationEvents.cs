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
        void Start()
        {
            CF = GetComponentInParent<CerberusFinal>();
            animator = GetComponent<Animator>();
        }

        public void BigDie()
        {
            Destroy(transform.parent.gameObject);
        }

        public void Fireball()
        {
            //Instantiate(CerbFire, transform.Find("CerbSpawnPoint").position, Quaternion.identity);
            CF.isFiring = false;
            animator.SetTrigger("endShot");
            CF.ResetFireTime();
            
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
