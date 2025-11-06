using System;
using System.Collections;
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
    public float crouchHight;
    private float standHight;
    [SerializeField] private float speed = 3;
    private Coroutine crouch;
    private bool crouching;
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
    Animator animator;


    private void Awake()
    {
        Camera3P = transform.GetChild(2).gameObject;
        Camera3P.transform.GetComponentInChildren<Camera>().enabled = false;
        Camera3P.transform.GetComponentInChildren<AudioListener>().enabled = false;
        character = characters[currentCharacter];
        cameraTransform = character.GetComponentInChildren<Camera>().transform;
        cameraTransform.GetComponent<Camera>().enabled = true;
        cameraTransform.GetComponent<AudioListener>().enabled = true;
        controller = character.GetComponent<CharacterController>();
        animator = character.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        standHight = controller.height;
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
            cameraTransform = character.GetComponentInChildren<Camera>().transform;
            cameraTransform.gameObject.GetComponent<Camera>().enabled = true;
            cameraTransform.gameObject.GetComponent<AudioListener>().enabled = true;
            animator = character.GetComponent<Animator>();
            controller.Move(Vector3.zero);
            controller = character.GetComponent<CharacterController>();

        }
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) {
            GameObject.Find("Ui Screnes").GetComponent<Pause>().ActivetePause();
            Debug.Log("Pause triggered");
        }
    }
    IEnumerator CrouchDown(){
        while(controller.height > crouchHight) {
            yield return new WaitForSeconds(0.01f);
            controller.height -= 0.1f/speed;
            controller.center = new Vector3(0, controller.center.y + (0.035f/speed), 0);
            animator.SetFloat("StandHight", (controller.height-crouchHight)/(standHight-crouchHight));
        }
        controller.height = crouchHight;
        animator.SetFloat("StandHight", 0);
    }
    IEnumerator CrouchUp(){
        while(controller.height < standHight) {
            yield return new WaitForSeconds(0.01f);
            controller.height += 0.1f/speed;
            controller.center = new Vector3(0, controller.center.y - (0.035f / speed), 0);
            animator.SetFloat("StandHight", (controller.height-crouchHight)/(standHight-crouchHight));
        }
        controller.height = standHight;
        animator.SetFloat("StandHight", 1);
    }
    public void HandleMovement()
    {
        float speed = moveSpeed;
        if (character.GetComponent<Luna>() != null && character.GetComponent<Luna>().GetFlight()) {
            animator.SetBool("Fly", true);
            if (jumpInput) {
                moveY = flySpeed;
            } else if (crouchInput) {
                moveY = -flySpeed;
            } else  {
                moveY = 0;
            }
            velocity.y = 0;
            if (verticalRotation > verticalLookLimit - flyLookVeriance || verticalRotation < -verticalLookLimit + flyLookVeriance) {
                flyLook = flySpeed * -verticalRotation / 90;
            } else {
                flyLook = 0;
                if (!jumpInput && !crouchInput) {
                    velocity.y = 0;
                }
            }
        } else {
            animator.SetBool("Fly", false);
            moveY = 0;
            velocity.y += gravity * Time.deltaTime;
            if (controller.isGrounded && jumpInput) {
                animator.SetTrigger("Jump");
                velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
            }

            if (crouchInput) {
                speed = crouchSpeed;
                if(!crouching){
                    if(crouch != null){
                        StopCoroutine(crouch);
                    }
                    crouching = true;
                crouch = StartCoroutine(CrouchDown());
                }
            } else {
                if(crouching){
                    if(crouch != null){
                        StopCoroutine(crouch);
                    }
                    crouching = false;
                crouch = StartCoroutine(CrouchUp());
                }
            }
        }

        if (runInput)  {
            speed = runSpeed;
        }
        Vector3 move = character.right * moveInput.x + character.forward * moveInput.y + character.up * moveY;
        float directionY = 0;
        float directionX = 0;
        if (moveInput.y != 0) {
            directionY = Mathf.Sign(moveInput.y);
        }
        if (moveInput.x != 0) {
            directionX = Mathf.Sign(moveInput.x);
        }
        if (runInput)  {
            directionX *= 2;
            directionY *= 2;
        } 
        animator.SetFloat("Y Direction", directionY);
        animator.SetFloat("X Direction", directionX);

        if (flyLook != 0 && (moveInput.x != 0 || moveInput.y != 0)) {
            move += character.up * flyLook;
        }

        controller.Move(move * speed * Time.deltaTime);

        if (moveY == 0) {
            if (controller.isGrounded && velocity.y <= 0) {
                print("down");
                velocity.y = -2f;
            }
        } else {
            velocity.y = 0;
        }
        controller.Move(velocity * Time.deltaTime);
    }
    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity;
        float mouseY = lookInput.y * lookSensitivity;

        verticalRotation -= mouseY;
        //verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);
        if(verticalRotation > verticalLookLimit){
            verticalRotation = verticalLookLimit;
        } else if (verticalRotation < -verticalLookLimit){
            verticalRotation = -verticalLookLimit;
        }

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0);
        //animator.SetFloat("Look", verticalRotation+verticalLookLimit);
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

