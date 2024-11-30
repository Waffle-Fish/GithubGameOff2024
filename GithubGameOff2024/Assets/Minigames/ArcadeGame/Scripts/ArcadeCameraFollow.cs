using UnityEngine;

namespace ArcadePlatformer
{
    public class ArcadeCameraFollow : MonoBehaviour
    {
        public Transform player;
        public Rigidbody2D playerRB;
        public float speed;
        public float maxOffset;
        public float velocityStrength;

        private void LateUpdate()
        {
            Vector3 offset = Mathf.Clamp(playerRB.linearVelocity.x * velocityStrength, -maxOffset, maxOffset) * Vector3.right;
            Vector3 newPos = Vector3.Lerp(transform.localPosition, player.localPosition + offset, Time.deltaTime * speed);
            newPos.z = -10;

            transform.localPosition = newPos;
        }
    }
}