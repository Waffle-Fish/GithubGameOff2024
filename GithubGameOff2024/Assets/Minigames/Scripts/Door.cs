using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private MeshRenderer outsideObject;
    private Material[] outsideMaterials;
    private const float fadeInOutDuration = 0.3f;

    private void Awake()
    {
        outsideMaterials = outsideObject.materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 31)
            return;

        StopAllCoroutines();
        for (int i = 0; i < outsideMaterials.Length; i++)
            StartCoroutine(LerpFunction(0, fadeInOutDuration, i));
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 31)
            return;

        StopAllCoroutines();
        for (int i = 0; i < outsideMaterials.Length; i++)
            StartCoroutine(LerpFunction(1, fadeInOutDuration, i));
    }

    IEnumerator LerpFunction(float endValue, float duration, int i)
    {
        float time = 0;
        float startValue = outsideMaterials[i].GetFloat("_AlphaS");

        while (time < duration)
        {
            outsideMaterials[i].SetFloat("_AlphaS", Mathf.Lerp(startValue, endValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        outsideMaterials[i].SetFloat("_AlphaS", endValue);
    }
}
