using UnityEngine.UIElements.Experimental;

namespace Platformer
{
    public partial interface IState
    {
        

        public interface ITransition
        {
            IState To { get; }
            IPredicate Condition { get; }
        }
    }

    
}

