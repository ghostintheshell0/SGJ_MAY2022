using Leopotam.Ecs;
using UnityEngine;
using Extensions;

namespace Game
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly StaticData _staticData = default;
        private readonly EcsFilter<RaycastComponent> _raycasts = default;
        private readonly EcsFilter<LockInputComponent> _locks = default;
    
        public void Run()
        {
            if(!_locks.IsEmpty()) return;
            
            if(Input.GetMouseButton(0))
            {
                if(TryGetInteractiveObject(out var interactiveObject, out var hit))
                {
                    var player = _sceneData.Player;
                    if(interactiveObject.Entity.Has<CrapComponent>() && player.Entity.Has<PickerComponent>())
                    {
                        ref var picker = ref player.Entity.Get<PickerComponent>();

                        if(picker.View.Character.transform.IsNear(hit.point, picker.View.PickDistance))
                        {
                            ref var cmd = ref player.Entity.Get<PickCommand>();
                            ref var crapComp = ref interactiveObject.Entity.Get<CrapComponent>();
                            cmd.Picker = picker.View;
                            cmd.Crap = crapComp.View;
                            return;
                        }
                    }
                }

                MoveTo(_sceneData.Player);
               
            }
        }

        private bool TryGetInteractiveObject(out MonoEntity interactiveObject, out RaycastHit hit)
        {
            foreach(var i in _raycasts)
            {
                ref var hit2 = ref _raycasts.Get1(i).Hit;

                if(_staticData.InteractiveObjectsLayer.Contains(hit2.collider.gameObject.layer))
                {
                    interactiveObject = hit2.collider.GetComponent<MonoEntity>();
                    hit = hit2;
                    return true;
                }
            }

            hit = default;
            interactiveObject = default;
            return false;
        }

        private void MoveTo(Character player)
        {
            if(player.Agent.enabled == false)
            {
                return;
            }

            foreach(var i in _raycasts)
            {
                ref var hit = ref _raycasts.Get1(i).Hit;

                if(_staticData.GroundLayers.Contains(hit.collider.gameObject.layer))
                {
                    var worldPos = hit.point;
                    player.Agent.SetDestination(worldPos);
                    return;
                }
            }

        }
    }
}