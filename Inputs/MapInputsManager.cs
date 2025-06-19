using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapInputsManager : MonoBehaviour
{
    private MapActions _inputActions;

    public static MapInputsManager Instance { get; private set; }

    //public Vector2 MovementVector { get
    //    {
    //        int x = 0;
    //        int y = 0;
    //
    //        if (_inputActions.Movement.MoveUp.IsPressed())
    //            y += 1;
    //        if (_inputActions.Movement.MoveDown.IsPressed())
    //            y -= 1;
    //        if (_inputActions.Movement.MoveRight.IsPressed())
    //            x += 1;
    //        if (_inputActions.Movement.MoveLeft.IsPressed())
    //            x -= 1;
    //
    //        return new Vector2(x, y);
    //    } 
    //}

    public Vector2 MovementVector { get; private set; } = Vector2.zero;

    private void Awake()
    {
        Debug.Log("InputManager awake");

        _inputActions = new MapActions();
        //_inputActions.Enable();

        Instance = this;

        _inputActions.Disable();
        _inputActions.Clicks.ClickRight.performed += ctx => Debug.Log("Right-click performed!");

        AssignArrows();

        _inputActions.Enable();
    }

    private void Update()
    {
        //if (Mouse.current.rightButton.wasPressedThisFrame)
        //    Debug.Log("right btn was pressed");
    }

    private void AssignArrows()
    {
        _inputActions.Movement.MoveUp.started += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveUp.canceled += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveDown.started += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveDown.canceled += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveRight.started += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveRight.canceled += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveLeft.started += ctx => UpdateMovementVector();
        _inputActions.Movement.MoveLeft.canceled += ctx => UpdateMovementVector();
    }

    private void UpdateMovementVector()
    {
        MovementVector = new Vector2(
            (_inputActions.Movement.MoveRight.IsPressed() ? 1 : 0) +
            (_inputActions.Movement.MoveLeft.IsPressed() ? -1 : 0),

            (_inputActions.Movement.MoveUp.IsPressed() ? 1 : 0) +
            (_inputActions.Movement.MoveDown.IsPressed() ? -1 : 0)
        );
    }

    public void SubscribeOnMove(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Disable();
        _inputActions.Clicks.ClickRight.performed += action;
        _inputActions.Enable();
    }

    public void UnsubscribeOnMove(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Disable();
        _inputActions.Clicks.ClickRight.performed -= action;
        _inputActions.Enable();
    }

    public void SubscribeOnMenu(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Disable();
        _inputActions.Controls.Menu.performed += action;
        _inputActions.Enable();
    }

    public void UnsubscribeOnMenu(Action<InputAction.CallbackContext> action)
    {
        _inputActions.Disable();
        _inputActions.Controls.Menu.performed -= action;
        _inputActions.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
        _inputActions = null;
    }
}
