using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GD_FirstPersonController : MonoBehaviour
{
    public float WalkSpeed = 0.5f;
    public float SprintMultiplier = 1.05f;
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
        characterController = GetComponent<CharacterController>(); // Získá CharacterController
        Cursor.lockState = CursorLockMode.Locked; // Uzamkne kurzor do středu obrazovky
    }

    void Update()
    {
        // Zpracovávat vstupy pouze pokud hra není pozastavena a kurzor je uzamčen
        if (Time.timeScale == 0f || Cursor.lockState != CursorLockMode.Locked)
        {
            return; // Přeskočit zpracování vstupů
        }

        // Získání vstupů pro pohyb
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        // Výpočet směru pohybu podle transformace hráče
        Vector3 moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
        moveDirection.Normalize();

        // Aplikace rychlosti pohybu, se sprintem pokud aktivován
        float speed = WalkSpeed;
        if(Input.GetAxis("Sprint") > 0)
        {
            speed *= SprintMultiplier;
        }

        characterController.Move(moveDirection * speed * Time.deltaTime);

        // Skákání a gravitace
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y = JumpForce;
        }else{
            velocity.y += Gravity * Time.deltaTime;  // Gravitační pád
        }

        characterController.Move(velocity * Time.deltaTime);

        // Kamera – otočení podle myši
        if(PlayerCamera != null){
            float mouseX = Input.GetAxis("Mouse X") * LookSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * LookSensitivityY;
            
            verticalRotation -= mouseY; // Myš nahoru/dolů
            verticalRotation = Mathf.Clamp(verticalRotation, MinYLookAngle, MaxYLookAngle);

            PlayerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f); // Otočení kamery nahoru/dolů
            transform.Rotate(Vector3.up * mouseX); // Otočení postavy doleva/doprava
        }
    }

     // Kontrola, jestli je hráč na zemi (pomocí raycastu směrem dolů)
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

