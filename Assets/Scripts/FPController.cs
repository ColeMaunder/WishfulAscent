using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    public int baseCharacter = 0;
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

    private void Awake()
    {
        character = characters[baseCharacter];
        cameraTransform.SetParent(character);
        controller = character.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        standHight = cameraTransform.localPosition;
        crouchHight = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y * crouchMod, cameraTransform.localPosition.z);

    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleHold();
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
        if(context.performed && controller.isGrounded){
            velocity.y = Mathf.Sqrt(jumpHight * -2f * gravity);
        }
    }
    public void OnSwap(InputAction.CallbackContext context)
    {
        if(context.performed){
            if (character == characters[0]){
                character = characters[1];
            }else{
                character = characters[0];
            }
            cameraTransform.SetParent(character);
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
    public void HandleHold(){
        if (holdInput){
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, pickupRange)){
                if (hit.collider.CompareTag("Object")){
                    heldObject = hit.collider.gameObject;
                    Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                    if (rb != null){
                        Object hitObject = heldObject.GetComponent<Object>();
                        hitObject.SetHoolding(true);
                    }
                    heldObject.transform.SetParent(holdPoint);
                    //heldObject.transform.localPosition = Vector3.zero;
                    //heldObject.transform.localRotation = Quaternion.identity;
                }
            }
        }
        else{
            if (heldObject != null){
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null){
                    heldObject.GetComponent<Object>().SetHoolding(false);
                }
                heldObject.transform.SetParent(null);
                heldObject = null;
            }
        }
    }
    public void wipeHeldObject(){
        heldObject = null;
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
}

