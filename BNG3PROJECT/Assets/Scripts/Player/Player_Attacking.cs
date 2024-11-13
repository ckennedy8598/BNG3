using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    public class Player_Attacking : MonoBehaviour
    {
        [Header("Player Variables")]
        private Rigidbody _rb;
        private Player_Movement _pm;
        public Transform OrientObj;

        [Header("Ranged Variables")]
        private bool _shooting;
        private bool _allowInvoke;
        public GameObject Fireball;
        public bool AllowedToShoot;
        public float ShootForce;
        public float TimeBetweenShots;
        public float Spread;

        [Header("Camera Variables")]
        public Camera Camera;
        public Transform createPoint;

        public AttackState State;
        public enum AttackState
        {
            Light_Attack,
            Light_Attack2,
            Heavy_Attack,
            Ranged
        }
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _pm = GetComponent<Player_Movement>();
            AllowedToShoot = true;
            _allowInvoke = true;
        }

        // Update is called once per frame
        void Update()
        {
            _stateHandler();
            Ray debugRay = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(debugRay.origin, debugRay.direction * 80, Color.green);
        }


        private void _shoot()
        {
            AllowedToShoot = false;

            // Find point from raycast
            Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // check if the ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75); // Random point far away from player
            }

            // Calculate direction from attackPoint to targetPoint
            Vector3 directionWithoutSpread = targetPoint - createPoint.position;

            // Calculate spread
            float x = Random.Range(-Spread, Spread);
            float y = Random.Range(-Spread, Spread);
            float z = Random.Range(-Spread, Spread);

            // Calculate new position w/ spread
            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, z);
            
            // Instantiate fireball - Stores spawned Fireball inside 'currentFireball'
            GameObject currentFireball = Instantiate(Fireball, createPoint.position, Quaternion.identity);

            // Rotate to shoot direction (I don't think we need?)
            currentFireball.transform.forward = directionWithoutSpread;

            // Add force to bullet
            currentFireball.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * ShootForce, ForceMode.Impulse);

            // Invoke resetShot function for delay between each shot
            if (_allowInvoke)
            {
                Invoke("_resetShot", TimeBetweenShots);
                _allowInvoke = false;
            }
        }
        private void _resetShot()
        {
            AllowedToShoot = true;
            _allowInvoke = true;
        }

        private void _stateHandler()
        {
            if (_pm.state == Player_Movement.MovementState.paused)
            {
                return;
            }

            if (AllowedToShoot && Input.GetMouseButton(1))
            {
                //do stuff
                State = AttackState.Ranged;
                _shoot();
            }
        }
    }
}
