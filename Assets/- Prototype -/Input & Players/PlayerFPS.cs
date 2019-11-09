using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerFPS : MonoBehaviour
{
	PlayerInfo player;
	Rigidbody rb;

	[Header("Camera Settings")]
	public Transform playerCamera;
	public float lookSensitivity = 3.0f;
	private bool _cameraZoomed; public bool cameraZoomed
	{
		get { return _cameraZoomed; }
		set
		{
			Camera playerCameraComponent = playerCamera.GetComponent<Camera>();
			if (value) { playerCameraComponent.fieldOfView = zoomFOV; }
			else { playerCameraComponent.fieldOfView = normalFOV; }
			_cameraZoomed = value;
		}
	}
	public float normalFOV = 60f, zoomFOV = 30f;

	[Header("Movement Settings")]
	public float moveSpeed = 5.0f;
	public float gravity = 15.0f;
	public float jumpForce = 5.0f;
	public float airMovePercent = 0.35f;
	[ReadOnly] public bool grounded;


	void Awake()
	{
		player = GetComponent<PlayerMaster>().LoadPlayer();
		rb = GetComponent<Rigidbody>();
		LockCursor();
	}

	void Update()
	{
		if (player != null)
		{
			PlayerLook();
			PlayerZoom();
			PlayerMove();
			PlayerJump();
		}
	}

	void OnTriggerEnter(Collider other) { grounded = true; velocityAtJump = Vector3.zero; }
	void OnTriggerStay(Collider other) { grounded = true; }
	void OnTriggerExit(Collider other) { grounded = false; }


	#region Player Input Functions

	void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	float verticalClamp = 0.0f;
	Vector2 deltaLook = Vector2.zero;
	void PlayerLook()
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

	void PlayerZoom()
	{
		if (player.GetButtonDown("ToggleZoom"))
		{
			cameraZoomed = !cameraZoomed;
		}
	}

	Vector3 velocityAtJump = Vector3.zero;
	void PlayerMove()
	{
		Vector3 inputVector3 = player.GetVector3("MoveHorizontal", "MoveVertical");

		if (inputVector3 != Vector3.zero)
		{
			Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

			Vector3 addVelocityFromSub = Vector3.zero;
			if (GetComponent<RigidbodyChild>() != null && GetComponent<RigidbodyChild>().parentRigidbody != null) { addVelocityFromSub = GetComponent<RigidbodyChild>().parentRigidbody.velocity; }

			if (grounded)
			{
				localVelocity.x = inputVector3.x * moveSpeed;
				localVelocity.z = inputVector3.z * moveSpeed;
			}
			else
			{
				localVelocity.x = (velocityAtJump.x * (1.0f - airMovePercent)) + ((inputVector3.x * moveSpeed) * airMovePercent);
				localVelocity.z = (velocityAtJump.z * (1.0f - airMovePercent)) + ((inputVector3.z * moveSpeed) * airMovePercent);
			}

			addVelocityFromSub.y = 0f;

			rb.velocity = (transform.TransformDirection(localVelocity) + addVelocityFromSub);
		}

		rb.AddForce(new Vector3(0, -gravity * rb.mass, 0));
	}

	void PlayerJump()
	{
		if (player.GetButtonDown("Jump") && grounded)
		{
			rb.AddForce(new Vector3(0, jumpForce * 100, 0));
			velocityAtJump = transform.InverseTransformDirection(rb.velocity);
		}
	}
	#endregion
}
