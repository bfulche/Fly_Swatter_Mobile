using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Game state variables
    public int playerScore;
    public bool isGameplayActive;

    //Display variables for Swatter, Background, and Flies
    public float swatterDisplayTime = 0.2f; //duration of time the swatter is displayed
    public SpriteRenderer swatterRenderer; //reference to the swatter sprite renderer
    public BackgroundItem newBackgroundItem;


    //UI management variables - from old "MainMenuUI" script
    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;
    public GameObject unlockShopPanel;
    public TMP_Text scoreText; //text component used for showing score through all UI panels
    public Button playButton;
    public Button unlockShopButton;
    public Button quitButton;

    // References to other components & scripts
    public FlySpawner2 flySpawner; //script & game object that spawn flies
    public UnlockShop unlockShop; //Placeholder as of now. manages the unlock shop UI? This will probably be folded into this script, but we'll see
    public CosmeticManager cosmeticManager; //Placeholder as of now. script & game object that manages what the player has unlocked and what they currently have "equipped"
    

    // Input handling variables
    public Camera mainCamera;
    public LayerMask flyLayer;

    //Player Preferences and Saved Data variables - Used for saving data across plays
    private const string PlayerScoreKey = "PlayerScore";



    private void Awake()
    {
        // Singleton setup - ensures there is only ever one GameManager in a scene
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }




    private void Start()
    {
        // Initialize game state
        playerScore = 0;
        //isGamePaused = false;
        
        mainMenuPanel.SetActive(true); //activate the Main Menu on startup
        gameplayPanel.SetActive(false); //set gameplay to false at the start of the game
        isGameplayActive = false; //set the gameplay to false so that the player can not tap on flies while Main Menu is active

        //check if scoreText is assigned and update the score UI
        if (scoreText != null)
        {
            UpdateScoreText();
        }
        else
        {
            Debug.LogError("scoreText is not assignmed in Inspector");
        }

        //get main camera
        mainCamera = Camera.main;


        //THIS CODE FROM VRESION 1, USING CHATGPT. COPIED FROM "TapInput" SCRIPT (5/28)
        //----------------------------------------------------------------------------------------------------------
        //Check if the fly coin balance is already stored in PlayerPrefs
        if (PlayerPrefs.HasKey(PlayerScoreKey))
        {
            //Retrieve the fly coin balance from PlayerPrefs
            playerScore = PlayerPrefs.GetInt(PlayerScoreKey);
        }
        else
        {
            //Create the fly coin balance in PlayerPrefs with an initial value of zero
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") +1);
        }

        UpdateScoreText();
        //----------------------------------------------------------------------------------------------------------

        //swatterRenderer = GameObject.FindGameObjectWithTag("Swatter").GetComponent<SpriteRenderer>(); //get the sprite renderer for the fly swatter
    }




    private void Update()
    {
        // Check if the game is in gameplay mode and not paused
        if (isGameplayActive /*&& !isGamePaused*/)
        {
            HandleTapInput();
        }

        UpdateScoreText(); //Constantly update the player's score in the UI text component
}


    private void UpdateScoreText() //Used to update the UI text display to show the player's score
    {
        scoreText.text = playerScore.ToString();
    }


    private void HandleTapInput()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Vector3 tapPosition = GetTapPosition();

            ShowSwatterSprite(tapPosition); //show the swatter at the tapped position

            //Display the swatter sprite at the tap position 
            /*Vector3 worldPosition = Camera.main.ScreenToWorldPoint(tapPosition);
            worldPosition.z = 0f;
            swatterRenderer.transform.position = worldPosition;
            swatterRenderer.sprite = cosmeticManager.equippedSwatter.swatterSprite;
            swatterRenderer.enabled = true;
            Invoke("HideSwatterSprite", swatterDisplayTime);*/

            Collider2D[] colliders = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(tapPosition));

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Fly"))
                {
                    Destroy(collider.gameObject);
                    IncreaseScore();
                    break;
                }
            }
        }
    }

    private Vector3 GetTapPosition()
    {
        Vector3 tapPosition = Vector3.zero;

        if (Input.touchCount > 0)
        {
            tapPosition = Input.GetTouch(0).position;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            tapPosition = Input.mousePosition;
        }

        return tapPosition;
    }

    private void ShowSwatterSprite(Vector3 position) //show the swatter sprite that has been determined in the Cosmetic Manager
    {
        if (cosmeticManager == null)
        {
            Debug.LogError("CosmeticManager reference is not assigned!");
            return;
        }

        if (cosmeticManager.equippedSwatter == null)
        {
            Debug.LogError("EquippedSwatter is not assigned in CosmeticManager!");
            return;
        }

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        worldPosition.z = 0f;

        GameObject swatterObject = new GameObject("SwatterSprite");
        swatterObject.transform.position = worldPosition;

        SpriteRenderer swatterRenderer = swatterObject.AddComponent<SpriteRenderer>();
        swatterRenderer.sprite = cosmeticManager.equippedSwatter.swatterSprite;

        Destroy(swatterObject, swatterDisplayTime);
    }

    private void HideSwatterSprite() //quick method to disable the swatter sprite after the screen has been tapped
    {
        swatterRenderer.enabled = false;
    }



    public void IncreaseScore() //increases the player's score in the UI by a specific number of points 
    {
        playerScore++;
        UpdateScoreText();
    }




    private void LoadGameData()
    {
        // Load player score
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);

        // Load unlocked cosmetics
        // Implement loading logic for unlocked cosmetics
    }

    private void SaveGameData()
    {
        // Save player score
        PlayerPrefs.SetInt("PlayerScore", playerScore);

        // Save unlocked cosmetics
        // Implement saving logic for unlocked cosmetics
    }




    public void toGameplay() //Used to turn on gameplay and deactivate the Main Menu and Unlock Shop UI panels. Used for buttons that go to gameplay
    {
        mainMenuPanel.SetActive(false);
        unlockShopPanel.SetActive(false);
        gameplayPanel.SetActive(true);
        isGameplayActive = true;
    }

    public void toMainMenu() //deactivate gameplay and go to the Main Menu
    {
        mainMenuPanel.SetActive(true);
        unlockShopPanel.SetActive(false);
        gameplayPanel.SetActive(false);
        isGameplayActive = false;

    }

    public void toUnlockShop() //deactivate gameplay and go to the Unlock Shop
    {
        mainMenuPanel.SetActive(false);
        unlockShopPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        isGameplayActive = false;
    }

    public void QuitGame() //quit game method, to be used with the Quit button on the Main Menu
    {
        SaveGameData();
        Application.Quit();
    }
}
