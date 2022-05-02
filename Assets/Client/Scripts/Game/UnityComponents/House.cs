using UnityEngine;

namespace Game
{
    public class House : MonoEntity
    {
        public Renderer RoofRenderer;
        public Collider Trigger;
        
        private void OnTriggerEnter(Collider collider)
        {
            var player = collider.GetComponent<Player>();

            if(player != default)
            {
                RoofRenderer.enabled = false;
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            var player = collider.GetComponent<Player>();

            if(player != default)
            {
                RoofRenderer.enabled = true;
            }
        }
    }
}