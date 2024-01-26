using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControlsScript playerControls;
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject playerBody;
    [SerializeField] Animation playerANIM;

    [Header("Cutscene")]
    [SerializeField] bool inCutscene;
    [SerializeField] bool isDead;
    [SerializeField] bool hasWon;

    [Header("Movement")]
    [SerializeField] float speed;
    Vector3 playerVelocity;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 1f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] bool isJumping;
    [SerializeField] bool startJump;

    [Header("Grab")]
    [SerializeField] bool canGrab;

    [Header("DeathTimer")]
    [SerializeField] float deathTime = 5;

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
        isDead = false;
    }

    #region On Enable/Disable
    public void OnEnable()
    {
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump;
    }

    public void OnDisable()
    {
        playerControls.Player.Disable();
        playerControls.Player.Jump.performed -= Jump;
    }
    #endregion

    private void Update()
    {
        ProcessMovement();

        if(characterController.isGrounded)
        {
            isJumping = false;
            startJump = true;
        }

        if (hasWon)
        {
            playerANIM.Stop("Grab");
        }

        if (isDead)
        {
            playerANIM.Play("Death");
            deathTime -= Time.deltaTime;

            if(deathTime < 1 )
            {
                deathTime = 0;
                playerANIM.Stop("Death");
                playerANIM.RemoveClip("Death");
                playerANIM.Play("DeathPose");
            }
           
        }
    }
    
    public bool CanGrab(bool setActive)
    {
        return canGrab = setActive;
    }

    private void ProcessMovement()
    {
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2;
        }

        if (isDead || hasWon)
        {
            return;
        }

        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        Vector3 movementDir = (new Vector3(inputVector.x, 0, inputVector.y)).normalized;
        if (inputVector.magnitude != 0)
        {
            if (isJumping)
            {
                if (startJump)
                {
                    playerANIM.Play("Jump");
                    startJump = false;
                }
                playerANIM.PlayQueued("Jump_Fall");
            }
            else
            {
                playerANIM.Play("Run");
            }

            float angle = Vector3.SignedAngle(playerBody.transform.forward, movementDir, Vector3.up);

            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);

            playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, playerBody.transform.rotation * rot, Time.deltaTime * rotationSpeed);
        }
        else
        {
            if (canGrab)
            {
                playerANIM.Play("Grab");
                canGrab = false;
                return;
            }
            else if (characterController.isGrounded)
            {
                playerANIM.Play("Idle");
            }
            else
            {
                if (isJumping)
                {
                    if (startJump)
                    {
                        playerANIM.Play("Jump");
                        startJump = false;
                    }
                }
            }
        }


        playerVelocity.y += gravity * Time.deltaTime;

        Vector3 moveInputVal = transform.TransformDirection(movementDir) * speed;
        playerVelocity.x = moveInputVal.x;
        playerVelocity.z = moveInputVal.z;

        characterController.Move(playerVelocity * Time.deltaTime);

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (characterController.isGrounded)
        {
            isJumping = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void GameOver()
    {
        playerANIM.Stop("Run");
        playerANIM.RemoveClip("Run");
        playerANIM.Stop("Idle");
        playerANIM.RemoveClip("Idle");

        isDead = true;
    }
    public void GameWin()
    {
        playerANIM.Stop("Run");
        playerANIM.Stop("Idle");
        hasWon = true;

    }
}
