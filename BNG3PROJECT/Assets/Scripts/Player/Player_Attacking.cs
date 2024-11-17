using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace Platformer
{
    public class Player_Attacking : MonoBehaviour
    {
        [Header("Animation Variables")]
        public Animator Anim;

        [Header("Player Variables")]
        private Rigidbody _rb;
        private Player_Movement _pm;
        public Transform OrientObj;

        [Header("Melee Variables")]
        private bool _meleeAllowed;
        public BoxCollider MeleeHitbox;

        [Header("Ranged Variables")]
        private string _mana;
        private bool _shooting;
        private bool _allowInvoke;
        private float PlayerMana;
        public TMP_Text ManaCounter;
        public GameObject Fireball;
        public bool AllowedToShoot;
        public float FireballCost;
        public float ShootForce;
        public float TimeBetweenShots;
        public float Spread;

        [Header("Soul Overflow Elements")]
        [SerializeField]
        private float _soulOverflowDuration;
        [SerializeField]
        private float _soulOverflowCooldown;
        [SerializeField]
        private bool _inOverflow;
        public Animator Soul_Overflow_Animator;

        [Header("Mana Bar")]
        public Slider ManaBar;

        [Header("Camera Variables")]
        public Camera Camera;
        public Transform createPoint;

        public AttackState State;
        public enum AttackState
        {
            Neutral,
            Light_Attack,
            Light_Attack2,
            Heavy_Attack,
            Ranged,
        }
        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _pm = GetComponent<Player_Movement>();
            AllowedToShoot = true; _meleeAllowed = true;
            _allowInvoke = true;
            PlayerMana = 20;
        }

        // Update is called once per frame
        void Update()
        {
            _stateHandler();
            Ray debugRay = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(debugRay.origin, debugRay.direction * 80, Color.green);

            _soulOverflow();

            _manaRegen();

            // Display Mana Count in whole numbers (#)
            _mana = PlayerMana.ToString("#");
            ManaCounter.text = "Mana: " + _mana;
        }


        private void _shoot()
        {
            AllowedToShoot = false;
            State = AttackState.Ranged;
            PlayerMana -= FireballCost;

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

        private void _soulOverflow()
        {
            if (!_inOverflow)
            {
                FireballCost = 2f;
                TimeBetweenShots = .4f;
            }

            if (_soulOverflowDuration > 0)
            {
                _soulOverflowDuration -= Time.deltaTime;
            }
            else
            {
                _inOverflow = false;
            }

            if (_soulOverflowCooldown > 0)
            {
                _soulOverflowCooldown -= Time.deltaTime;
                
                return;
            }

            if (Input.GetKeyDown(KeyCode.Q) && _soulOverflowCooldown <= 0)
            {
                Soul_Overflow_Animator.SetTrigger("Soul_Overflow");
                _soulOverflowCooldown = 10f; _soulOverflowDuration = 2f;
                if (_soulOverflowDuration > 0)
                {
                    _inOverflow = true;
                    FireballCost = 0f;
                    PlayerMana = 20;
                    TimeBetweenShots = .2f;
                }
            }
        }

        private void _lightAttack()
        {
            State = AttackState.Light_Attack;
            _meleeAllowed = false;
            Anim.SetTrigger("Light_Attack_Trigger");

            if (_allowInvoke)
            {
                Invoke("_resetMelee", 1);
                _allowInvoke = false;
            }
        }

        // Cooldown between shots
        private void _resetShot()
        {
            State = AttackState.Neutral;
            AllowedToShoot = true;
            _meleeAllowed = true;
            _allowInvoke = true;
        }

        // Time Between Melee Attacks
        private void _resetMelee()
        {
            State = AttackState.Neutral;
            _meleeAllowed = true;
            AllowedToShoot = true;
            _allowInvoke = true;
        }

        private void _manaRegen()
        {
            ManaBar.value = PlayerMana;

            if (PlayerMana < 20)
            {
                PlayerMana += Time.deltaTime;
            }
            else if (PlayerMana > 20)
            {
                PlayerMana = 20;
            }
        }

        private void _stateHandler()
        {
            if (_pm.state == Player_Movement.MovementState.paused)
            {
                return;
            }

            if (State  == AttackState.Neutral)
            {

            }

            if (State == AttackState.Ranged && State != AttackState.Light_Attack)
            {
                Anim.SetBool("Firing_State", true);
            }
            else
            {
                Anim.SetBool("Firing_State", false);
            }

            if (_meleeAllowed && Input.GetMouseButtonDown(0))
            {
                AllowedToShoot = false;
                _lightAttack();
            }

            if (AllowedToShoot && Input.GetMouseButton(1))
            {
                if (PlayerMana > FireballCost)
                {
                    _meleeAllowed = false;
                    _shoot();
                }
            }
        }
    }
}
