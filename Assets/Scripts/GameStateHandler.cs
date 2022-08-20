using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
    public Settings settings;
    public PlayerStats playerStats;
    public SpawnObstacles obstacleSpawner;
    public GameObject menuUI;
    public GameObject gameUI;
    public GameObject settingsUI;
    public GameObject creditsUI;
    public ShipRotation ShipRotation;

    public bool StartInGame = true;

    private Settings coreSettings; // The basic settings the game refers to
    private PlayerStats coreStats; // The basic settings the game refers to

    public bool isIngame;

    // Start is called before the first frame update
    void Start()
    {
        //ShipRotation.currentFloor = 0;
        menuUI.SetActive(!StartInGame);
        gameUI.SetActive(StartInGame);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        coreSettings = GameObject.Instantiate(settings);
        coreStats = GameObject.Instantiate(playerStats);
        isIngame = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isIngame && !playerStats.GameOver)
        {
            settings.Speed = settings.baseSpeed * settings.TimeMultiplier;
            if (settings.TimeMultiplier < settings.MaxTimeMultiplier)
                settings.TimeMultiplier += settings.MultiplierGrowth / 50.0f;
            settings.TransitionSpeed = settings.BaseTransitionSpeed * (settings.Speed / settings.baseSpeed);
            settings.AllowMovement = true;
        } else
        {
            settings.AllowMovement = false;
        }
        if (isIngame && Input.GetKey(KeyCode.Escape))
        {
            ToMainMenu();
        }
    }

    public void StartGame()
    {

        settings.ResetSettings(coreSettings);
        playerStats.ResetStats(coreStats);
        obstacleSpawner.ResetObstacles();
        gameUI.GetComponentInChildren<PlayerStatsDisplay>().GameOverScreen.SetActive(false);
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        creditsUI.SetActive(false);
        settingsUI.SetActive(false);
        isIngame = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ToMainMenu()
    {
        menuUI.SetActive(true);
        gameUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        obstacleSpawner.ResetObstacles();
        isIngame = false;
        settings.Speed = settings.baseSpeed;
    }

    public void OpenSettings()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        settingsUI.SetActive(true);
        creditsUI.SetActive(false);
        obstacleSpawner.ResetObstacles();
        isIngame = false;
        settings.Speed = settings.baseSpeed;
    }

    public void MogusMode()
    {
        System.Diagnostics.Process process = new System.Diagnostics.Process();
        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/c start https://static.wikia.nocookie.net/meme/images/0/07/Amogus_Template.png/revision/latest?cb=20210308145830";
        process.StartInfo = startInfo;
        process.Start();
    }

    public void ShowCredits()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(true);
        obstacleSpawner.ResetObstacles();
        isIngame = false;
        settings.Speed = settings.baseSpeed;
    }
}
