using UnityEngine;

public class Hazzard : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    
    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameManager.LoseGame();
        }    
    }
}
