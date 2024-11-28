using UnityEngine;
using TMPro;

namespace ArcadePlatformer
{
    public class ArcadeUI : MonoBehaviour
    {
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI fishText;

        private int score;
        private float timer;

        private void Start()
        {
            ResetData();
        }

        public void Update()
        {
            TrackTime();
        }

        public void ResetData()
        {
            timer = 0;
            score = 0;

            timerText.text = "00:00:00";
            fishText.text = string.Format("{0:00}", score);
        }

        public void CollectFish()
        {
            score++;
            fishText.text = string.Format("{0:00}", score);
        }

        private void TrackTime()
        {
            timer += Time.deltaTime;
            timerText.text = FormatTime(timer);
        }

        public static string FormatTime(float time)
        {
            if (time == 0)
                return "N/A";

            int intTime = (int)time;
            int minutes = intTime / 60;
            int seconds = intTime % 60;
            float fraction = time * 1000;
            fraction = fraction % 1000;
            fraction = fraction % 1000 / 10;

            string timeText = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);

            return timeText;
        }
    }
}