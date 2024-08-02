using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    
    [SerializeField] private Transform counterTopPoint;
    
    private KitchenObject kitchenObject;
    // Start is called before the first frame update
    public virtual void Interact(Player player){
        Debug.LogError("BaseCoutner.Interact()");
    }
    public virtual void InteractAlternate(Player player){
        Debug.LogError("BaseCoutner.InteractAlternate()");
    }
    public Transform GetKitchenObjectFollowTrasnform(){
        return counterTopPoint;
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
