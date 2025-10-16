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
    public float flySpeed = 2f;
    public float runSpeed = 8f;
    public float crouchSpeed = 3f;
    public float crouchMod = 0.5f;
    public Vector3 crouchHight;
    private Vector3 standHight;
    public float gravity = -9.81f;
    public float jumpHight = 1.5f;
    public float flyLookVeriance = 25f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool runInput;
    private bool crouchInput;
    private bool jumpInput;
    private Vector3 velocity;
    [SerializeField] private float verticalRotation = 0f;
    private bool paused = false;
    private float flyLook = 0;
    private float moveY;
    private GameObject Camera3P;


    private void Awake()
    {
        Camera3P = transform.GetChild(2).gameObject;
        Camera3P.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
        Camera3P.transform.GetChild(0).gameObject.GetComponent<AudioListener>().enabled = false;
        character = characters[currentCharacter];
        cameraTransform = character.GetChild(0);
        cameraTransform.gameObject.GetComponent<Camera>().enabled = true;
        cameraTransform.gameObject.GetComponent<AudioListener>().enabled = true;
        controller = character.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        standHight = cameraTransform.localPosition;
        crouchHight = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y * crouchMod, cameraTransform.localPosition.z);

    }

    private void Update()
    {
        if (!paused)
        {
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
    public void OnJump(InputAction.CallbackContext context)
    {
        jumpInput = context.performed;
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
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) {
            GameObject.FindWithTag("UI Screens").GetComponent<Pause>().ActivetePause();
        }
    }
    public void HandleMovement()
    {
        float speed = moveSpeed;
        if (character.GetComponent<Luna>() != null && character.GetComponent<Luna>().GetFlight())
        {
            if (jumpInput)
            {
                moveY = flySpeed;
            }
            else if (crouchInput)
            {
                moveY = -flySpeed;
            }
            else
            {
                moveY = 0;
            }
            velocity.y = 0;
            if (verticalRotation > verticalLookLimit - flyLookVeriance || verticalRotation < -verticalLookLimit + flyLookVeriance)
            {
                flyLook = flySpeed * -verticalRotation / 90;
            }
            else
            {
                flyLook = 0;
                if (!jumpInput && !crouchInput)
                {
                    velocity.y = 0;
                }
            }
        }
        else
        {
            moveY = 0;
            velocity.y += gravity * Time.deltaTime;
            if (controller.isGrounded && jumpInput)
            {
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
            }

            if (crouchInput)
            {
                speed = crouchSpeed;
                cameraTransform.localPosition = crouchHight;
            }
            else
            {
                cameraTransform.localPosition = standHight;
            }
        }

        if (runInput)
        {
            speed = runSpeed;
        }
        Vector3 move = character.right * moveInput.x + character.forward * moveInput.y + character.up * moveY;
        if (flyLook != 0 && (moveInput.x != 0 || moveInput.y != 0))
        {
            move += character.up * flyLook;
        }

        controller.Move(move * speed * Time.deltaTime);

        if (moveY == 0)
        {
            if (controller.isGrounded && velocity.y <= 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {
            velocity.y = 0;
        }
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
    public Transform GetActiveCharicter()
    {
        return character;
    }
    public void DisableChricters(bool on)
    {
        if (on)
        {
            paused = true;
            //controller.enabled = false;
        }
        else
        {
            character = characters[currentCharacter];
            //controller.enabled = true;
            paused = false;
        }
    }
    
    public String GetControlScheme(){
        PlayerInput input = gameObject.GetComponent<PlayerInput>();
        return input.currentControlScheme;
    }
    public string GetActiveName(bool active){
        if(active) {
            return character.name;
        }else{
            foreach (Transform i in characters) {
                if(i != character){
                    return i.name;
                }
            }
        }
        return null;
    }
}

