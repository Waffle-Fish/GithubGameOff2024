using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject bar;
    public Image Self;
    public Blocker barScript;

    public GameObject Owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    { 
        Collider2D Coll = GetComponent<BoxCollider2D>();
        Coll.offset =new Vector2(0,Self.rectTransform.localPosition.y) ;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {

        GameObject hit = other.gameObject;
        Debug.Log("Trigger");
        if(hit.CompareTag("Bar"))
        {
            bar = hit;
            barScript = bar.GetComponent<Blocker>();
            Debug.Log(barScript.clicks);
            Owner.GetComponent<FishingMiniGame>().CurrentBlocker = hit;
        }
    }
    
}
