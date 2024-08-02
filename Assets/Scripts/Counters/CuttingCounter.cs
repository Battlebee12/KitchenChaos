using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] CuttingRecipieSO[] cuttingRecipieSOArray;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public event EventHandler OnCut;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                //Player is carrying something
                if(hasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectsSO())){
                    //player carrying something that cna be cut

                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progressNormalized = (float)cuttingProgress/cuttingRecipieSO.cuttingProgressMax,
                    });

                }
                
            }
            else{
                //player not carrying anything
            }

        }
        else{
            if(player.HasKitchenObject()){
                
            }
            else{
                GetKitchenObject().SetkitchenObjectParent(player);
            }

        }
    }
    public override void InteractAlternate(Player player){
        if(HasKitchenObject() && hasRecipieWithInput(GetKitchenObject().GetKitchenObjectsSO())){
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);
            CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectsSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = (float)cuttingProgress/cuttingRecipieSO.cuttingProgressMax,
            });

            if(cuttingProgress >= cuttingRecipieSO.cuttingProgressMax){
                KitchenObjectsSO outputkitchenObjectsSO = getOutputForInput(GetKitchenObject().GetKitchenObjectsSO());
                

                GetKitchenObject().DestroySelf();
                KitchenObject.SpwanKitchenObject(outputkitchenObjectsSO,this);

            }
            
            
        }
    }
    private bool hasRecipieWithInput(KitchenObjectsSO inputKitchenObjectSO){
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        return cuttingRecipieSO != null;
        

    }
    public KitchenObjectsSO getOutputForInput(KitchenObjectsSO inputKitchenObjectSO){
        CuttingRecipieSO cuttingRecipieSO = GetCuttingRecipieSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipieSO.output != null){
            return cuttingRecipieSO.output;
        }
        else{
            return null;
        }
    

    }
    private CuttingRecipieSO GetCuttingRecipieSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach(CuttingRecipieSO cuttingRecipieSO in cuttingRecipieSOArray){
            if(cuttingRecipieSO.input == inputKitchenObjectSO){
                return cuttingRecipieSO;
            }
        }
        
        return null;
    }
}
