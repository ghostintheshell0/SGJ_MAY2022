using Leopotam.Ecs;
using UnityEngine;
using Extensions;

namespace Game
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly SceneData _sceneData = default;
        private readonly StaticData _staticData = default;
        private readonly EcsFilter<RaycastComponent> _raycasts = default;
        private readonly EcsFilter<LockInputComponent> _locks = default;
        private readonly EcsFilter<PlayerComponent> _players = default;

        private RaycastHit[] hits;
    
        public void Init()
        {
            hits = new RaycastHit[5];
        }

        public void Run()
        {
            if(!_locks.IsEmpty()) return;
            
            if(Input.GetMouseButton(0))
            {
                if(TryGetInteractiveObject(out var interactiveObject, out var hit))
                {
                    if(TryPickNearCrap())
                    {
                        return;
                    }
                }

                MoveTo(_sceneData.Player);
               
            }

            var v = Input.GetAxis("Vertical");
            var h = Input.GetAxis("Horizontal");

            var s = new Vector3(h, 0, v);
            if(s.sqrMagnitude > 0)
            {
                var inputDir = _sceneData.Player.transform.position + s;
                var cameraDir = _sceneData.MainCamera.transform.TransformDirection(s);
                _sceneData.Player.Agent.SetDestination(_sceneData.Player.transform.position + cameraDir*2);
            }

            if(Input.GetAxis("Fire1") != 0)
            {
                TryPickNearCrap();
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

        private bool TryPickNearCrap()
        {
            foreach(var i in _players)
            {
                ref var player = ref _players.Get1(i).View;
                var pickBehaviour = player.GetBehaviour<PickBehaviour>();

                var x = Physics.SphereCastNonAlloc(player.transform.position, pickBehaviour.PickDistance, player.transform.forward, hits, pickBehaviour.PickDistance, _staticData.InteractiveObjectsLayer);
                
                for(var k = 0; k < x; k++)
                {
                    var monoEntity = hits[k].transform.GetComponent<MonoEntity>();

                    if(monoEntity.Entity.Has<CrapComponent>())
                    {
                        Pick(player, monoEntity);
                        return true;
                    }
                }
            }

            return false;
        }

        private void Pick(Character pickerCharacter, MonoEntity crap)
        {
            ref var picker = ref pickerCharacter.Entity.Get<PickerComponent>();

            ref var cmd = ref pickerCharacter.Entity.Get<PickCommand>();
            ref var crapComp = ref crap.Entity.Get<CrapComponent>();
            cmd.Picker = picker.View;
            cmd.Crap = crapComp.View;
        }

        private void MoveTo(Character player)
        {
            if(player.Agent.enabled == false)
            {
                return;
            }

            RaycastHit nearestHit = default;
            var nearestDistance = float.MaxValue;

            foreach(var i in _raycasts)
            {
                ref var hit = ref _raycasts.Get1(i).Hit;

                if(_staticData.GroundLayers.Contains(hit.collider.gameObject.layer))
                {
                    var distance = (_sceneData.MainCamera.transform.position - hit.point).sqrMagnitude;
                    if(distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestHit = hit;
                    }
                }
            }

            var worldPos = nearestHit.point;
            player.Agent.SetDestination(worldPos);

        }
    }
}