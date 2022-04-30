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
    
        public void Run()
        {
            if(Input.GetMouseButton(0))
            {
                if(TryGetInteractiveObject(out var interactiveObject, out var hit))
                {
                    var player = _sceneData.Player;
                    if(interactiveObject.Entity.Has<CrapComponent>())
                    {
                        if(player.transform.IsNear(hit.point, player.PickDistance))
                        {
                            ref var cmd = ref player.Entity.Get<PickCommand>();
                            ref var crapComp = ref interactiveObject.Entity.Get<CrapComponent>();
                            cmd.Picker = player;
                            cmd.Crap = crapComp.View;
                            return;
                        }
                    //    else
                    //    {
                    //        Debug.Log((player.transform.position - hit.point).magnitude);
                    //    }
                    }
                }

                MoveTo();
               
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

        private void MoveTo()
        {
            foreach(var i in _raycasts)
            {
                ref var hit = ref _raycasts.Get1(i).Hit;

                if(_staticData.GroundLayers.Contains(hit.collider.gameObject.layer))
                {
                    var worldPos = hit.point;
                    _sceneData.Player.MoveTarget.position = worldPos;
                    _sceneData.Player.Agent.SetDestination(worldPos);
                    return;
                }
            }

        }
    }
}