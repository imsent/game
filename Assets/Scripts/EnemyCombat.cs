using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth = 50;
    public float currentHealth;
    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public float attackRate = 5f;
    private float nextAttackTime = 0f;

    public GameObject attackPoint;
    public LayerMask playerLayer;

    public Animator animator;

    
    
    public SpriteRenderer healthBar;
    public SpriteRenderer backGround;
    private void Start() {
        currentHealth = maxHealth;
        healthBar.size = new Vector2(0, 0);
        backGround.size = new Vector2(0, 0);
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime) {
            var player = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer);

            if (player != null) {
                //animator.SetTrigger("Attack");

                player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        animator.SetTrigger("Attacked");
        healthBar.size = new Vector2(currentHealth / maxHealth, 0.8f);
        backGround.size = new Vector2(1, 0.8f);// изменение размера спрайта
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z); // установка положения спрайта
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        animator.SetBool("isDead", true);

        // Disable the enemy's movement and input here
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}
