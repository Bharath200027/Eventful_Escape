using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    public bool IsInteracted;

    private PlayerView playerView;
    private PlayerScriptableObject playerScriptableObject;
    private float velocity;
    private float horizontalAxis;
    private float verticalAxis;
    private PlayerState playerState;

    public PlayerController(PlayerView playerView, PlayerScriptableObject playerScriptableObject)
    {
        this.playerView = playerView;
        this.playerView.SetController(this);

        this.playerScriptableObject = playerScriptableObject;
        this.playerScriptableObject.KeysEquipped = 0;

        this.playerState = PlayerState.InDark;
    }

    public void Interact()
    {
        // simplify
        // plus something doesn't make logical sense in this logic
        IsInteracted = Input.GetKeyDown(KeyCode.E) ? true : false;
        if (Input.GetKeyUp(KeyCode.E))
        {
            IsInteracted = false;
        }
    }

    public void Jump(Rigidbody playerRigidbody, Transform transform)
    {
        bool IsGrounded = Physics.Raycast(transform.position, -transform.up, playerScriptableObject.raycastLength);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * playerScriptableObject.jumpForce, ForceMode.Impulse);
        }
    }

    public void Move(Rigidbody playerRigidbody, Transform transform)
    {
        GetInput();

        // why are we getting mouse x here and horizontal/vertical in a function call?
        Quaternion rotation = playerRigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * playerScriptableObject.sensitivity, 0));
        Vector3 position = transform.position + Time.fixedDeltaTime * velocity * (transform.forward * verticalAxis + transform.right * horizontalAxis);

        playerRigidbody.MoveRotation(rotation);
        playerRigidbody.MovePosition(position);
    }

    public int KeysEquipped { get => playerScriptableObject.KeysEquipped; set => playerScriptableObject.KeysEquipped = value; }

    public PlayerState PlayerState { get => playerState; private set => playerState = value; }

    private void GetInput()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        velocity = Input.GetKey(KeyCode.LeftShift) ? playerScriptableObject.sprintSpeed : playerScriptableObject.walkSpeed;
    }
}
