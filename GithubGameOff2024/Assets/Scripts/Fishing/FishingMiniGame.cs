using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGame : MonoBehaviour
{
      public enum Actions
    {
        Blocked,
        Fishing,
    }

    private Actions CurrentActions;
    public GameObject MainProgressBar;

    [SerializeField]
    public Image ProgressBar;

    [Tooltip("Block to stop Progress")]
    [SerializeField]
    public GameObject BlockerBar;
    [SerializeField]
    public Vector2 FishRate;
    public bool DoneFishing;
    
    [SerializeField]
    public Vector2 ProgressStart = new Vector2(0,0); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void StartMiniGame(float WeightOfObject)
    {
      Vector2 HeightOfProgressbar =  ProgressBar.rectTransform.sizeDelta;
      Vector2 ProgressbarPosition = ProgressBar.rectTransform.position;
      float RandomSpawn = Random.Range(-HeightOfProgressbar.y/2,HeightOfProgressbar.y/2);
      UnityEngine.Debug.Log(RandomSpawn);
      Quaternion spawnRotation = Quaternion.identity; 
      Vector3 BlockLocation = new Vector3(0,RandomSpawn,0);
      GameObject Bar =  Instantiate(BlockerBar);
      Bar.transform.SetParent(MainProgressBar.transform, true);
      Bar.transform.localPosition = BlockLocation;
     
    }
    void FixedUpdate()
    {
      if (DoneFishing) return;

      if (Input.GetMouseButtonDown(0))
    {
      UnityEngine.Debug.Log("click");
      return ;
    }
      if (Input.GetMouseButton(0))
    {
      UnityEngine.Debug.Log("held");
      ProgressBar.rectTransform.anchorMax += FishRate;

      if ( ProgressBar.rectTransform.anchorMax.y >= .5 ){
        DoneFishing = true;
        
    }

    } 

    }

    void Start()
    {
      ProgressBar.rectTransform.anchorMax = ProgressStart;
      StartMiniGame(10.0f);

    }


}
