using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    // Start is called before the first frame update

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;


    }
    [SerializeField] private int moveSpeed = 0;
    [SerializeField] private InputManager inputManger;
    [SerializeField] private LayerMask counterLayer;
    private KitchenObject kitchenObject;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private Vector3 lastInteractionDir;
    private bool isWalking;
    private BaseCounter selectedCounter;

    private void Awake(){
        if(Instance != null){
            Debug.LogError("More than once player instance found");
        }
        Instance = this;
    }

    private void Start(){
        inputManger.OnInteractAction += InputManager_OnInteractAction;
        inputManger.OnInteractAlternativeAction += InputManager_OnInteractAlternativeAction;
    }
    private void InputManager_OnInteractAlternativeAction(object sender, System.EventArgs e){
        if(selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
       
    }
    
    private void InputManager_OnInteractAction(object sender, System.EventArgs e){
        if(selectedCounter != null){
            selectedCounter.Interact(this);
        }
       
    }
    
    private void Update(){
        HandleMovement();
        HandleInteractions();

    }
    public bool GetIsWalking(){
        return isWalking;

    }
    private void HandleMovement(){
          Vector2 inputVector = inputManger.GetInputVectorNomralized();
        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);
        Vector3 moveDis = transform.position + moveDir;
        float playerRadius = 0.6f;
        float playerHeight = 2f;
        float moveDistance = moveSpeed*Time.deltaTime;

        bool canMove  = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight,playerRadius, moveDir, moveDistance);

        if(!canMove){
            //Attempt to move in X
            Vector3 moveDirX = new Vector3(moveDir.x,0,0);
            canMove  = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight,playerRadius, moveDirX, moveDistance);
            if(canMove){
                moveDir = moveDirX;
            }
            else{
                // cannot move on X so attempt Z
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z);
                canMove  = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up*playerHeight,playerRadius, moveDirZ, moveDistance);
                if(canMove){
                    moveDir = moveDirZ;
                }

            }



        }
        
        if(canMove){
            transform.position += moveDir*moveDistance;
        }
        


        if(moveDir != Vector3.zero){
            isWalking = true;
        }
        else{
            isWalking = false;
        }

        
        float rotateSpeed = 10f;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime*rotateSpeed);
    }
    private void HandleInteractions(){
        
        Vector2 inputVector = inputManger.GetInputVectorNomralized();
        Vector3 moveDir = new Vector3(inputVector.x,0,inputVector.y);
        if(moveDir != Vector3.zero){
            lastInteractionDir = moveDir;

        }
        float interactionDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractionDir,  out RaycastHit raycastHit, interactionDistance, counterLayer)){
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)){
                //clearCounter.Interact();
                if(selectedCounter != baseCounter){
                    SetSelectedCounter(baseCounter);
                }   
            }
            else{
                SetSelectedCounter(null);
            }          
        }
        else{
                SetSelectedCounter(null);
        }
        
    }
    public void SetSelectedCounter(BaseCounter clearCounter){
        this.selectedCounter = clearCounter;
        if(OnSelectedCounterChanged == null){Debug.Log("slected counter isnull");};
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounter = selectedCounter
            
        });

    }

     public Transform GetKitchenObjectFollowTrasnform(){
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObejct(KitchenObject kitchenObject){
        this.kitchenObject=kitchenObject;
    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }   
    public void ClearKitchenObject(){
        this.kitchenObject = null;
    }
    public bool HasKitchenObject(){
        if(this.kitchenObject != null){
            return true;
        }
        else{
            return false;
        }
    }
}

