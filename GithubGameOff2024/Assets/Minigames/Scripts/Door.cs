using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    private MeshRenderer outsideObject;
    private Material[] outsideMaterials;
    private Color[] baseColors;
    private const float fadeInOutDuration = 0.3f;

    private void Awake()
    {
        outsideObject = GetComponent<MeshRenderer>();
        outsideMaterials = outsideObject.materials;
        baseColors = new Color[outsideMaterials.Length];
        for (int i = 0; i < outsideMaterials.Length; i++)
            baseColors[i] = outsideMaterials[i].GetColor("_BaseColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 31)
            return;

        StopAllCoroutines();
        for (int i = 0; i < outsideMaterials.Length; i++)
            StartCoroutine(LerpFunction(Color.clear, fadeInOutDuration, i));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 31)
            return;

        StopAllCoroutines();
        for (int i = 0; i < outsideMaterials.Length; i++)
            StartCoroutine(LerpFunction(baseColors[i], fadeInOutDuration, i));
    }

    IEnumerator LerpFunction(Color endValue, float duration, int i)
    {
        float time = 0;
        Color startValue = outsideMaterials[i].GetColor("_BaseColor");

        while (time < duration)
        {
            outsideMaterials[i].SetColor("_BaseColor", Color.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        outsideMaterials[i].SetColor("_BaseColor", endValue);
    }
}
