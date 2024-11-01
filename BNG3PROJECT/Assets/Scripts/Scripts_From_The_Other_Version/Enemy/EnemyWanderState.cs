using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class EnemyWanderState : EnemyBaseState
    {
        readonly NavMeshAgent agent;
        readonly Vector3 startPoint;
        readonly float wanderRadius;

        

        public EnemyWanderState(Improved_Enemy_Test_script enemy, Animator animator, NavMeshAgent agent, float wanderRaduis) : base(enemy, animator)
        {
            this.agent = agent;
            this.startPoint = enemy.transform.position;
            this.wanderRadius = wanderRaduis;
        }

        public override void OnEnter()
        {
            Debug.Log(message: "Wandering");
            animator.CrossFade(WalkHash, crossFadeDuration);
        }

        public override void Update()
        {
            if (HasReachedDestination())
            {
                //find a new destination

                //if this next line breaks, add Vector3
                var randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += startPoint;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, areaMask:1);
                var finalPosition = hit.position;
                agent.SetDestination(finalPosition);

            }
        }

        bool HasReachedDestination()
        {
            return !agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance &&
                    (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        }
    }



}
