using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using KBCore.Refs;
using static Platformer.IState;

namespace Platformer
{
    [RequireComponent(typeof(NavMeshAgent))]

    public class Improved_Enemy_Test_script : MonoBehaviour



    {



        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Child] Animator animator;

        StateMachine stateMachine;

        private void OnValidate() => this.ValidateRefs();
        



        private void Awake()
        {
            //player = GameObject.Find("Player").transform;
            //agent = GetComponent<NavMeshAgent>();

        }

        // Start is called before the first frame update
        void Start()
        {
            stateMachine = new StateMachine();

            var wanderState = new EnemyWanderState(enemy: this, animator, agent, wanderRaduis:20f);

            Any(wanderState, condition: new FuncPredicate(() => true)); //Always True

            stateMachine.SetState(wanderState);


        }

        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        // Update is called once per frame
        void Update()
        {

        }
        private void OnDrawGizmosSelected()
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, attackRange);

            //Gizmos.color = Color.yellow;
            //Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
