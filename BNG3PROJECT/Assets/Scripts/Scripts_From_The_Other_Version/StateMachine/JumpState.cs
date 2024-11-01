using UnityEngine;

namespace Platformer
{
    public class JumpState : BaseState 
    { 
        //public JumpState(Player_Movement player, Animator animator) : base(player, animator) { }

        public override void OnEnter()
        {
            animator.CrossFade(JumpHash, crossFadeDuration);
            

        }

        public override void FixedUpdate()
        {
            //Call Player jump logic and move logic
            
        }
    }
}

