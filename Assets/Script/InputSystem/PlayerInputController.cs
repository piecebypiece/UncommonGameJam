using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _player;
    [SerializeField] MoveCore moveCore;
    [SerializeField] public GameObject player;

    private void Awake()
    {
        TryGetComponent(out _input);
    }

    public void PlayerSetting()
    {
        player.TryGetComponent(out _player);
    }

    private void OnEnable()
    {
        SetPlayerInput();
    }

    private void SetPlayerInput()
    {
        var playerInput = _input.actions.FindActionMap("Player");
        playerInput["Move"].performed += OnMove;
        playerInput["Move"].canceled += OnMoveStop;
        playerInput["Dash"].performed += OnDash;
        playerInput["Dash"].canceled += OnDashStop;
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Dash"].performed -= OnDash;
        _input.actions["Dash"].canceled -= OnDashStop;
    }

    #region Player
    private void OnMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        var direction = new Vector3(value.x, 0, value.y);
        _player.SetDirection(direction);
    }

    private void OnMoveStop(InputAction.CallbackContext obj)
    {
        _player.SetDirection(Vector3.zero);
    }

    private void OnDash(InputAction.CallbackContext obj)
    {
        _player.Dash(PlayerController.dashSpeed);
    }

    private void OnDashStop(InputAction.CallbackContext obj)
    {
        _player.Dash(PlayerController.normalSpeed);
    }
    #endregion
}
