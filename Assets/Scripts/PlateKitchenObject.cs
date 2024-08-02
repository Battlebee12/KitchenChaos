using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    private List<KitchenObjectsSO> kitchenObjectsSOList;
    private void Awake() {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }
    public void AddIngreadients(KitchenObjectsSO kitchenObjectsSO){
        kitchenObjectsSOList.Add(kitchenObjectsSO);

    }
}
