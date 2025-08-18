using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    public int currentCharacter = 0;
    public Transform[] characters;
    public Transform character;
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float crouchSpeed = 3f;
    public float crouchMod = 0.5f;
    public Vector3 crouchHight;
    private Vector3 standHight;
    public float gravity = -9.81f;
    public float jumpHight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    [Header("Pick UP Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private GameObject heldObject;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool holdInput;
    private bool runInput;
    private bool crouchInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    private bool paused = false;

    private void Awake()
    {
        character = characters[currentCharacter];
        controller = character.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        standHight = cameraTransform.localPosition;
        crouchHight = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y * crouchMod, cameraTransform.localPosition.z);

    }

    private void Update()
    {
        if(!paused){
            HandleMovement();
            HandleLook();
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        runInput = context.performed;
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        crouchInput = context.performed;
    }
    public void OnHold(InputAction.CallbackContext context)
    {
        holdInput = context.performed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded && !paused)
        {
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }
    }
    public void OnSwap(InputAction.CallbackContext context)
    {
        if (context.performed && !paused)
        {
            if (character == characters[0])
            {
                character = characters[1];
                currentCharacter = 1;
            }
            else
            {
                character = characters[0];
                currentCharacter = 0;
            }
            cameraTransform.gameObject.GetComponent<Camera>().enabled = false;
            cameraTransform.gameObject.GetComponent<AudioListener>().enabled = false;
            cameraTransform = character.GetChild(0);
            cameraTransform.gameObject.GetComponent<Camera>().enabled = true;
            cameraTransform.gameObject.GetComponent<AudioListener>().enabled = true;
            controller.Move(Vector3.zero);
            controller = character.GetComponent<CharacterController>();

        }
    }

    public void HandleMovement()
    {
        float speed = moveSpeed;
        if (crouchInput)
        {
            speed = crouchSpeed;
            cameraTransform.localPosition = crouchHight;
        }
        else
        {
            cameraTransform.localPosition = standHight;
            if (runInput)
            {
                speed = runSpeed;
            }
        }
        Vector3 move = character.right * moveInput.x + character.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        character.Rotate(Vector3.up * mouseX);
    }
    public Transform GetActiveCharicter(){
        return character;
    }
    public void DisableChricters(bool on){
        if(on){
            paused = true;
            character = characters[2];
            //controller.enabled = false;
        } else {
            character = characters[currentCharacter];
            //controller.enabled = true;
            paused = false;
        }
    }
}

