using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent 
{
     public Transform GetKitchenObjectFollowTrasnform();
   public void SetKitchenObejct(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();  
    public void ClearKitchenObject();
    public bool HasKitchenObject();

}
