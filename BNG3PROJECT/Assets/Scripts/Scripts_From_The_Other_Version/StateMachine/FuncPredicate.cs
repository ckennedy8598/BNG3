using System;
namespace Platformer
{
    public partial interface IState
    {
        public class FuncPredicate : IPredicate
        {
            readonly Func<bool> func;


            public FuncPredicate(Func<bool> func)
            {
                this.func = func;
            }

            public bool Evauluate() => func.Invoke();
        }
    }
}
