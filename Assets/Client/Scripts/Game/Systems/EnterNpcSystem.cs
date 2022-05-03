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

                ref var player = ref cmd.Player.Entity.Get<PlayerComponent>();
                if(player.View.CurrentCrap != default)
                {
                    var crap = player.View.CurrentCrap;
                    player.View.CurrentCrap = default;
                    cmd.Npc.SpeechBubble.SetActive(false);
                    _runtimeData.Progress++;
                    cmd.Npc.AudioSource.Play();
                    player.View.Entity.Get<RespawnCrapCommand>();

                    if(_runtimeData.Progress == 1)
                    {
                        _sceneData.House.IsLocked = false;
                        cmd.Npc.ReadyForMove();
                        cmd.Npc.Animator.SetBool(AniamtionNames.Carrying, true);
                        cmd.Npc.Agent.SetDestination(_sceneData.House.HousePoint.position);
                        crap.Collider.enabled = false;
                        crap.Obstacle.enabled = false;
                        crap.transform.position = cmd.Npc.Hand.position;
                        crap.transform.SetParent(cmd.Npc.Hand, true);
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
                    ShowBubble(cmd.Npc);
                    player.View.Entity.Get<UpdateProgressComponent>();
                }
                else
                {
                    var ent = _world.NewEntity();
                    _runtimeData.IsEnd = true;
                    ref var changeScene = ref ent.Get<ChangeSceneComponent>();
                    changeScene.SceneName = _staticData.MainScene;
                    changeScene.Player = player.View;
                    changeScene.MovePoint = player.View.transform;
                }
                
                _filter.GetEntity(i).Del<EnterNpcCommand>();
            }
        }

        private void ShowBubble(Npc npc)
        {
            var currentCrap = _staticData.AllCrap[_runtimeData.Progress]; 
            npc.SpeechBubble.SetActive(true);
            npc.SpeechIcon.sprite = currentCrap.Icon;
            npc.SpeechIcon.color = currentCrap.Color;
            ref var showBubble = ref npc.Entity.Get<HideWithDelayComponent>();
            showBubble.Target = npc.SpeechBubble;
            showBubble.Delay = npc.ShowBubbleDuration;
        }
    }
}