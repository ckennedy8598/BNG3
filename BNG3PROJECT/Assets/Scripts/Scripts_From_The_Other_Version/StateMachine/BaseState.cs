using UnityEngine;

namespace Platformer
{


    public abstract class BaseState : IState
        {
            
            protected readonly Animator animator;


        protected static readonly int LocomotionHash = Animator.StringToHash(name: "Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash(name: "Jump");


        protected const float crossFadeDuration = 0.1f;

        //protected BaseState(Player_Movement player, Animator animator)
        //{
        //    this.player = player;
        //    this.animator = animator;

        //}
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

