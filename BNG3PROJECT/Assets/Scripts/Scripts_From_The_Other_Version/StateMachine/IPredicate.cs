namespace Platformer
{
    public partial interface IState
    {
        // Start is called before the first frame update


        // Update is called once per frame
        public interface IPredicate
        {
            bool Evauluate();

        }
    }
}
