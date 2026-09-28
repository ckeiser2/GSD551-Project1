using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private Rigidbody2D slimeRigidbody;

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private Transform leftGroundCheck;

    [SerializeField]
    private Transform rightGroundCheck;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float groundCheckDistance = 0.2f;

    private int direction = 1;

    void FixedUpdate()
    {
        // Check for ground on each side
        bool leftGrounded = Physics2D.Raycast(
            leftGroundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        bool rightGrounded = Physics2D.Raycast(
            rightGroundCheck.position,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        // Turn if groundchecks don't find ground
        if (direction == 1 && !rightGrounded)
        {
            direction = -1;
        }
        else if (direction == -1 && !leftGrounded)
        {
            direction = 1;
        }

        // Move using Rigidbody2D
        slimeRigidbody.linearVelocity = new Vector2(
            direction * movementSpeed,
            slimeRigidbody.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Player")) 
        { 
            Debug.Log("Player touched the slime!"); 
            gameManager.LoseGame(); 
        } 
    }
}