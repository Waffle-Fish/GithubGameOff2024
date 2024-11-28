using UnityEngine;

namespace ArcadePlatformer
{
    public class FishSlapManager : ArcadeSceneHandler
    {
        public int score;
        public ArcadePlayerMovement playerMovement;
        public FishCollectable[] collectables;

        private Vector3 startingPlayerPos;

        public void Awake()
        {
            startingPlayerPos = playerMovement.transform.position;
        }

        public override void TurnOn()
        {
            playerMovement.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            playerMovement.transform.position = startingPlayerPos;
            playerMovement.enabled = true;
        }

        public override void TurnOff()
        {
            playerMovement.enabled = false;
            playerMovement.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            playerMovement.transform.position = startingPlayerPos;

            for (int i = 0; i < collectables.Length; i++)
            {
                collectables[i].gameObject.SetActive(true);
            }
        }
    }
}