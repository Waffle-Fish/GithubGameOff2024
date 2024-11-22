using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AreaMusicTrigger : MonoBehaviour
{
    [SerializeField] private AreaType area = AreaType.Village;

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.SetAreaParameter(area);
    }
}
