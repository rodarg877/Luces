using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speedMovement = 5f;
    [SerializeField] private float speedRotation = 2f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private Camera pcamera;

    private Vector3 movement;
    private float rotationX;

    void Start()
    {
        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovementCamera();
        MovementCharacter();
    }

    void MovementCharacter()
    {
        float movX = Input.GetAxis("Horizontal");
        float movZ = Input.GetAxis("Vertical");

        movement = transform.right * movX + transform.forward * movZ;
        characterController.SimpleMove(movement * speedMovement);
    }

    void MovementCamera()
    {
        float rotX = Input.GetAxis("Mouse X") * speedRotation; // Horizontal
        float rotY = Input.GetAxis("Mouse Y") * speedRotation; // Vertical

        // Rotación vertical (cámara)
        rotationX -= rotY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        pcamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // Rotación horizontal (personaje)
        characterTransform.Rotate(Vector3.up * rotX);
    }
}