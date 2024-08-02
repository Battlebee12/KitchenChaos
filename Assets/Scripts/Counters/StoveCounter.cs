using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnStateChangedEventArgs : EventArgs{
        public State state;
    }

    public enum State{
        Idle,
        Frying,
        Fried,
        Burned
    }
    private State state;
    [SerializeField] private FryingRecipieSO[] fryingRecipieSOArray;
    [SerializeField] private BurningRecipieSO[] burningRecipieSOArray;
    private float fryingTimer;
    private float burnedTimer;
    private FryingRecipieSO fryingRecipieSO;
    private BurningRecipieSO burningRecipieSO;

    private void Start(){
        state = State.Idle;
    }
    private void Update() {
        if(HasKitchenObject()){
            switch (state) {
                case State.Idle:
                    break;

                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer/ fryingRecipieSO.fryingTimerMax

                    });
        
                    if(fryingTimer > fryingRecipieSO.fryingTimerMax){
                        //Fried

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpwanKitchenObject(fryingRecipieSO.output,this);
                        
                        burningRecipieSO = GetBurningRecipieSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                        state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                    }
                    break;

                case State.Fried:
                     burnedTimer += Time.deltaTime;
                     OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = burnedTimer/ burningRecipieSO.burningTimerMax

                    });
        
                    if(burnedTimer > burningRecipieSO.burningTimerMax){
                        //Fried
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpwanKitchenObject(burningRecipieSO.output,this);
                        
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = 0f

                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }

        
        
        
    }






    public override void Interact(Player player)
    {
         if(!HasKitchenObject()){
            if(player.HasKitchenObject()){
                //Player is carrying something
                if(hasRecipieWithInput(player.GetKitchenObject().GetKitchenObjectsSO())){
                    //player carrying something that cna be Fried

                    player.GetKitchenObject().SetkitchenObjectParent(this);
                    fryingRecipieSO = GetFryingRecipieSOWithInput(GetKitchenObject().GetKitchenObjectsSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = fryingTimer/ fryingRecipieSO.fryingTimerMax

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
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{
                            state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs{
                        progressNormalized = 0f

                });
            }

        }
    }
    private bool hasRecipieWithInput(KitchenObjectsSO inputKitchenObjectSO){
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);
        return fryingRecipieSO != null;
        

    }
    public KitchenObjectsSO getOutputForInput(KitchenObjectsSO inputKitchenObjectSO){
        FryingRecipieSO fryingRecipieSO = GetFryingRecipieSOWithInput(inputKitchenObjectSO);
        if(fryingRecipieSO.output != null){
            return fryingRecipieSO.output;
        }
        else{
            return null;
        }
    

    }
    private FryingRecipieSO GetFryingRecipieSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach(FryingRecipieSO fryingRecipieSO in fryingRecipieSOArray){
            if(fryingRecipieSO.input == inputKitchenObjectSO){
                return fryingRecipieSO;
            }
        }
        
        return null;
    }
    private BurningRecipieSO GetBurningRecipieSOWithInput(KitchenObjectsSO inputKitchenObjectSO){
        foreach(BurningRecipieSO burningRecipieSO in burningRecipieSOArray){
            if(burningRecipieSO.input == inputKitchenObjectSO){
                return burningRecipieSO;
            }
        }
        
        return null;
    }
}
