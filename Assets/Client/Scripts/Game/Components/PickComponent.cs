namespace Game
{
    public struct PickComponent
    {
        public Player Player;
        public Crap Target;
        public float PrePickDuration;
        public float PostPickDuration;
        public bool Picked;
    }
}