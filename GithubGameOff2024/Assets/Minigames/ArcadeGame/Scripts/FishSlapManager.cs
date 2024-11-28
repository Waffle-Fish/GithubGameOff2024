using UnityEngine;

namespace ArcadePlatformer
{
    public class FishSlapManager : ArcadeSceneHandler
    {
        public int score;
        public ArcadePlayerMovement playerMovement;
        public FishCollectable[] collectables;

        public override void TurnOn()
        {
            playerMovement.enabled = true;
        }

        public override void TurnOff()
        {
            playerMovement.enabled = false;
        }
    }
}