using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
  private IKitchenObjectParent kitchenObjectParent;

  public IKitchenObjectParent GetkitchenObjectParent(){
    return kitchenObjectParent;
  }
  public void SetkitchenObjectParent(IKitchenObjectParent kitchenObjectParent){
    if(this.kitchenObjectParent != null){
        this.kitchenObjectParent.ClearKitchenObject();

    }
    this.kitchenObjectParent = kitchenObjectParent;
    if(kitchenObjectParent.HasKitchenObject()){
        Debug.LogError("Counter already has Kitchen object");
    }
    kitchenObjectParent.SetKitchenObejct(this);
    transform.parent = kitchenObjectParent.GetKitchenObjectFollowTrasnform();
    transform.localPosition = Vector3.zero;
  }
  public KitchenObjectsSO GetKitchenObjectsSO(){
    return kitchenObjectsSO;
  }
  public void DestroySelf(){
    kitchenObjectParent.ClearKitchenObject();
    Destroy(gameObject);
  }
  public static KitchenObject SpwanKitchenObject(KitchenObjectsSO kitchenObjectsSO,IKitchenObjectParent kitchenObjectParent){
    Transform kitchenObejctTransform  = Instantiate(kitchenObjectsSO.prefab);
    KitchenObject kitchenObject = kitchenObejctTransform.GetComponent<KitchenObject>();
    kitchenObject.SetkitchenObjectParent(kitchenObjectParent);
    return kitchenObject;
  }
}
