using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SlotMachine : MonoBehaviour, IInteractable, IInteractableNPC
{
    public Cinemachine.CinemachineVirtualCamera activityCam;
    public Vector3 standOffset;
    public bool beingInteracted;
    public bool isSpinning;
    public bool canSpin;
    public Vector2 spinTimeMinMax;
    public float spinAmount;
    public float[] spinTimer;
    public Transform leverArm;
    public AnimationCurve correctionCurve;
    public AnimationCurve leverCurve;
    public int[] valueTypes;
    public float[] rotateAmounts;
    public bool[] stopped;

    public Transform[] slotWheels;
    public UnityEvent winEffects;
    public UnityEvent loseEffects;
    private bool npcInteractedWith;

    public Vector2 _timeMinMax { get { return new Vector2(5f, 5.5f); } set { } }

    private void Start()
    {
        NPCInteract();
    }

    public GameObject Interact()
    {
        if (npcInteractedWith)
            return null;

        npcInteractedWith = false;
        activityCam.Priority = activityCam.Priority == 30 ? 0 : 30;
        beingInteracted = !beingInteracted;

        if(isSpinning)
        {
            for (int i = 0; i < stopped.Length; i++)
            {
                stopped[i] = true;
            }
        }

        return gameObject;
    }

    public bool Taken()
    {
        return beingInteracted || npcInteractedWith;
    }

    public bool NPCInteract()
    {
        if(canSpin)
        {
            npcInteractedWith = true;
            Spin();
        }
        return false;
    }

    public void EndNPCInteract() { }

    public Vector3 GetPosition()
    {
        return transform.position + standOffset;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + standOffset, 0.5f);
    }

    private void Update()
    {
        if(beingInteracted && InputManager.Instance.WasSpacePressed())
        {
            if(canSpin)
            {
                Spin();
            }else if(!npcInteractedWith)
            {
                for (int i = 0; i < stopped.Length; i++)
                {
                    if (!stopped[i])
                    {
                        stopped[i] = true;
                        //spinTimer[i] = 0.1f;
                        break;
                    }
                }
            }
        }

        if(isSpinning)
        {
            isSpinning = false;
            for (int i = 0; i < spinTimer.Length; i++)
            {
                if(spinTimer[i] > 0)
                {
                    slotWheels[i].Rotate(Vector3.up, spinAmount * spinTimer[i] * Time.deltaTime, Space.Self);
                    rotateAmounts[i] += spinAmount * spinTimer[i] * Time.deltaTime;
                    isSpinning = true;
                }

                if(stopped[i] || npcInteractedWith)
                    spinTimer[i] -= Time.deltaTime * (npcInteractedWith ? 1 : 10);
            }

            if(!isSpinning)
            {
                StartCoroutine(ClockWheels(0.3f));
            }
        }
    }

    public void Spin()
    {
        isSpinning = true;
        canSpin = false;
        StartCoroutine(LerpAndBack(Quaternion.Euler(new Vector3(64, -90, -90)), 0.25f));
        for (int i = 0; i < spinTimer.Length; i++)
        {
            spinTimer[i] = Random.Range(spinTimeMinMax.x, spinTimeMinMax.y);
            stopped[i] = false;
            //rotateAmounts[i] = 0;
        }
    }

    IEnumerator ClockWheels(float duration)
    {
        yield return new WaitForSeconds(0.2f);

        float time = 0;

        Quaternion[] startValues = new Quaternion[3];
        Quaternion[] endMoves = new Quaternion[3];
        for (int i = 0; i < slotWheels.Length; i++)
        {
            startValues[i] = slotWheels[i].localRotation;
            valueTypes[i] = Mathf.RoundToInt(rotateAmounts[i] / 45 % 8);
            spinTimer[i] = valueTypes[i] * 45;
            endMoves[i] = Quaternion.Euler(valueTypes[i] * 45, -90, -90);
        }

        while (time < duration)
        {
            for (int i = 0; i < slotWheels.Length; i++)
            {
                slotWheels[i].localRotation = Quaternion.LerpUnclamped(startValues[i], endMoves[i], correctionCurve.Evaluate(time / duration));
                time += Time.deltaTime;
                yield return null;
            }
        }

        for (int i = 0; i < slotWheels.Length; i++)
        {
            slotWheels[i].localRotation = endMoves[i];
            rotateAmounts[i] = valueTypes[i] * 45;
            //valueTypes[i] = Mathf.RoundToInt(slotWheels[i].localRotation.eulerAngles.x / 45);
        }

        if(valueTypes[0] == valueTypes[1] && valueTypes[1] == valueTypes[2])
        {
            if(valueTypes[0] == 6) // Jackpot
            {

            }else
            {

            }

            winEffects.Invoke();
        }
        else
        {
            loseEffects.Invoke();
        }

        canSpin = true;
        npcInteractedWith = false;
    }

    IEnumerator LerpAndBack(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = leverArm.localRotation;

        while (time < duration)
        {
            leverArm.localRotation = Quaternion.LerpUnclamped(startValue, endValue, leverCurve.Evaluate(time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        leverArm.localRotation = endValue;


        time = 0;
        endValue = startValue;
        startValue = leverArm.localRotation;

        while (time < duration)
        {
            leverArm.localRotation = Quaternion.LerpUnclamped(startValue, endValue, leverCurve.Evaluate(time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        leverArm.localRotation = endValue;
    }
}
