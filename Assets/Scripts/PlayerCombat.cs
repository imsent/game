using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public GameObject attackPoint;
    public LayerMask enemyLayers;

    public Animator animator;
    
    

    public GameObject startPoint;
    
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
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
                animator.SetBool("isAttack",true);
                break;
            case 2:
                animator.SetBool("isAttack2",true);
                // Выполнить второй удар
                break;
            case >= 3:
                animator.SetBool("isAttack3",true);
                comboCount = 0;
                // Выполнить третий и последующие удары
                break;
        }

        lastAttackTime = Time.time; // Обновляем время последнего удара
    }
    void Attack() {
        
        foreach (var enemy in Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers)) {
            enemy.GetComponent<EnemyCombat>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        //animator.SetTrigger("Hurt");
        //heartGUI.DecreaseHealth();
        if (currentHealth <= 0)
            Die();
        }

    void Die() {
        animator.SetTrigger("death");
        rb.transform.position = startPoint.transform.position;
        
        // Disable the player's movement and input here
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
