using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnPlayerGrabbedObject;
    
    public override void Interact(Player player){
        if(!player.HasKitchenObject()){

            
            KitchenObject.SpwanKitchenObject(kitchenObjectsSO,player);
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }
    }
   
}
