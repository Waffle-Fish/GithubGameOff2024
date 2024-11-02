using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.iOS;


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

    [Tooltip("Current State of the player")]
    [SerializeField]
    public Actions CurrentAction;
    [Tooltip("Time it takes to cast.")]
    [SerializeField]
    private int CastingTime;
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
    private Vector3 TempHeight = new Vector3(0,20,0);
    private void Awake() {
        
        inputManager = InputManager.Instance;
    
    }
 
    private void Fishing(){
        bool IsFishing = InputManager.Instance.GetFishing();
        if (!IsFishing) 
        {
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
            Debug.Log("Did Hit"); 
        }
        else
        { 
            Debug.DrawRay(transform.position + CurrentCastingDistance + TempHeight, transform.TransformDirection(Vector3.down) * 1000, Color.red); 
            Debug.Log("Did not Hit"); 
        }
       
    }

    private void StartCasting(){
        CurrentCastingDistance = Vector3.zero;
        StartCoroutine(StartCastingTimer());
    }
    IEnumerator StartCastingTimer()
    {
        
        Debug.Log("Timer started");
        yield return new WaitForSeconds(CastingTime); // Wait for delay seconds
        Debug.Log("Timer ended after " + CastingTime + " seconds");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Fishing();
    }


}
