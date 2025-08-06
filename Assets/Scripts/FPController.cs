using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    [Header("Pick UP Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private GameObject heldObject;
    private bool holding;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float holdInput;
    private float runInput;
    private Vector3 velocity;
    private float verticalRotation = 0f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    public void OnHold(InputAction.CallbackContext context)
    {
        holdInput = context.ReadValue<float>();
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        runInput = context.ReadValue<float>();
    }


    public void HandleMovement()
    {
        float speed = moveSpeed;
        if(runInput > 0){
            speed = runSpeed;
        }
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
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
        transform.Rotate(Vector3.up * mouseX);
    }
    public void HandleHold()
    {
        if(holdInput>0){
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, pickupRange)){
                if (hit.collider.CompareTag("Object")){
                    heldObject = hit.collider.gameObject;
                        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                        if (rb != null){
                            rb.isKinematic = true;
                            heldObject.GetComponent<Object>().SetHoolding(true);   
                        }
                        heldObject.transform.SetParent(holdPoint);
                        //heldObject.transform.localPosition = Vector3.zero;
                        //heldObject.transform.localRotation = Quaternion.identity;
                }
            } 
        }else {
            if(heldObject != null) {
                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null) {
                    rb.isKinematic = false;
                    heldObject.GetComponent<Object>().SetHoolding(false);
                }
                heldObject.transform.SetParent(null);
                heldObject = null;
            }
        }
    }
}

