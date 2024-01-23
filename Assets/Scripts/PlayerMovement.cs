using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControlsScript playerControls;
    [SerializeField] CharacterController characterController;

    [Header("Cutscene")]
    [SerializeField] bool inCutscene;

    [Header("Movement")]
    [SerializeField] float speed;
    Vector3 playerVelocity;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravity = -9.8f;

    #region Cutscene Get/Set
    public void SetCutscene(bool cutscene)
    {
        inCutscene = cutscene;

        if(inCutscene)
        {
            OnDisable();
        }
    }
    public bool GetCutscene()
    {
        return inCutscene;
    }
    #endregion

    private void Awake()
    {
        playerControls = new PlayerControlsScript();
        playerControls.Player.Enable();

        playerControls.Player.Jump.performed += Jump;
    }

    #region On Enable/Disable
    private void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
        playerControls.Player.Jump.performed -= Jump;
    }
    #endregion

    private void Update()
    {
        ProcessMovement();
    }
    
    private void ProcessMovement()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 movementDir = new Vector3(inputVector.x, 0, inputVector.y);

        playerVelocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }

        Vector3 moveInputVal = transform.TransformDirection(movementDir) * speed;
        playerVelocity.x = moveInputVal.x;
        playerVelocity.z = moveInputVal.z;

        characterController.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
