using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Animation Controller, which animates the Player Character")]
    private AnimationController animationController = null;

    [Header("Movement Keys")]
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveRight = KeyCode.D;
    public KeyCode moveLeft = KeyCode.A;

    [Header("Player Movement Properties")]

    [SerializeField]
    [Tooltip("Specifies if player is allowed move or not")]
    private bool canMove = true;
    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    [SerializeField]
    [Tooltip("Speed at which the player moves (also the Minimum speed)")]
    private float moveSpeed = 3f;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [SerializeField]
    [Tooltip("Maximum Speed at which the player moves (will never be less than Minimum Speed)")]
    private float maxSpeed = 8f;
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }

    [SerializeField]
    [Tooltip("Speed at which the player character rotates")]
    private float rotationSpeed = 5f;
    public float RotationSpeed
    {
        get => rotationSpeed;
        set => rotationSpeed = value;
    }

    // Minimum speed of the Player
    private float minSpeed = 3f;
    
    // Player should move this frame or not
    private bool shouldMove = false;

    // Direction in which the player should move
    private Vector3 moveDirection = Vector3.zero;

    // The Unity Character Controller Component
    private CharacterController characterController = null;
    // Reference to Player Info class
    private PlayerInfo playerInfo = null;


    // Start is called before the first frame update
    void Start()
    {
        // Get reference to Character Controller Component
        characterController = GetComponent<CharacterController>();
        // Get reference to Player Info Component
        if (GetComponent<PlayerInfo>())
        {
            playerInfo = GetComponent<PlayerInfo>();
        }
        else if (!playerInfo)
        {
            Debug.LogError("PlayerInfo not found!");
        }
        // Check if Animation Controller is assigned in the Editor
        if (!animationController)
        {
            Debug.LogError("Animation Controller not assigned!");
            return;
        }
        // Set Minimum Speed
        minSpeed = MoveSpeed;
        // Check if Maximum Speed is less than Minimum Speed
        if (MaxSpeed <= MoveSpeed)
        {
            // Maximum Speed should never be less than or equal to Minimum Speed
            MaxSpeed = MoveSpeed + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Check if Player released any of the Movement Keys in this frame
        if (Input.GetKeyUp(moveForward))
        {
            shouldMove = false;
        }
        if (Input.GetKeyUp(moveBackward))
        {
            shouldMove = false;
        }
        if (Input.GetKeyUp(moveRight))
        {
            shouldMove = false;
        }
        if (Input.GetKeyUp(moveLeft))
        {
            shouldMove = false;
        }

        // Check if the player is allowed to move
        if (CanMove)
        {
            // Check if Player pressed any of the Movement Keys in this frame
            // and flag if the player should move or not
            if (Input.GetKey(moveForward))
            {
                // Move Forward
                moveDirection += Vector3.forward;
                shouldMove = true;
            }
            if (Input.GetKey(moveBackward))
            {
                // Move Backwards
                moveDirection += Vector3.back;
                shouldMove = true;
            }
            if (Input.GetKey(moveRight))
            {
                // Move Right
                moveDirection += Vector3.right;
                shouldMove = true;
            }
            if (Input.GetKey(moveLeft))
            {
                // Move Left
                moveDirection += Vector3.left;
                shouldMove = true;
            }
        }
        // Apply gravity
        moveDirection.y -= 1 * Time.deltaTime;
        // Set animtion script variable to animate between idle and walking
        animationController.IsMoving = shouldMove;
    }

    private void LateUpdate()
    {
        // Check if the Player is allowed to Move or not
        if (canMove)
        {
            // Only move if the player is Grounded
            if (characterController.isGrounded)
            {
                if (shouldMove)
                {
                    // If player has pressed any movement keys, then move the Player
                    // Also, normalize Movement Vector so that movement speed remains constant
                    // even while moving diagonally 
                    characterController.Move(moveDirection.normalized * MoveSpeed * Time.deltaTime);
                    // Calculate Look Rotation for the Player to face the direction in which the player is moving
                    Quaternion rotation =
                        Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
                    // Apply rotation
                    transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
                    //Debug.Log(moveDirection);

                    // Reset Movement Vector
                    moveDirection = Vector3.zero;
                }
            }
            // If not Grounded, apply gravity
            else
            {
                // Apply gravity in Y-Axis
                moveDirection.y -= 1 * Time.deltaTime;
                // Move player in the Gravity Direction
                characterController.Move(moveDirection * Time.deltaTime);
                Debug.Log(playerInfo.playerNum + ": Not grounded!");
            }
        }
    }
}
