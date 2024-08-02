using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    // Start is called before the first frame update
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    
    
    public override void Interact(Player player){
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                player.GetKitchenObject().SetkitchenObjectParent(this);
            }
            else{
                //player not carrying anything
            }

        }
        else{
            if(player.HasKitchenObject()){
                if(player.GetKitchenObject() is PlateKitchenObject){
                    //player is carrying a plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    plateKitchenObject.AddIngreadients(GetKitchenObject().GetKitchenObjectsSO());
                    GetKitchenObject().DestroySelf();
                }
                
            }
            else{
                GetKitchenObject().SetkitchenObjectParent(player);
            }

        }
        
    }
  
}

