using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const String CUT = "Cut";
    // Start is called before the first frame update
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start(){
        cuttingCounter.OnCut += CuttingCounter_OnCut;

    }
    private void CuttingCounter_OnCut(object sender, System.EventArgs e){
        animator.SetTrigger(CUT);

    }

    
}
