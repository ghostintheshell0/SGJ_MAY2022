using Leopotam.Ecs;

namespace Game
{
    public class PickBehaviour : CharacterBehaviour
    {
        public float PickDistance;
        public float PrePickDuration;
        public float PostPickDuration;


        protected override void OnInit()
        {
            ref var picker = ref Character.Entity.Get<PickerComponent>();
            picker.View = this;
        }
    }
}
