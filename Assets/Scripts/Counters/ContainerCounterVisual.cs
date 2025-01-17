using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const String OPEN_CLOSE = "OpenClose";
    // Start is called before the first frame update
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;
    void Awake(){
        animator = GetComponent<Animator>();
    }
    void Start(){
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;

    }
    private void ContainerCounter_OnPlayerGrabbedObject(object sender, System.EventArgs e){
        animator.SetTrigger(OPEN_CLOSE);

    }

    
}
