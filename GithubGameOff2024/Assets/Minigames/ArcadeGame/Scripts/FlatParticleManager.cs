using UnityEngine;

public class FlatParticleManager : MonoBehaviour
{
    public ParticleSystem[] particles;

    public void EmitParticles(int type, Vector3 position, int amount)
    {
        var shape = particles[type].shape;
        shape.position = position;

        var emitParams = new ParticleSystem.EmitParams();
        emitParams.applyShapeToPosition = true;

        var main = particles[type].main;

        particles[type].Emit(amount);
    }
}
