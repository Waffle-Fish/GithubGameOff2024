using UnityEngine;

namespace ArcadePlatformer
{
    public class Spikes : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 10)
            {
                collision.gameObject.GetComponent<ArcadePlayer>().Die();
            }
        }
    }
}