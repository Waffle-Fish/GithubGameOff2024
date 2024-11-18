using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FishingMiniGame : MonoBehaviour
{
   

    public Dictionary<int, float> rarityTable = new()
    {
      {0,.85f},
      {1,.10f},
      {2,.05f},
    };
    public float timerDuration = 2f;


    public FishInstance CurrentFish;
    public GameObject MainProgressBar;
    public Image ProgressBar;
    public Image RedBar;

    private bool RedBarCanProgress;
    public GameObject BlockerBar;
    public GameObject CurrentBlocker;
    public Vector2 FishRate;
    public bool DoneFishing;
    
    public Vector2 ProgressStart = new Vector2(0,0); 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void StartMiniGame(  FishInstance item)
    { 
      
      float value = item.value;
      int rarity = (int)item.rarity;
      
      foreach (var x in new RangeInt[rarity + UnityEngine.Random.Range(1, 3) ])
      {
      Vector2 HeightOfProgressbar =  ProgressBar.rectTransform.sizeDelta;
      Vector2 ProgressbarPosition = ProgressBar.rectTransform.position;
      float RandomSpawn = UnityEngine.Random.Range(-HeightOfProgressbar.y/2,HeightOfProgressbar.y/2);
      UnityEngine.Debug.Log(RandomSpawn);
      Quaternion spawnRotation = Quaternion.identity; 
      Vector3 BlockLocation = new Vector3(0,RandomSpawn,0);
      GameObject Bar =  Instantiate(BlockerBar,MainProgressBar.transform);
      Bar.transform.GetChild(0).GetComponent<Blocker>().clicks = Mathf.CeilToInt(value) + UnityEngine.Random.Range(-rarity*3, 3);
      //Bar.transform.SetParent(MainProgressBar.transform, true);
      Bar.transform.localPosition = BlockLocation;
      }
      
     
    }
  void FixedUpdate()
    {

  
      if (DoneFishing) return;
      if ( RedBarCanProgress){
        RedBar.rectTransform.anchorMax += FishRate * new Vector2(0.0f,0.2f);
        if (RedBar.rectTransform.anchorMax.y >= ProgressBar.rectTransform.anchorMax.y)
        {
          DoneFishing = true;
          EndMiniGame(false);
        }
      } 
      if (Input.GetMouseButtonDown(0))
      {
        UnityEngine.Debug.Log("click");
        if (CurrentBlocker){
          CurrentBlocker.GetComponent<Blocker>().Click();
        }
        return ;
      }

      if (CurrentBlocker)return;
      if (Input.GetMouseButton(0))
      {
        UnityEngine.Debug.Log("held");
        ProgressBar.rectTransform.anchorMax += FishRate;

        if ( ProgressBar.rectTransform.anchorMax.y >= .5 )
        {
          EndMiniGame(true);
          DoneFishing = true;
        } 
      } 

    }




  void Start()
    {
      StartCoroutine(TimerCoroutine());
      ProgressBar.rectTransform.anchorMax = ProgressStart;
      RedBar.rectTransform.anchorMax = ProgressStart;
      float rarityValue = UnityEngine.Random.Range(0, 201)/200f;
      UnityEngine.Debug.Log(rarityValue);
      InventoryItem TempItem =new InventoryItem();
      if (rarityValue <= rarityTable[0]){
         UnityEngine.Debug.Log("Common");
        TempItem.rarity = InventoryItemRarity.Common;
        
      }
      else if( rarityValue <= rarityTable[0] +rarityTable[1] )
      {
         UnityEngine.Debug.Log("Uncommon");
         TempItem.rarity = InventoryItemRarity.Uncommon;
        
      }
      else
      {
          UnityEngine.Debug.Log("rare");
         TempItem.rarity = InventoryItemRarity.Rare;
      }

      UnityEngine.Debug.Log(TempItem.rarity);
      CurrentFish = new FishInstance(TempItem);
      CurrentFish.rarity = TempItem.rarity;
      UnityEngine.Debug.Log(CurrentFish.rarity);
      StartMiniGame(CurrentFish);

    }
  private IEnumerator TimerCoroutine()
    {
        float timeRemaining = timerDuration;

        while (timeRemaining > 0)
        {
           UnityEngine.Debug.Log($"Time remaining: {timeRemaining:F2} seconds");
            yield return new WaitForSeconds(1f); // Wait for 1 second
            timeRemaining -= 1f;
        }

       UnityEngine. Debug.Log("Timer ended!");
       RedBarCanProgress = true;
    }



  void EndMiniGame(bool DidWinMiniGame = false)
  {
    if (!DidWinMiniGame) Destroy(gameObject);

    UnityEngine.Debug.Log("You Won!!");

    /// add fish to inventory



  }



  


}
