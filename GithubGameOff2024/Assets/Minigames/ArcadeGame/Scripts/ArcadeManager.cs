using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ArcadePlatformer;

public class ArcadeManager : MonoBehaviour
{
    public Camera cam;
    public Image blackScreen;
    public Image actualBlackScreen;
    public float turnOnSpeed;
    public ArcadeSceneHandler sceneHandler;
    public bool playerOperated;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOn(bool isPlayer)
    {
        playerOperated = isPlayer;
        StopAllCoroutines();
        StartCoroutine(LerpFunction(0, turnOnSpeed, true));
    }

    public void TurnOff()
    {
        StopAllCoroutines();
        StartCoroutine(LerpFunction(1, 0.25f, false));
    }

    IEnumerator LerpFunction(float endValue, float duration, bool on)
    {
        float time = 0;
        float startValue = blackScreen.color.a;

        if (on)
            cam.enabled = true;
        else
        {
            actualBlackScreen.color = Color.black;
            sceneHandler.TurnOff();
        }

        if (on)
            actualBlackScreen.transform.SetAsFirstSibling();
        //else
        //    actualBlackScreen.transform.SetAsLastSibling();

        //actualBlackScreen.color = on ? Color.clear : Color.black;

        if (on)
        {
            float secretStartValue = 0, secretEndValue = 1;
            while (time < duration)
            {
                blackScreen.color = new Color(1, 1, 1, Mathf.Lerp(secretStartValue, secretEndValue, fadeCurve.Evaluate(time / duration)));
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
        }

        while (time < duration)
        {
            blackScreen.color = new Color(1, 1, 1, Mathf.Lerp(startValue, endValue, fadeCurve.Evaluate(time / duration)));
            time += Time.deltaTime;
            yield return null;
        }
        blackScreen.color = Vector4.one * endValue;

        yield return new WaitForEndOfFrame();

        if (!on)
        {
            actualBlackScreen.transform.SetAsLastSibling();

            yield return new WaitForEndOfFrame();

            cam.enabled = false;
        }
        else
        {
            actualBlackScreen.color = Color.clear;
            sceneHandler.TurnOn();
        }
    }
}