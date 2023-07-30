using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInputController : MonoBehaviour
{
    [Header("*Player")]
    [SerializeField] PlayerInput _input;
    [SerializeField] IMoveable _player;
    [SerializeField] MoveCore moveCore;
    [SerializeField] PlayerController myPlayer;

    [Header("*Camera")]
    [SerializeField] Transform mainCamera;
    [SerializeField] CinemachineVirtualCamera followCamera;
    [SerializeField] float turnTime;

    [Header("*Input")]
    [SerializeField] CinemachineInputProvider inputProcider;
    [SerializeField] InputActionReference moveReference;

    [Header("*UI")]
    [SerializeField] Compass compass;

    private void Awake()
    {
        TryGetComponent(out _input);
        mainCamera = Camera.main.transform;
    }


    private void Start()
    {
        PlayManager.Inst.OnCompleteSpawn += SetPlayerControllr;
    }

    private void SetPlayerControllr()
    {
        var playerController = PlayManager.Inst.GetMineController();

        myPlayer = playerController;
        myPlayer.TryGetComponent(out _player);
        myPlayer.mainCamera = mainCamera.gameObject;
        followCamera.Follow = playerController.transform;
        followCamera.LookAt = playerController.transform;
    }

    private void OnEnable()
    {
        SetPlayerInput();
    }

    private void SetPlayerInput()
    {
        var playerInput = _input.actions.FindActionMap("Player");
        // playerInput["Move"].performed += OnMove;
        // playerInput["Move"].canceled += OnMoveStop;
        playerInput["Dash"].performed += OnDash;
        playerInput["Dash"].canceled += OnDashStop;
    }

    private void OnDisable()
    {
        // _input.actions["Move"].performed -= OnMove;
        // _input.actions["Move"].canceled -= OnMoveStop;
        _input.actions["Dash"].performed -= OnDash;
        _input.actions["Dash"].canceled -= OnDashStop;
    }

    #region Player
    //private void OnMove(InputAction.CallbackContext obj)
    //{
    //    var value = obj.ReadValue<Vector2>();
    //    var direction = new Vector3(value.x, 0, value.y);
    //    _player.SetDirection(direction);
    //}

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
