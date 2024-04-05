using UnityEngine;

namespace BuilderStory
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