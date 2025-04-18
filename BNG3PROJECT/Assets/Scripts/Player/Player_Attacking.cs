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
        public TMP_Text parryText;

        // Weapon_GetCollision Reference
        [SerializeField]
        public Weapon_GetCollision Weapon_Collision_Script;

        [Header("Animation Variables")]
        public Animator Anim;

        [Header("Player Variables")]
        private Player_Movement _pm;
        public Transform OrientObj;

        [Header("Melee Variables")]
        private bool _meleeAllowed;
        private float _blockTimerMax = .25f;
        private float _blockTimer;
        public BoxCollider MeleeHitbox;
        public TMP_Text BlockIndicator;
        public AudioSource ParrySound;
        public bool CanParry;
        public bool CanBlock;
        public bool IsBlocking;

        // Combo Attack Variables
        private float _comboTimePassed = 0.2f;
        private float _comboTimer;
        private bool _startTimer;
        private int _numClicks;

        [Header("Ranged Variables")]
        private string _mana;
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

        //Player_Health Script Variable
        public Player_Health Player_Health_Script;
        public Pause_Menu PauseScript;
        public TMP_Text State_Shower;

        public AttackState State;
        public enum AttackState
        {
            Neutral,
            Light_Attack,
            Light_Attack2,
            Heavy_Attack,
            Ranged,
            Blocking,
        }
        // Start is called before the first frame update
        void Start()
        {
            _pm = GetComponent<Player_Movement>();
            Player_Health_Script = GetComponent<Player_Health>();
            PauseScript = GetComponent<Pause_Menu>();
            AllowedToShoot = true; _meleeAllowed = true;
            _allowInvoke = true;
            PlayerMana = 20;
        }

        // Update is called once per frame
        void Update()
        {
            if (_pm != null)
            {
                if (_pm.state == Player_Movement.MovementState.paused)
                {
                    return;
                }
            }
            _stateHandler();
            Ray debugRay = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.DrawRay(debugRay.origin, debugRay.direction * 80, Color.green);

            _getInput();
            _comboTimerMethod();
            _soulOverflow();
            _manaRegen();

            // Display Mana Count in whole numbers (#)
            _mana = PlayerMana.ToString("#");
            ManaCounter.text = "Mana: " + _mana;
        }



        // Controls what is possible in each state
        private void _stateHandler()
        {
            
            if (_pm.state == Player_Movement.MovementState.paused)
            {
                return;
            }

            if (State == AttackState.Neutral)
            {
                State_Shower.text = "Player State: Neutral";

                // All combo variables
                Anim.SetBool("Light_Attack_Combo_Allowed", false);
                Anim.SetBool("Block_Bool_Loop", false);
                Anim.ResetTrigger("Light_Attack_Combo_Trigger");
                _comboTimer = 0; _blockTimer = 0;
                _startTimer = false; IsBlocking = false;

                Player_Health_Script.CanBeDamaged = true;
                Player_Health_Script.HalfDamage = false;
                CanBlock = true;
                _meleeAllowed = true;
                AllowedToShoot = true;
                _allowInvoke = true;
            }

            if (State == AttackState.Blocking)
            {
                State_Shower.text = "Player State: Blocking";
                IsBlocking = true;
                Player_Health_Script.HalfDamage = true;
                AllowedToShoot = false;
                _meleeAllowed = false;
                if (_blockTimer < _blockTimerMax)
                {
                    _blockTimer += Time.deltaTime;
                    CanParry = true;
                    Player_Health_Script.CanBeDamaged = false;
                    parryText.text = "Parry State: True";
                    //Debug.Log("This is _canParry: " + _canParry);
                }
                else
                {
                    CanParry = false;
                    Player_Health_Script.CanBeDamaged = true;
                    parryText.text = "Parry State: False";
                    //Debug.Log("This is _canParry: " + _canParry);
                }
            }

            // If in proper state, range attack anim and setting
            if (State == AttackState.Ranged)
            {
                State_Shower.text = "Player State: Ranged";
                _meleeAllowed = false;
                Anim.SetBool("Firing_State", true);
            }
            else
            {
                Anim.SetBool("Firing_State", false);
            }

            // If in proper state, light attack
            if (State == AttackState.Light_Attack)
            {
                State_Shower.text = "Player State: Light Attack";
                // Combo Timer
                Anim.ResetTrigger("Light_Attack_Combo_Trigger");
                Anim.SetBool("Light_Attack_Combo_Allowed", true);
                _startTimer = true;

                AllowedToShoot = false;
                _meleeAllowed = false;
                CanBlock = false;
            }

            if (State == AttackState.Light_Attack2)
            {
                State_Shower.text = "Player State: Combo Light Attack";

                // Combo Variables
                _startTimer = false;
                _comboTimer = 0;

                AllowedToShoot = false;
                _meleeAllowed = false;
                CanBlock = false;
            }

            if (State == AttackState.Heavy_Attack)
            {
                State_Shower.text = "Player State: Heavy Attack";
                AllowedToShoot = false;
                _meleeAllowed = false;
                CanBlock = false;
                //Anim.SetTrigger("Heavy_Attack_Trigger");
            }
        }

        private void _getInput()
        {
            // Light Attack Input
            if (State == AttackState.Neutral && Input.GetMouseButtonDown(0))
            {
                _lightAttack();
            }

            if (State == AttackState.Light_Attack)
            {
                if (_comboTimer > _comboTimePassed && Input.GetMouseButtonDown(0))
                {
                    _lightAttack2();
                }
            }

            // Heavy Attack Input
            if (State == AttackState.Neutral && Input.GetMouseButtonDown(2))
            {
                _heavyAttack();
            }

            // Ranged Attack Input
            if (State == AttackState.Neutral && Input.GetMouseButton(1))
            {
                if (PlayerMana > FireballCost)
                {
                    _shoot();
                }
            }

            // Blocking Input
            if (Input.GetKeyDown(KeyCode.LeftControl) && CanBlock)
            {
                if (IsInvoking("_resetState"))
                {
                    CancelInvoke("_resetState");
                    _allowInvoke = true;
                }
                State = AttackState.Blocking;
                Anim.SetBool("Block_Bool_Loop", true);
                Debug.Log("Annoy Log For Blocking Lol");
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                if (_allowInvoke)
                {
                    CanBlock = false;
                    Invoke("_resetState", 1);
                    _allowInvoke = false;
                }
            }
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
                Invoke("_resetState", TimeBetweenShots);
                _allowInvoke = false;
            }
        }

        private void _soulOverflow()
        {
            // Neutral state reset
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

            // Set power up use state
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

        // Method for checking time passed

        // Method for light attacking; does not include input
        private void _lightAttack()
        {
            State = AttackState.Light_Attack;
            
            // Deal Light Attack Damage on Collision
            Weapon_Collision_Script.DealDamage(25);
            Anim.SetTrigger("Light_Attack_Trigger");
            _comboTimerMethod();

            if (_allowInvoke)
            {
                Invoke("_resetState", 1);
                _allowInvoke = false;
            }
        }
        private void _comboTimerMethod()
        {
            if (_comboTimer < _comboTimePassed && _startTimer)
            {
                _comboTimer += Time.deltaTime;
            }
            else if (_comboTimer > _comboTimePassed)
            {
                //Debug.Log("Combo Timer Counter: " + _comboTimer);
                _startTimer = false;
                _meleeAllowed = true;
            }
        }

        private void _lightAttack2()
        {
            State = AttackState.Light_Attack2;
            if (IsInvoking("_resetState"))
            {
                CancelInvoke("_resetState");
                _allowInvoke = true;
            }

            Weapon_Collision_Script.DealDamage(25);
            Anim.SetTrigger("Light_Attack_Combo_Trigger");

            if (_allowInvoke)
            {
                Invoke("_resetState", 1f);
                _allowInvoke = false;
            }
        }

        // Method for heavy attacking; does not include input
        private void _heavyAttack()
        {
            State = AttackState.Heavy_Attack;

            // Deal Heavy Attack Damage on Collision
            Weapon_Collision_Script.DealDamage(100);
            Anim.SetTrigger("Heavy_Attack_Trigger");

            if (_allowInvoke)
            {
                Invoke("_resetState", 2.2f);
                _allowInvoke = false;
            }
        }

        private void _manaRegen()
        {
            ManaBar.value = PlayerMana;

            if (PlayerMana < 20)
            {
                // Increase 
                PlayerMana += (.5f * Time.deltaTime);
            }
            else if (PlayerMana > 20)
            {
                PlayerMana = 20;
            }
        }

        public void IncreaseMana(float mana)
        {
            PlayerMana += mana;
        }


        // Cooldown for attacks, shots, and state change
        private void _resetState()
        {
            State = AttackState.Neutral;
        }
    }
}
