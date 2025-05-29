using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapInputsManager : MonoBehaviour
{
    private MapActions _inputActions;

    public static MapInputsManager Instance { get; private set; }

    private void Awake()
    {
        _inputActions = new MapActions();
        //_inputActions.Enable();

        Instance = this;

        _inputActions.Clicks.ClickRight.performed += ctx => Debug.Log("Right-click performed!");

        _inputActions.Enable();
    }

    private void Update()
    {
        //if (Mouse.current.rightButton.wasPressedThisFrame)
        //    Debug.Log("right btn was pressed");
    }

    public void SubscribeOnMove(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Disable();
        _inputActions.Clicks.ClickRight.performed += action;
        _inputActions.Enable();
    }
}
