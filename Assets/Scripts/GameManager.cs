using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private int currentCollectibles = 0;
    private bool gameEnded = false;
    private Vector3 playerStartPosition = new Vector3(0, 0, 0);
    private List<Collectible> collectibles = new List<Collectible>(); 

    [SerializeField]
    private TextMeshProUGUI collectableText;

    [SerializeField]
    private CanvasGroup YouWinGoalUI;

    [SerializeField] 
    private CanvasGroup YouLoseGoalUI;
    
    [SerializeField]
    private GameObject GameplayRoot;
    
    [SerializeField]
    private int requiredCollectibles = 3;

    [SerializeField]
    private PlayerMovement Player;

    [SerializeField]
    private Rigidbody2D PlayerRigidBody;
    

    private void Start()
    {
        playerStartPosition = Player.transform.position;
        
        YouWinGoalUI.gameObject.SetActive(false);
        YouWinGoalUI.alpha = 0;
        YouLoseGoalUI.gameObject.SetActive(false);
        YouLoseGoalUI.alpha = 0;
        GameplayRoot.SetActive(true);
        UpdateCollectibleText();
    }

    public void CollectItem()
    {
        // + 1
        currentCollectibles++;
        UpdateCollectibleText();

        if (currentCollectibles >= requiredCollectibles)
        {
            EndGame(YouWinGoalUI);
        }
    }

    private void UpdateCollectibleText()
    {
        collectableText.text = "Collectibles: " + currentCollectibles + "/" + requiredCollectibles;
        
    }

    public void LoseGame()
    {
        EndGame(YouLoseGoalUI);
    }
    
    private void EndGame(CanvasGroup resultsPanel)
    {
        if (gameEnded == true)
        {
            return;
        }

        gameEnded = true;
        SetPlayerGameplay(enable: false);
        SetUIState(resultsPanel, enabled: true);
        resultsPanel.alpha = 1;
        resultsPanel.gameObject.SetActive(true);
    }

    private void SetUIState(CanvasGroup panel, bool enabled)
    {
        
        if (enabled)
        {
            panel.alpha = 1;
            
        }
        else
        {
            panel.alpha = 0;
        }

        panel.gameObject.SetActive(enabled);
    }

    private void SetPlayerGameplay(bool enable)
    {
        Player.enabled = enable;
        PlayerRigidBody.simulated = enable;
    }

    public void ResetGameplay()
    {
        gameEnded = false;
        currentCollectibles = 0;
        Player.transform.position = playerStartPosition;
        SetPlayerGameplay(enable: true);
        foreach (Collectible c in collectibles)
        {
            c.gameObject.SetActive(true);
        }

        UpdateCollectibleText();
        SetUIState(YouWinGoalUI, enabled: false);
        SetUIState(YouLoseGoalUI, enabled: false);

    }

    public void RegisterCollectible(Collectible collectible)
    {
        collectibles.Add(collectible);
    }
}
