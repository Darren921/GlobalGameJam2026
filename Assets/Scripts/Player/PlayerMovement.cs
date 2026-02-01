using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : MonoBehaviour
{
    [Header("Classes")] 
    
    internal PlayerController player;
    [SerializeField] private CinemachineCamera VirtualCamera;
     CinemachineInputAxisController CinemachineInputAxisController;
    private Camera Camera;

    [Header("Moving")] 
    
    private Vector3 MoveDir;
    [field : SerializeField] public float MoveSpeed {get; private set;} 
    private Vector3 SmoothedMoveDir;
    private Vector3 SmoothedMoveVelocity;
    
    [SerializeField]private float runSpeed;
    private bool isSprinting;
    

    [Header("Jumping")] 
   
    [SerializeField]private float JumpForce;
    private bool isGrounded;
    private bool isJumping;
    private bool isCrouched;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponents();
        PlayerController.PlayerJumpAction += Jump;
        PlayerController.PlayerSprintAction += Sprint;
        PlayerController.PlayerCrouchAction += Crouch;
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = true;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
    
    private void Update()
    {
      //  print(player.Rb.linearVelocity);
//      print(player.transform.localRotation.y);

    }

    private void FixedUpdate()
    {
        SetMoveDir(player.movement);
        ApplyVelocity();
        RotatePlayerTowardsCamera();
        if (isJumping && isGrounded)
        {
            player.Rb.AddForce(JumpForce * Vector3.up, ForceMode.Impulse);
            isJumping = false;
        }
    }

    
    private void Jump()
    {
        isJumping = true;
    }

    private void Crouch()
    {
        isCrouched = !isCrouched;
        player.transform.localScale = isCrouched ? new Vector3(0.75f, 0.75f, 0.75f) : new Vector3(1f, 1f, 1f);
    }

    private void Sprint()
    {
        isSprinting = !isSprinting;
    }

    private void RotatePlayerTowardsCamera()
    {
        if (!Camera || !player.Rb) return;
        if (Camera.transform.forward == Vector3.zero) return;
        var newRotation = Quaternion.LookRotation(new Vector3(Camera.transform.forward.x, 0f, Camera.transform.forward.z));
        player.Rb.MoveRotation(newRotation);
    }

    private void GetComponents()
    {
        player = GetComponent<PlayerController>();
        CinemachineInputAxisController = VirtualCamera.GetComponent<CinemachineInputAxisController>();
        Camera = Camera.main;
    }

    private void ApplyVelocity()
    {
        player.Rb.linearVelocity = !isSprinting ? new Vector3(MoveDir.normalized.x * MoveSpeed, player.Rb.linearVelocity.y, MoveDir.normalized.z * MoveSpeed) : new Vector3(MoveDir.normalized.x * runSpeed, player.Rb.linearVelocity.y, MoveDir.normalized.z * runSpeed);
       
    }

    private void SetMoveDir(Vector3 newDir)
    {
        var cameraForward = Camera.transform.forward;
        var cameraRight = Camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        MoveDir = cameraForward.normalized * newDir.z + cameraRight.normalized * newDir.x;
    }
}