using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthFill;

    public PlayerCombat player;

    void Start()
    {
        player.GetComponent<PlayerCombat>();
    }

    void Update()
    {
        healthFill.fillAmount = player.currentHealth / player.maxHealth;
    }
}
