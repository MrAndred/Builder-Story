using BuilderStory.PlayerSystem;
using UnityEngine;

namespace BuilderStory.Respawn
{
    public class RespawnZone : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) == true)
            {
                player.Respawn();
            }
        }
    }
}
