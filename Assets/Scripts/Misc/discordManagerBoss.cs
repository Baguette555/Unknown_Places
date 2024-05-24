using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class discordManagerBoss : MonoBehaviour
{
    Discord.Discord discord;

    public PauseMenu pauseMenu;
    public HealthManagerBoss healthManagerBoss;

    private int lastLevelInt = -1; // Détecter les changements de niveau pour drp
    private int lastHealth = -1; // Détecter les changements de vie pour drp

    private int frameCounter = 0;

    /*private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);     // Ne marche pas : Reload la scène à l'infini
    }*/

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
        if (pauseMenu.levelInt != lastLevelInt || healthManagerBoss.playerHealth != lastHealth)
        {
            lastLevelInt = pauseMenu.levelInt;
            lastHealth = healthManagerBoss.playerHealth;

            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                State = "Niveau " + pauseMenu.levelTxt,
                Details = "Chapitre " + pauseMenu.chapterInt,
                Assets =
            {
                LargeImage = "grandeimagevisageneutre",
                LargeText = "Vies : " + healthManagerBoss.playerHealth
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
