using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public static Level instance;

    uint numDestuctable = 0; // Counter for the number of destructible objects (e.g., enemies)
    bool startNextLevel = false; // Flag indicating whether to start the next level
    float nextLevelTimer = 5; // Timer for the level transition

    string[] levels = { "Level1", "Level2", "Level3", "BossKampf" }; // Array containing the level names to load
    int currentLevel = 1; // Current level

    int score = 0;
    TMP_Text scoreText;

    public PlayerMovement player;

    private void Awake()
    {
        // Singleton check: If no instance exists, create one
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevents object from being destroyed when changing scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Register the scene loaded event
            scoreText = GameObject.Find("scoreText").GetComponent<TMP_Text>(); // Find the score UI text
            Debug.Log("Level-Manager erstellt.");
        }
        else
        {
            Debug.Log("Doppelte Level-Manager-Instanz entdeckt, wird zerstört.");
            Destroy(gameObject); // Destroy duplicate instance if it exists
        }
    }
    void Update()
    {
        // Check if the level transition should start
        if (startNextLevel)
        {
            // Safety check: Only allow level transition if there are no more enemies
            if (numDestuctable > 0)
            {
                Debug.LogError("Fehler: startNextLevel aktiviert, obwohl noch Gegner übrig sind!");
                startNextLevel = false;
                return;
            }

            // Countdown the timer for level transition
            Debug.Log($"Levelwechsel in: {nextLevelTimer} Sekunden");
            if (nextLevelTimer <= 0)
            {
                currentLevel++; // Move to the next level
                if (currentLevel <= levels.Length) 
                {
                    Debug.Log($"Lade Level {currentLevel}");
                    string sceneName = levels[currentLevel - 1]; // Select the level name from the array
                    SceneManager.LoadSceneAsync(sceneName); // Load the next level asynchronously

                    // Reset variables
                    numDestuctable = 0;
                    startNextLevel = false;
                    nextLevelTimer = 5;
                }
                else
                {
                    Debug.Log("Keine weiteren Level verfügbar. Spiel beendet.");
                    startNextLevel = false;
                }
            }
            else
            {
                nextLevelTimer -= Time.deltaTime; // Decrease the timer using DeltaTime
            }
        }
    }

    // Function to reset the level (e.g., after restarting the game)
    
    public void ResetLevel()
    {
        numDestuctable = 0; 
        startNextLevel = false; 
         score = 0;
        currentLevel = 1;
        
        if (scoreText != null)
        {
            scoreText.text = "0";
        }

        Debug.Log("Level-Manager: Hard Reset durchgeführt.");
    }

    // Logic for respawning enemies in Level 1
    void RespawnEnemiesForLevel1()
    {
        // Find all enemies with the "Enemy" tag and add them to the destructible counter
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            AddDestuctable(); // Add enemies to the counter
        }
    }
    private void OnEnable()
    {
        // register the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // When a new scene is loaded, reset the destructible counter and start counting after a short delay
        numDestuctable = 0;
        startNextLevel = false;
        nextLevelTimer = 5f;
        
       
    }

    void CountEnemiesAfterDelay()
    {
        numDestuctable = 0; 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            AddDestuctable();
        }
        Debug.Log($"Zählung abgeschlossen: {numDestuctable} Gegner gefunden.");
    }
    // Function to add points to the score
    public void AddScore(int amountToAdd)
    {
        score += amountToAdd; //Increase Score
        scoreText.text = score.ToString(); //Update the UI Score Text
    }
    public int GetScore()
    {
        return score; // Return the current score
    }

    // Function to increase the number of destructible objects
    public void AddDestuctable()
    {
        numDestuctable++;  // Increase the counter
        Debug.Log($"Gegner hinzugefügt. numDestuctable: {numDestuctable}");
    }

    // Function to decrease the number of destructible objects
    public void RemoveDestuctable()
    { 
        numDestuctable--; // Decrease the counter
        Debug.Log($"Gegner entfernt. numDestuctable: {numDestuctable}");

        // If no enemies are left, initiate level change
        if (numDestuctable <= 0)
        {
            numDestuctable = 0; // Safety measure in case the value goes negative
            startNextLevel = true; // Start the level change
            Debug.Log("Alle Gegner zerstört. Levelwechsel wird vorbereitet.");
        }
    }
    
}


