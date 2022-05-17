namespace Game
{
    public struct PickComponent
    {
        public PickBehaviour Picker;
        public Crap Target;
        public float PrePickDuration;
        public float PostPickDuration;
        public bool Picked;
    }
}