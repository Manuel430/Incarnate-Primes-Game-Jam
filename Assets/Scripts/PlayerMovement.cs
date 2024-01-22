using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    /*
     PlayerInputScript;
    CharacterController characterController;

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

        if (inCutscene)
        {
            playerControls.Player.Disable();
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

        characterController = GetComponent<CharacterController>();

        playerControls.Player.Jump.performed += Jump;
    }

    private void Update()
    {
        ProcessMovement();
    }
    private void ProcessMovement()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 movementDir = new Vector3(inputVector.x, 0, 0);

        playerVelocity.y += gravity * Time.deltaTime;

        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }

        Vector3 moveInputVal = transform.TransformDirection(movementDir) * speed;
        playerVelocity.x = moveInputVal.x;

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    */
}
