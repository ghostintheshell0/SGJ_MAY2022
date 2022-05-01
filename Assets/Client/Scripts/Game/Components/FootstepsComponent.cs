using Leopotam.Ecs;

namespace Game
{
    public struct FootstepsComponent
    {
        public Step View;
        public float LifeTime;
        public float MaxLifeTime;
        public EcsEntity SpawnerEnt;

    }
}