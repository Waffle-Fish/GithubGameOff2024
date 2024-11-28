using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace ArcadePlatformer
{
    public class ArcadePlayer : MonoBehaviour
    {
        private Vector3 startPos;
        public ParticleSystem deathPS;

        private void Awake()
        {
            startPos = transform.position;
        }

        public void CollectFish()
        {

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