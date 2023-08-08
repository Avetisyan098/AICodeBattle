using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadInput : MonoBehaviour
{
    public InputActionAsset InputActionButton;
    InputAction Button;

    void Start()
    {
        var GameActionMap = InputActionButton.FindActionMap("VrButton");

        Button = GameActionMap.FindAction("Button");


        Button.performed += ButtonPress;
        Button.Enable();
        
    }

    void ButtonPress(InputAction.CallbackContext context)
    {
        Debug.Log("sss");
    }
}
