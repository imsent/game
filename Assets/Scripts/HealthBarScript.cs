using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerCombat playerHealth;
    private PlayerMove playerStats;
    public GameObject player;

    public Text money;
    public Text kills;
    public Image healthFill;

    
    public Text Lvl;
    public int lvl = 0;
    public Image expFill;
    public GameObject levelUP;

    public GameObject selectBonus;


    public Transform TextSpawn;
    public GameObject TextUp;
    [SerializeField] private AudioSource error;
    void Start()
    {
        playerHealth = player.GetComponent<PlayerCombat>();
        playerStats = player.GetComponent<PlayerMove>();
        Lvl.text = lvl.ToString();
    }

    void Update()
    {
        HpMoneyKills();
        Exp();
    }

    private void HpMoneyKills()
    { 
        healthFill.fillAmount = playerHealth.currentHealth / playerHealth.maxHealth;
        money.text = playerStats.coins.ToString();
        kills.text = playerHealth.kills.ToString();
    }

    private void Exp()
    {
        levelUP.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
        expFill.fillAmount = playerHealth.exp / playerHealth.expMax;
        if (expFill.fillAmount >= 1 && levelUP.activeSelf == false)
        {
            levelUP.SetActive(true);
        }

        if (!Input.GetKeyDown(KeyCode.N) || !levelUP.activeSelf) return;
        if (playerStats.coins < 2)
        {
            error.Play();
            var go = Instantiate(TextUp, TextSpawn.localPosition, Quaternion.identity);
            go.transform.SetParent(TextSpawn.transform,true);
            go.GetComponent<TMPro.TextMeshPro>().SetText("Вам не хватает "+ (2-playerStats.coins) + " монет.");
            go.GetComponent<TMPro.TextMeshPro>().fontSize = 4;
            go.name = "no money";
            Destroy(go,0.5f);
            return;
        }
        levelUP.SetActive(false);
        selectBonus.SetActive(true);
        playerHealth.exp = 0;
        playerHealth.expMax += 50;
        playerStats.coins -= 2;
        lvl += 1;
        Lvl.text = lvl.ToString();
    }
}
