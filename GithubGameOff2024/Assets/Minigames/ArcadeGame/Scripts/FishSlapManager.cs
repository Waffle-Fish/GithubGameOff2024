using UnityEngine;

namespace ArcadePlatformer
{
    public class FishSlapManager : ArcadeSceneHandler
    {
        public int score;
        public ArcadePlayerMovement playerMovement;
        public ArcadeUI uiManager;
        public FishCollectable[] collectables;
        public Checkpoint[] checkpoints;

        private Vector3 startingPlayerPos;

        public void Awake()
        {
            startingPlayerPos = playerMovement.transform.position;
        }

        private void Start()
        {
            checkpoints = FindObjectsByType<Checkpoint>(FindObjectsSortMode.None);
            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].slapManager = this;
            }
        }

        public override void TurnOn()
        {
            playerMovement.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            playerMovement.transform.position = startingPlayerPos;
            playerMovement.enabled = true;

            CallOffCheckpoints();

            uiManager.ResetData();
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

            CallOffCheckpoints();

            uiManager.ResetData();
        }

        public void CallOffCheckpoints()
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                checkpoints[i].Deactivate();
            }
        }
    }
}