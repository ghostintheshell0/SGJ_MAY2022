using UnityEngine;

namespace Game
{
    public class Tile : MonoEntity
    {
        public Material DefaultMateral;
        public Material EnabledMaterial;
        public Renderer Renderer;

        private void OnTriggerEnter(Collider collider)
        {
            Renderer.material = EnabledMaterial;
        }

        private void OnTriggerExit(Collider collider)
        {
            Renderer.material = DefaultMateral;
        }
    }

}