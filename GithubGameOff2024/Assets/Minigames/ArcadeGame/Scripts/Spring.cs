using UnityEngine;

namespace ArcadePlatformer
{
    public class Spring : MonoBehaviour
    {
        [SerializeField] private float springPower = 20;
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 10)
            {
                anim.SetTrigger("Spring");
                collision.gameObject.GetComponent<ArcadePlayerMovement>().SpringJump(springPower);
            }
        }
    }
}