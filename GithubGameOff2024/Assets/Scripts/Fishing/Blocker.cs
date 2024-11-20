using UnityEngine;

public class Blocker : MonoBehaviour
{
    public int clicks = 5;
    public GameObject Owner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Click(){
        clicks -=1;
        Debug.Log(clicks);
        if ( clicks <=0){

            DestroyImmediate(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
