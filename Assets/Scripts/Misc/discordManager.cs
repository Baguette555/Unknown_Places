using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class discordManager : MonoBehaviour
{
    Discord.Discord discord;

    public PauseMenu pauseMenu;
    public HealthManager healthManager;

    private int lastLevelInt = -1; // Détecter les changements de niveau pour drp
    private int lastHealth = -1; // Détecter les changements de vie pour drp

    private int frameCounter = 0;

    void Start()
    {
        discord = new Discord.Discord(1233671946621943839, (ulong)Discord.CreateFlags.NoRequireDiscord);
        SceneManager.sceneLoaded += OnSceneLoaded;
        ChangeActivity();
    }

    private void OnDisable()
    {
        discord.Dispose();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeActivity();
    }

    public void ChangeActivity()
    {
        if (pauseMenu.levelInt != lastLevelInt || healthManager.playerHealth != lastHealth)
        {
            lastLevelInt = pauseMenu.levelInt;
            lastHealth = healthManager.playerHealth;

            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                State = "Niveau " + pauseMenu.levelInt,
                Details = "Chapitre " + pauseMenu.chapterInt,
                Assets =
            {
                LargeImage = "grandeimagevisageneutre",
                LargeText = "Vies : " + healthManager.playerHealth
            },
                Timestamps =
            {
                Start = 0
            }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                Debug.Log("Activity updated !");
            });
        }
    }
    public void updateActivity()
    {
        ChangeActivity();
    }

    void Update()
    {
        if (frameCounter % 10 == 0)
        {
            discord.RunCallbacks();
        }
        frameCounter++;
    }
}
