using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 1.5f;

    [SerializeField]
    private float roamDistance = 2.0f;

    [SerializeField]
    private float directionChangeTime = 1.0f;

    [Header("Bounce")]
    [SerializeField]
    private float bounceHeight = 0.1f;

    [SerializeField]
    private float bounceSpeed = 3.0f;

    [Header("Visual")]
    [SerializeField]
    private float rotationSpeed = 100f;

    private Rigidbody2D rb;

    private Vector2 startPosition;
    private float moveDirection;
    private float directionTimer;
    private float bounceTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startPosition = transform.position;

        moveDirection = Random.value < 0.5f ? -1f : 1f;

        directionTimer = Random.Range(0.5f, directionChangeTime);

        bounceTime = Random.Range(0f, Mathf.PI * 2f);

        gameManager.RegisterCollectible(this);
    }

    void FixedUpdate()
    {
        MoveCollectible();
    }

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void MoveCollectible()
    {
        directionTimer -= Time.fixedDeltaTime;

        if (directionTimer <= 0f)
        {
            moveDirection = Random.value < 0.5f ? -1f : 1f;

            directionTimer = Random.Range(
                directionChangeTime * 0.5f,
                directionChangeTime
            );
        }

        float horizontalMovement =
            moveDirection * moveSpeed * Time.fixedDeltaTime;

        float newX = rb.position.x + horizontalMovement;

        if (newX > startPosition.x + roamDistance)
        {
            newX = startPosition.x + roamDistance;
            moveDirection = -1f;
        }
        else if (newX < startPosition.x - roamDistance)
        {
            newX = startPosition.x - roamDistance;
            moveDirection = 1f;
        }

        bounceTime += bounceSpeed * Time.fixedDeltaTime;

        float bounceOffset =
            Mathf.Abs(Mathf.Sin(bounceTime)) * bounceHeight;

        float newY = startPosition.y + bounceOffset;

        rb.MovePosition(new Vector2(newX, newY));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {

            gameManager.CollectItem();

            gameObject.SetActive(false);
        }
    }
}