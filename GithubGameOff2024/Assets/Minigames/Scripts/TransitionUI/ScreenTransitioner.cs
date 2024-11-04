using UnityEngine;

public class ScreenTransitioner : MonoBehaviour
{
    private Animator animator;

    public static ScreenTransitioner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void TransitionIn()
    {
        animator.Play("CircleWipeOut");
    }

    public void TransitionOut()
    {
        animator.Play("CircleWipeIn");
    }
}
