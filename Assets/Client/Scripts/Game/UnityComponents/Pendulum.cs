using System.Collections;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class Pendulum : MonoEntity
    {
        private Coroutine process;
        public float StunDuration;
        public float Force;

        private void OnCollisionEnter(Collision collision)
        {
            if(process != default) return;

            var player = collision.collider.GetComponent<Character>();
            if(player != default)
            {
                process = StartCoroutine(Process(player, collision.relativeVelocity.normalized));
            }
        }

        private IEnumerator Process(Character character, Vector3 directoin)
        {
            character.Agent.enabled = false;
            character.Body.isKinematic = false;
            yield return default;
            character.Body.AddForce(directoin * Force);
            character.Entity.Get<LockInputComponent>();
            yield return new WaitForSeconds(StunDuration);
            character.Entity.Del<LockInputComponent>();
            character.Agent.enabled = true;
            character.Body.isKinematic = true;
            process = default;
        }
    }

}