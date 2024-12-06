using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;


public class fishingManager : MonoBehaviour
{
   
 // Define an enum to represent directions
    public enum Actions
    {
        Idle,
        Casting,
        Fishing,
        DoneFishing
    }

    [Tooltip("Player")]
    [SerializeField]
    public GameObject Player;

    [Tooltip("Fishing Spot Object")]
    [SerializeField]
    public GameObject FishingIndicator;
    public GameObject FishingMiniGame;

    public GameObject CurrentFishingMiniGame;
    private GameObject CurrentFishIndicator;


    [Tooltip("Current State of the player")]
    [SerializeField]
    public Actions CurrentAction;
    [Tooltip("height of where fishing can take places")]
    [SerializeField]
    private int HeightOfWater;
    [SerializeField]

    [Tooltip("Distance of the cast.")]
    private int CasttingDistance;
    [SerializeField]
    [Tooltip("Speed of the cast / raycast")]
    private float CasttingSpeed;
    
    private InputManager inputManager;
    private RaycastHit CastingRayCast;
    
    private Vector3 CurrentCastingDistance;
    [SerializeField]
    private Vector3 TempHeight = new Vector3(0,0,0);
    private void Awake() {
        
        inputManager = InputManager.Instance;
    
    }
    


    private void Fishing(){

        if ( CurrentAction == Actions.Fishing){
            if (!CurrentFishingMiniGame)
            {
                CurrentAction = Actions.Idle;
            }
            return;
        }

         

        bool IsFishing = InputManager.Instance.GetFishing();
        if (!IsFishing) 
        {
            if ( CurrentAction == Actions.Casting){
                if ( CheckIfCanFish()){
                    StartFishingMiniGame();

                    DestroyImmediate(CurrentFishIndicator);
                    return;
                }
            }
            DestroyImmediate(CurrentFishIndicator);
            
            CurrentAction = Actions.Idle;
            return;
        }

    
           

  
    switch (CurrentAction)
        {
        case Actions.Idle:
            CurrentAction = Actions.Casting;
            StartCasting();
            break;

        case Actions.Casting:
            Casting();
            break;

        case Actions.Fishing:

     
            break;
        case Actions.DoneFishing:
            break;
     
        }


        
        Debug.Log("is fishing");


    }

    private void Casting(){

        float NewDistance = CasttingSpeed * Time.fixedDeltaTime;
        CurrentCastingDistance += transform.forward * NewDistance;
        if (Physics.Raycast(transform.position + CurrentCastingDistance  + TempHeight, transform.TransformDirection(Vector3.down), out  CastingRayCast, Mathf.Infinity))

        { 
            Debug.DrawRay(transform.position+ CurrentCastingDistance + TempHeight, transform.TransformDirection(Vector3.down) *  CastingRayCast.distance, Color.blue); 
           // Debug.Log("Did Hit"); 
           // Debug.Log(CastingRayCast.point); 
        }
      
       UpdateFishFishingIndicator(CastingRayCast.point);
    }
    private void StartCasting(){
        Destroy(CurrentFishIndicator);
        CurrentFishIndicator = Instantiate(FishingIndicator, Player.transform);
        CurrentFishIndicator.transform.localPosition = Player.transform.position;
        CurrentCastingDistance = Vector3.zero;
        
    }
    void Start()
    {
      
    }

    void UpdateFishFishingIndicator(Vector3 Target) {
        if (CurrentFishIndicator & Target != Vector3.zero) {

            CurrentFishIndicator.transform.position = Target;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckIfCanFish()
    {
        if ( CastingRayCast.point.y <= HeightOfWater ) return true;
        
        return false;
        

    }


    void StartFishingMiniGame(){
        CurrentFishingMiniGame = Instantiate(FishingMiniGame);
        CurrentAction = Actions.Fishing;
        Debug.Log("we are fishingggggggg!!!!!!!!!");
        
    }


    void FixedUpdate()
    {
        Fishing();
    }


}
