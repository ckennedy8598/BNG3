using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//dusing UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Platformer
{
    public class Player_Dashing : MonoBehaviour
    {
        [Header("References")]
        private Rigidbody _rb;
        private Player_Movement _pm;
        public Transform OrientObject;
        public Transform PlayerCam;

        [Header("Dashing")]
        public float DashForce;
        public float DashUpwardForce;
        public float DashDuration;

        [Header("Cooldown")]
        public Animator anim;
        public float _dashCDTimer;
        public float DashCD;

        [Header("Input")]
        public KeyCode DashKey = KeyCode.LeftShift;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _pm = GetComponent<Player_Movement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(DashKey))
            {
                _dash();
                //Debug.Log("Player_Dashing.cs Script: LeftShiftKey Down");
            }

            if (_dashCDTimer >= 0)
            {
                _dashCDTimer -= Time.deltaTime;
            }
        }

        private void _dash()
        {
            //Debug.Log("Player_Dashing.cs _dash() WORKING");
            if (_dashCDTimer > 0)
            {
                return;
            }
            else
            {
                _dashCDTimer = DashCD;
            }
            _pm.Dashing = true;
            anim.SetTrigger("Dashed");
            Vector3 appliedForce = OrientObject.forward * DashForce + OrientObject.up * DashUpwardForce;
            delayedForceToApply = appliedForce;
            
            Invoke(nameof(_delayedDashForce), 0.025f);

            Invoke(nameof(_resetDash), DashDuration);

        }

        private Vector3 delayedForceToApply;
        private void _delayedDashForce()
        {
            _rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        }

        private void _resetDash()
        {
            _pm.Dashing = false;
            //Debug.Log("_pm.Dashing = " + _pm.Dashing);
        }
    }
}
