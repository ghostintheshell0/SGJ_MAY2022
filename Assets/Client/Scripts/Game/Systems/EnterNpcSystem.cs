using Leopotam.Ecs;

namespace Game
{
    public class EnterNpcSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnterNpcCommand> _filter = default;
        private readonly RuntimeData _runtimeData = default;
        private readonly StaticData _staticData = default;
        private readonly SceneData _sceneData = default;
        private readonly EcsWorld _world = default;
    
        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var cmd = ref _filter.Get1(i);
                var character = cmd.Npc.Character;

                ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();

                if(player.View.CurrentCrap != default)
                {
                    var crap = player.View.CurrentCrap;
                    player.View.CurrentCrap = default;
                    if(character.Entity.Has<SpeekerComponent>())
                    {
                        ref var speeker = ref character.Entity.Get<SpeekerComponent>();
                        speeker.View.SpeechBubble.SetActive(false);
                    }
                    
                    _runtimeData.Progress++;
                    character.AudioSource.Play();
                    player.View.Entity.Get<RespawnCrapCommand>();

                    if(_runtimeData.Progress == 1)
                    {
                        character.Entity.Get<NpcComponent>().View.ReadyForMove();
                        character.Animator.SetBool(AniamtionNames.Carrying, true);
                        character.Agent.SetDestination(cmd.Npc.PointForHideCrap.position);
                        crap.Collider.enabled = false;
                        crap.Obstacle.enabled = false;
                        crap.transform.position = character.HandPoint.position;
                        crap.transform.SetParent(character.HandPoint, true);
                        crap.transform.localPosition = UnityEngine.Vector3.zero;
                        crap.Entity.Del<CrapComponent>();
                    }
                    else
                    {
                        if(crap.Entity.IsAlive())
                        {
                            crap.Entity.Destroy();
                        }
                        
                        UnityEngine.Object.Destroy(crap.gameObject);
                    }
                   
                }
                
                if(_runtimeData.Progress < _staticData.AllCrap.Count)
                {
                    ShowBubble(character);
                    player.View.Entity.Get<UpdateProgressComponent>();
                }
                else
                {
                    var ent = _world.NewEntity();
                    _runtimeData.IsEnd = true;
                    ref var changeScene = ref ent.Get<ChangeSceneComponent>();
                    changeScene.NextSceneName = _staticData.MainScene;
                    changeScene.Player = player.View;
                    changeScene.MovePoint = player.View.transform;
                }
                
                _filter.GetEntity(i).Del<EnterNpcCommand>();
            }
        }

        private void ShowBubble(Character character)
        {
            if(!character.Entity.Has<SpeekerComponent>()) return;
            
            ref var speeker = ref character.Entity.Get<SpeekerComponent>();
            var currentCrap = _staticData.AllCrap[_runtimeData.Progress]; 
            speeker.View.SpeechBubble.SetActive(true);
            speeker.View.SpeechIcon.sprite = currentCrap.Icon;
            speeker.View.SpeechIcon.color = currentCrap.Color;
            ref var showBubble = ref character.Entity.Get<HideWithDelayComponent>();
            showBubble.Target = speeker.View.SpeechBubble;
            showBubble.Delay = speeker.View.ShowBubbleDuration;
        }
    }
}