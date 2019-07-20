using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(Rigidbody))]
public class PlayerFPS : MonoBehaviour
{
    PlayerInfo player;
    Rigidbody rb;

    public Transform playerCamera;
    public float lookSensitivity = 3.0f, moveSpeed = 5.0f, airMovePercent = 0.35f, gravity = 15.0f;
    public bool grounded;
    public float jumpForce = 5.0f;

    
    void Awake()
    {
        LoadPlayer();
        LockCursor();
    }

    void Update()
    {
        if (player != null)
        {
            RotatePlayer();
            MovePlayer();
            JumpPlayer();
        }
    }

    void OnTriggerEnter(Collider other) { grounded = true; velocityAtJump = Vector3.zero; }
    void OnTriggerStay(Collider other) { grounded = true; }
    void OnTriggerExit(Collider other) { grounded = false; }


    #region Player Input Functions
    
    void LoadPlayer()
    {
        player = LocalPlayers.players[0];
        rb = GetComponent<Rigidbody>();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    float verticalClamp = 0.0f;
    Vector2 deltaLook = Vector2.zero;
    void RotatePlayer()
    {
        deltaLook = new Vector2(player.GetAxis("LookHorizontal"), player.GetAxis("LookVertical"));
        deltaLook = deltaLook * lookSensitivity;
        
        verticalClamp += deltaLook.y;
        
        if (verticalClamp > 90.0f)
        { 
            verticalClamp = 90.0f; 
            deltaLook.y = 0;
            ClampCamera(270.0f);
        }
        if (verticalClamp < -90.0f)
        {
            verticalClamp = -90.0f;
            deltaLook.y = 0;
            ClampCamera(90.0f);
        }
        
        playerCamera.Rotate(Vector3.left, deltaLook.y);
        transform.Rotate(Vector3.up, deltaLook.x, Space.Self);
    }

    void ClampCamera(float clampValue)
    {
        Vector3 clampedRotation = playerCamera.eulerAngles;
        clampedRotation.x = clampValue;
        playerCamera.eulerAngles = clampedRotation;
    }

    Vector3 velocityAtJump = Vector3.zero;
    void MovePlayer()
    {
        Vector3 inputVector3 = player.GetVector3("MoveHorizontal", "MoveVertical");
        
        if (inputVector3 != Vector3.zero)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            
            if (grounded)
            {
                localVelocity.x = inputVector3.x * moveSpeed;
                localVelocity.z = inputVector3.z * moveSpeed;
            }
            else
            {
                localVelocity.x = (velocityAtJump.x * (1.0f - airMovePercent)) +  ((inputVector3.x * moveSpeed) * airMovePercent);
                localVelocity.z = (velocityAtJump.z * (1.0f - airMovePercent)) +  ((inputVector3.z * moveSpeed) * airMovePercent);
            }
            
            rb.velocity = transform.TransformDirection(localVelocity);
        }
        
        rb.AddForce(new Vector3 (0, -gravity * rb.mass, 0));
    }

    void JumpPlayer()
    {
        if (player.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector3(0, jumpForce * 100, 0));
            velocityAtJump = transform.InverseTransformDirection(rb.velocity);
        }
    }
    #endregion
}
