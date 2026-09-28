using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float smoothSpeed = 5f;

    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No GameObject with the 'Player' tag was found.");
        }
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}