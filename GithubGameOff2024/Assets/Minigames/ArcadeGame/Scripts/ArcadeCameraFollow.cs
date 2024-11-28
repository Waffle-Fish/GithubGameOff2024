using UnityEngine;

namespace ArcadePlatformer
{
    public class ArcadeCameraFollow : MonoBehaviour
    {
        public Transform player;
        public float speed;

        private void LateUpdate()
        {
            Vector3 newPos = Vector3.Lerp(transform.localPosition, player.localPosition, Time.deltaTime * speed);
            newPos.z = -10;

            transform.localPosition = newPos;
        }
    }
}