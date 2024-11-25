using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeBackgroundScroller : MonoBehaviour
{
    public float speed = 0.035f;
    public float xStrength = 0.032f; // 0.032 is perfect offset when moving camera.  > 0.032 is high movement.  < 0.032 is low movement.  0 is no movement.
    public float yStrength = 0.045711f;
    public Color color;
    public Sprite sprite;
    public Vector2 extraOffset;

    //private Vector2 randomOffset;
    public Transform positionP;
    private Vector2 startPos;
    private Material texture;

    // Start is called before the first frame update
    void Start()
    {
        texture = GetComponent<MeshRenderer>().material;
        texture.color = this.color;
        texture.mainTexture = sprite.texture;

        //randomOffset = new Vector2(Random.Range(-20, 20), 0);
        //if (Mathf.Approximately(speed, 0))
        //    randomOffset = Vector2.zero;

        if (Mathf.Approximately(speed, 0))
            startPos = positionP.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * speed, 0);

        //offset.x += (position.position.x + randomOffset.x - startPos.x) * xStrength;
        //offset.y += (position.position.y + randomOffset.y - startPos.y) * yStrength;

        offset.x += (positionP.position.x - startPos.x) * xStrength;
        offset.y += (positionP.position.y - startPos.y) * yStrength;

        texture.mainTextureOffset = offset + extraOffset;
    }
}
