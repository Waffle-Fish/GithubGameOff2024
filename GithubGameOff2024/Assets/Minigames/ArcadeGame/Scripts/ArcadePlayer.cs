using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace ArcadePlatformer
{
    public class ArcadePlayer : MonoBehaviour
    {
        public ArcadeUI uiManager;
        public ParticleSystem deathPS;
        private Vector3 startPos;

        private void Awake()
        {
            startPos = transform.position;
        }

        public void CollectFish()
        {
            uiManager.CollectFish();
        }

        public void Checkpoint(Vector3 pos)
        {
            startPos = pos;
        }

        public void Die()
        {
            deathPS.Play();
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            StartCoroutine(death());
        }

        public IEnumerator death()
        {
            yield return new WaitForEndOfFrame();

            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            transform.position = startPos;
        }
    }
}