using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{

    [SerializeField]
    [Tooltip("This is the object to Rotate in the Movement Direction")]
    private GameObject root;

    private Animator animator = null;

    private bool isMoving = false;
    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // Root is the object to rotate
        if (!root)
        {
            // If root is not specified in the Editor,
            // Set it to this gameobject
            root = gameObject;
            Debug.LogWarning("Root not assigned!");
            Debug.Log("Root assigned to Self");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved this frame
        if (isMoving)
        {
            // If the player is moving,
            // animate the player to go to Walking animation
            // Also, check if the player is already in walking animation state
            if (!animator.GetBool("IsMoving"))
            {
                // Player was in Idle animation state
                // Set Animator parameter
                animator.SetBool("IsMoving", isMoving);
            }
        }
        else
        {
            // If the player is not moving,
            // animate the player to go to Idle animation
            // Also, check if the player is already in idle animation state
            if (animator.GetBool("IsMoving"))
            {
                // Player was in Walking animation state
                // Set Animator parameter
                animator.SetBool("IsMoving", isMoving);
            }
        }
    }

}
