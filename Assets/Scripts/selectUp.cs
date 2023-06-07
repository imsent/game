using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class selectUp : MonoBehaviour
{
    public GameObject UI;

    public GameObject selectUpMenu;

    public GameObject player;
    private PlayerCombat playerHealth;
    private PlayerMove playerStats;

    private float hpPercent;
    private float jumpPercent;
    private float speedPercent;
    private float strengthPercent;
    private float abilityPercent;

    public Text abilityText;
    public Text hpText;
    public Text jumpText;
    public Text speedText;
    public Text strengthText;
    
    [SerializeField] private AudioSource upSound;
    [SerializeField] private AudioSource selectSound;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = player.GetComponent<PlayerCombat>();
        playerStats = player.GetComponent<PlayerMove>();
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
        upSound.Play();
        UI.SetActive(false);
        Cursor.visible = true;
        random();
        Debug.Log("dsadsa");
    }

    void random()
    {
        var rnd = new System.Random();
        hpPercent = rnd.Next(15, 51);
        jumpPercent = rnd.Next(15, 31);
        speedPercent = rnd.Next(15, 31);
        strengthPercent = rnd.Next(15, 61);
        abilityPercent = rnd.Next(1, 11);

        hpText.text = "Прибавляет к здоровью " + hpPercent + "%";
        jumpText.text = "Прибавляет к силе прыжка " + jumpPercent + "%";
        speedText.text = "Вы становитесь быстрее на " + speedPercent + "%";
        strengthText.text = "Вы получаете прибавку к силе! +" + strengthPercent + "%";
        abilityText.text = "Ускоряет восстановление способности на " + abilityPercent + "%";

    }
    

    // Update is called once per frame

    public void Ability()
    {
        playerHealth.ability = playerHealth.ability / (100 - playerHealth.abilityPercent) *
                               (100 - playerHealth.abilityPercent - abilityPercent);
        playerHealth.abilityPercent += abilityPercent;
    }

    public void Health()
    {
        playerHealth.maxHealth = playerHealth.maxHealth / (100 + playerHealth.hpPercent) *
                                 (100 + playerHealth.hpPercent + hpPercent);
        playerHealth.hpPercent += hpPercent;
    }

    public void Jump()
    {
        playerStats.jump = playerStats.jump / (100 + playerStats.jumpPercent) *
                              (100 + playerStats.jumpPercent + jumpPercent);
        playerStats.jumpPercent += jumpPercent;
    }

    public void Speed()
    {
        playerStats.speed = playerStats.speed / (100 + playerStats.speedPercent) *
                              (100 + playerStats.speedPercent + speedPercent);
        playerStats.speedPercent += speedPercent;
    }

    public void Strength()
    {
        playerHealth.attackDamage = playerHealth.attackDamage / (100 + playerHealth.strengthPercent) *
                                    (100 + playerHealth.strengthPercent + strengthPercent);
        playerHealth.strengthPercent += strengthPercent;
    }

    public void Exit()
    {
        playerHealth.currentHealth = playerHealth.maxHealth;
        selectSound.Play();
        Cursor.visible = false;
        UI.SetActive(true);
        Time.timeScale = 1f;
        selectUpMenu.SetActive(false);
    }
}
