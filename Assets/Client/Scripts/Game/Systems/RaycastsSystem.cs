using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class RaycastsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<RaycastComponent> _filter = default;
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
        private readonly StaticData _staticData = default;
        private RaycastHit[] results = default;
    
        public void Init()
        {
            results = new RaycastHit[_staticData.RaycastResylts];
        }

        public void Run()
        {
            foreach(var i in _filter)
            {
                _filter.GetEntity(i).Del<RaycastComponent>();
            }

            var screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -_sceneData.MainCamera.transform.position.z);
            var ray = _sceneData.MainCamera.ScreenPointToRay(screenPos);

            var hits = Physics.RaycastNonAlloc(ray, results, _staticData.InputRayDistance, _staticData.InputLayers);

            for(var i = 0; i < hits; i++)
            {
                ref var r = ref _world.NewEntity().Get<RaycastComponent>();
                r.Hit = results[i];
            }
        }
    }
}