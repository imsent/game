using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public float hpPercent;
    public float maxHealth = 100;
    public float currentHealth;
    public float attackRange = 0.5f;
    public float strengthPercent;
    public float attackDamage = 10;
    public float attackRate = 2f;
    private float nextAttackTime;

    public GameObject attackPoint;
    public LayerMask enemyLayers;

    public Animator animator;
    
    public int kills;

    public float exp;
    public float expMax = 100;

    public int score;

    public GameObject UI;
    public GameObject Death;
    
    public GameObject spawner;


    public Text time;


    
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attack1Sound;
    [SerializeField] private AudioSource attack2Sound;
    [SerializeField] private AudioSource attack3Sound;
    [SerializeField] private AudioSource abilitySound;
    [SerializeField] private AudioSource dieSound;
    [SerializeField] public AudioSource coinSound;
    
    
    public float ability = 60;
    public float abilityPercent;
    public LayerMask abilityLayer;
    private float abilityReset;
    public Text abilityText;
    public Image abilityImage;
    
    private void Start() {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (animator.GetBool("death")) return;
        Ability();
        if (!(Time.time >= nextAttackTime)) return;
        if (!Input.GetButtonDown("Fire1")) return;
        attackManager();
        nextAttackTime = Time.time + 1f / attackRate;
    }
    
    
    public float comboResetTime = 1f;
    
    private int comboCount;
    
    private float lastAttackTime;

    void attackManager()
    {
        if (Time.time - lastAttackTime > comboResetTime)
        {
            comboCount = 0; // Сбросить счетчик комбо, если прошло время, большее чем comboResetTime
        }

        comboCount++; // Увеличить счетчик комбо

        switch (comboCount)
        {
            case 1:
                attack1Sound.Play();
                animator.SetBool("isAttack",true);
                break;
            case 2:
                attack2Sound.Play();
                animator.SetBool("isAttack2",true);
                // Выполнить второй удар
                break;
            case >= 3:
                attack3Sound.Play();
                animator.SetBool("isAttack3",true);
                comboCount = 0;
                // Выполнить третий и последующие удары
                break;
        }

        lastAttackTime = Time.time; // Обновляем время последнего удара
    }
    void Attack() {
        foreach (var enemy in Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers))
        {
            var enemyCombat = enemy.GetComponent<EnemyCombat>();
            enemyCombat.TakeDamage(attackDamage);
            if (!(enemyCombat.currentHealth <= 0)) continue;
            if (!enemy.GetComponent<EnemyMove>().enabled) continue;
            enemyCombat.dieSound.Play();
            killMob(enemy);
        }
    }

    public void killMob(Collider2D mob)
    {
        mob.GetComponent<EnemyMove>().enabled = false;
        kills += 1;
        exp += mob.GetComponent<EnemyCombat>().expkill;
        score += 100;
        Debug.Log(mob.name);
        switch (mob.name)
        {
            case "mob(Clone)":
                spawner.GetComponent<Spawner>().bat1--;
                break;
            case "mob3(Clone)":
                spawner.GetComponent<Spawner>().bat2--;
                break;
            case "mob2(Clone)":
                spawner.GetComponent<Spawner>().bat3--;
                break;
        }
        //spawner.GetComponent<Spawner>().nowTheEnemies -= 1;
    }

    public void TakeDamage(float damage) {
        if (animator.GetBool("death")) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            dieSound.Play();
            animator.GetComponent<PlayerMove>().enabled = false;
            Data._Statistics = $@"Время: {time.text}
Убийств: {kills}
очков: {score}";
            animator.SetBool("death",true);
            return;
        }
        hitSound.Play();
        animator.SetTrigger("isHurt");
        //heartGUI.DecreaseHealth();
        
    }

    void Die()
    {
        SceneManager.LoadScene("Death");
    }
    
    void ActivateAbility()
    {
        foreach (var ability in Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, abilityLayer))
        {
            abilitySound.Play();
            foreach (var enemy in GameObject.FindGameObjectsWithTag("Agressive"))
            {
                enemy.GetComponent<EnemyCombat>().TakeDamage(10000);
                killMob(enemy.GetComponent<Collider2D>());
            }
            currentHealth = maxHealth;
            abilityReset = Time.time;
        }
    }

    void Ability()
    {
        if (abilityReset == 0)
        {
            if (Input.GetKey(KeyCode.E))
                ActivateAbility();
        }
        else
        {
            var timer = ability - (Time.time - abilityReset);
            abilityImage.fillAmount = 1 - timer/ability;
            abilityText.text = timer > 0 ? ((int)timer).ToString() : "";
            if (timer < 0)
            {
                abilityReset = 0;
            }
        }
        

        
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
    
    public void endAttack()
    {
        animator.SetBool("isAttack",false);
        animator.SetBool("isAttack2",false);
        animator.SetBool("isAttack3",false);
    }
}
