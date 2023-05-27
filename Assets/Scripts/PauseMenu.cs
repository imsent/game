using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject UI;

    public Text stats;

    public GameObject player;
    private PlayerCombat playerHealth;
    private PlayerMove playerStats;

    public Text score;
    
    [SerializeField] private AudioSource selectSound;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        playerHealth = player.GetComponent<PlayerCombat>();
        playerStats = player.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (player.GetComponent<Animator>().GetBool("death")) return;
        switch (pauseMenu.activeSelf)
        {
            case false when Time.timeScale == 1f:
                playerHealth.enabled = false;
                Time.timeScale = 0f;
                UI.SetActive(false);
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                stats.text = @$"
    Ability: {Math.Round(playerHealth.ability,2)} с. (-{playerHealth.abilityPercent}%)
    Health: {Math.Round(playerHealth.maxHealth,2)} (+{playerHealth.hpPercent}%)
    Jump: {Math.Round(playerStats.jump,2)} (+{playerStats.jumpPercent}%)
    Speed: {Math.Round(playerStats.speed,2)} (+{playerStats.speedPercent}%)
    Strength: {Math.Round(playerHealth.attackDamage,2)} (+{playerHealth.strengthPercent}%)
    ";
                score.text = "Очки игрока: " + playerHealth.score;
                break;
            case true:
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                UI.SetActive(true);
                Cursor.visible = false;
                playerHealth.enabled = true;
                break;
        }
    }

    public void quit()
    {
        selectSound.Play();
        Application.Quit();
    }

    public void resume()
    {
        selectSound.Play();
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        UI.SetActive(true);
        Cursor.visible = false;
        playerHealth.enabled = true;
    }
}
