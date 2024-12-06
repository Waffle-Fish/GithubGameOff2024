using UnityEngine;

namespace ArcadePlatformer
{
    public class Checkpoint : MonoBehaviour
    {
        public FlatParticleManager psManager;
        public FishSlapManager slapManager;
        private Animator animator;
        private bool activated;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Activate()
        {
            activated = true;
            animator.SetBool("Activated", true);
        }

        public void Deactivate()
        {
            activated = false;
            animator.SetBool("Activated", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!activated && collision.gameObject.layer == 10)
            {
                psManager.EmitParticles(1, transform.localPosition, 15);
                slapManager.CallOffCheckpoints();
                collision.gameObject.GetComponent<ArcadePlayer>().Checkpoint(transform.position);
                Activate();
            }
        }
    }
}