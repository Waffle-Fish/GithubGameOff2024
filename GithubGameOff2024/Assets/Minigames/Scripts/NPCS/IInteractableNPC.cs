using UnityEngine;

public interface IInteractableNPC
{
    Vector2 _timeMinMax { get; set; }

    bool NPCInteract();
    Vector3 GetPosition();
    bool Taken();
}
