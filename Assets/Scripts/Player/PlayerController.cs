using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour, Controls.IPlayerActions
{
    public Vector3 movement { get; private set; }
    [SerializeField]internal Transform cameraTransform;
    public Rigidbody Rb { get; private set; }
    private Controls _controls;
    internal PlayerMovement _playerMovement;
    public static Action PlayerJumpAction; 
    public static Action PlayerSprintAction;
    public static Action PlayerCrouchAction;
    public static Action<float> PlayerMaskAction;
    public static Action PlayerDeathAction;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Player.Move.performed += OnMove;
        _controls.Player.Move.canceled += OnMove;
        _controls.Player.Jump.performed += OnJump;
        _controls.Player.Sprint.performed += OnSprint;
        _controls.Player.Sprint.canceled += OnSprint;
        _controls.Player.Crouch.performed += OnCrouch;
        _controls.Player.Crouch.canceled += OnCrouch;
        _controls.Player.Mask.performed += OnMask;
    }

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        OnEnablePlayer();
        PlayerDeathAction += OnDeath;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDisable()
    {
        OnDisablePlayer();
    }

    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {

    }


    private void OnEnablePlayer()
    {
        _controls.Player.Enable();
    }

    private void OnDisablePlayer()
    {
        _controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector3>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        PlayerCrouchAction?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        PlayerJumpAction?.Invoke();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        PlayerSprintAction?.Invoke();
    }

    public void OnMask(InputAction.CallbackContext context)
    {
        Debug.Log("OnMask");
        PlayerMaskAction?.Invoke(context.ReadValue<float>());
    }
}
