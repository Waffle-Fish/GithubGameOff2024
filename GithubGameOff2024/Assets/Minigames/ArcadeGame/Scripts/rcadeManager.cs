using System.Collections;
using UnityEngine;

public class rcadeManager : MonoBehaviour
{
    public Camera cam;
    public Transform blackScreen;
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
        StartCoroutine(LerpFunction(0, turnOnSpeed, blackScreen.localScale.x, true));
    }

    public void TurnOff()
    {
        playerMovement.enabled = false;
        StopAllCoroutines();
        StartCoroutine(LerpFunction(1, turnOnSpeed, blackScreen.localScale.x, false));
    }

    IEnumerator LerpFunction(float endValue, float duration, float scaler, bool on)
    {
        if (on)
            cam.enabled = true;

        float time = 0;
        float startValue = scaler;
        Vector3 startScale = Vector3.one;

        while (time < duration)
        {
            scaler = Mathf.Lerp(startValue, endValue, time / duration);
            blackScreen.localScale = new Vector3(scaler, 1);
            time += Time.deltaTime;
            yield return null;
        }

        blackScreen.localScale = startScale * endValue;

        yield return new WaitForEndOfFrame();

        if (!on)
            cam.enabled = false;
    }
}
