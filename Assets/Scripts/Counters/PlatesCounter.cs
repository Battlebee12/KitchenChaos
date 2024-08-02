using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateRemove;
   private float spawnPlateTimer;
   private float spawnPlateTimerMax = 4f;
   private int platesSpawnedAmount;
   private int platesSpawnedAmountMax = 4;

   [SerializeField] private KitchenObjectsSO plateKitchenObjectSO;
   private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer>spawnPlateTimerMax){
            spawnPlateTimer = 0f;

            if(platesSpawnedAmount<platesSpawnedAmountMax){

                platesSpawnedAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        } 
    }
    public override void Interact(Player player)
    {
       if(!player.HasKitchenObject()){
        //player ins empty handed
        if(platesSpawnedAmount > 0){
            //thers atelast on plate here
            platesSpawnedAmount--;
            KitchenObject.SpwanKitchenObject(plateKitchenObjectSO, player);
            OnPlateRemove?.Invoke(this, EventArgs.Empty);
        }
       }
    }
}
