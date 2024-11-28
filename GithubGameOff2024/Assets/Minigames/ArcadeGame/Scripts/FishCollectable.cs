using UnityEngine;

namespace ArcadePlatformer
{
    public class FishCollectable : MonoBehaviour
    {
        public FlatParticleManager particleManager;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 10)
            {
                particleManager.EmitParticles(0, transform.localPosition, 10);
                gameObject.SetActive(false);
            }
        }
    }
}