using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot; 
    private PlayerMotor motor;
    private PlayerLook look;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        // anytime onFoot.Jump  are performed, using callback context (ctx) 
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    // tel the playermotor to move by using the value from movement action
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

 
    private void OnEnable() {
        onFoot.Enable();
    }

    private void OnDisable(){
        onFoot.Disable();
    } 
}
