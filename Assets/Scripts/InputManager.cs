using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    private InputActions inputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    void Awake(){
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternative.performed += InteractAlternative_performed;


    }
    private void InteractAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAlternativeAction?.Invoke(this,EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
       // Debug.Log(obj);
        OnInteractAction?.Invoke(this,EventArgs.Empty); // if not null send notif
    }
    public Vector2 GetInputVectorNomralized(){

        Vector2 inputVector = inputActions.Player.move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized ;
        return inputVector;
    }
    
}
