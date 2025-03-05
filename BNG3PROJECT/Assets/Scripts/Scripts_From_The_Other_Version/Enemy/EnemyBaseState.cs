using UnityEngine;

namespace Platformer
{
    public abstract class EnemyBaseState : IState
    {
        //Animation Hashes

        protected static readonly int IdleHash = Animator.StringToHash(name: "IdleNormal");
        //protected static readonly int RunHash = Animator.StringToHash(name: "Run");

        //walk hash
        protected static readonly int WalkHash = Animator.StringToHash(name: "Walk");
        //attack hash
        protected static readonly int AttackHash = Animator.StringToHash(name: "Attack");
        //death hash
        protected static readonly int DeathHash = Animator.StringToHash(name: "Death");

        public readonly Improved_Enemy_Test_script enemy;
        protected readonly Animator animator;

        protected const float crossFadeDuration = 1.0f;
        protected EnemyBaseState(Improved_Enemy_Test_script enemy, Animator animator)
        {
            this.enemy = enemy;
            this.animator = animator;
        }

        public virtual void FixedUpdate()
        {
            
        }

        public virtual void OnEnter()
        {
            
        }

        public virtual void OnExit()
        {
            
        }

        public virtual void Update()
        {
            
        }
    }
}
