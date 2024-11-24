using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class rcadeManager : MonoBehaviour
{
    public Camera cam;
    public Image blackScreen;
    public Image actualBlackScreen;
    public float turnOnSpeed;
    public ArcadePlayerMovement playerMovement;
    public bool playerOperated;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOn(bool isPlayer)
    {
        playerMovement.enabled = true;
        playerOperated = isPlayer;
        StopAllCoroutines();
        StartCoroutine(LerpFunction(0, turnOnSpeed, true));
    }

    public void TurnOff()
    {
        playerMovement.enabled = false;
        StopAllCoroutines();
        StartCoroutine(LerpFunction(1, 0.1f, false));
    }

    IEnumerator LerpFunction(float endValue, float duration, bool on)
    {
        float time = 0;
        float startValue = blackScreen.color.a;

        if (on)
            cam.enabled = true;

        actualBlackScreen.color = on ? Color.clear : Color.black;

        while (time < duration)
        {
            blackScreen.color = new Color(1, 1, 1, Mathf.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = Vector4.one * endValue;

        yield return new WaitForEndOfFrame();

        if (!on)
            cam.enabled = false;
    }
}
