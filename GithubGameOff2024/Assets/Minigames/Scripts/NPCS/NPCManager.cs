using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject[] npcInteractables;
    private IInteractableNPC[] interactables;

    public static NPCManager Instance { get; private set; }

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

    private void Start()
    {
        interactables = new IInteractableNPC[npcInteractables.Length];
        for (int i = 0; i < npcInteractables.Length; i++)
        {
            interactables[i] = npcInteractables[i].GetComponent<IInteractableNPC>();
        }
    }

    public IInteractableNPC GetRandomActivity()
    {
        for (int i = 0; i < 100; i++)
        {
            int interactableIndex = Random.Range(0, interactables.Length);
            if (!interactables[interactableIndex].Taken())
                return interactables[interactableIndex];
        }

        return null;
    }
}
