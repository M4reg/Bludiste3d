using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GD_FirstPersonController : MonoBehaviour
{
    public float WalkSpeed = 0.5f;
    public float SprintMultiplier = 2f;
    public float JumpForce = 0.1f;
    public float GoundCheckDistance = 0.2f;
    public float LookSensitivityX = 1f;
    public float LookSensitivityY = 1f;
    public float MaxYLookAngle = 90f;
    public float MinYLookAngle = -90f;
    public Transform PlayerCamera;
    public float Gravity = -9.81f;
    private CharacterController characterController;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Zpracovávat vstupy pouze pokud hra není pozastavena a kurzor je uzamčen
        if (Time.timeScale == 0f || Cursor.lockState != CursorLockMode.Locked)
        {
            return; // Přeskočit zpracování vstupů
        }

        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        moveDirection.Normalize();

        float speed = WalkSpeed;
        if(Input.GetAxis("Sprint") > 0)
        {
            speed *= SprintMultiplier;
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = JumpForce;
        }else{
            velocity.y += Gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);

        if(PlayerCamera != null){
            float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;
            
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, MinYLookAngle, MaxYLookAngle);

            PlayerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
    }
    bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, GoundCheckDistance))
        {
            return true;
        }
        return false;
    }
}

