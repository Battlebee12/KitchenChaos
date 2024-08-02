using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
   [SerializeField] private Transform counterTopPoint;
   [SerializeField] private Transform plateVisualPrefab;
   private List<GameObject> plateVisualGameObjectList;
   private void Awake() {
    plateVisualGameObjectList = new List<GameObject>();
    
   }
   private void Start() {
        platesCounter.OnPlateSpawn += PlateCouneter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlateCouneter_OnPlateRemove;
   }
   private void PlateCouneter_OnPlateSpawn(object sender, System.EventArgs e) {
    Transform plateVisualTransofrm = Instantiate(plateVisualPrefab,counterTopPoint);

    float plateOffsetY = 0.1f;
    plateVisualTransofrm.localPosition = new Vector3(0,plateVisualGameObjectList.Count*plateOffsetY,0);
    plateVisualGameObjectList.Add(plateVisualTransofrm.gameObject);

   }
   private void PlateCouneter_OnPlateRemove(object sender, System.EventArgs e) {
    GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count-1];
    plateVisualGameObjectList.Remove(plateGameObject);
    Destroy(plateGameObject);

   }

}


