using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth = 50;
    public float currentHealth;
    public float attackRange = 0.5f;
    public float attackDamage = 10;
    public float attackRate = 5f;
    private float nextAttackTime;

    public GameObject attackPoint;
    public LayerMask playerLayer;

    public Animator animator;

    public int expkill;


    public Transform TextSpawn;
    public GameObject damageText;


    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource attackSound;
    [SerializeField] public AudioSource dieSound;

    
    
    public SpriteRenderer healthBar;
    
    public SpriteRenderer backGround;
    private void Start() {
        currentHealth = maxHealth;
        healthBar.size = new Vector2(0, 0);
        backGround.size = new Vector2(0, 0);
    }

    private void Update()
    {
        if (!(Time.time >= nextAttackTime)) return;
        var player = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer);
        if (player == null || animator.GetBool("isDead")) return;
        //animator.SetTrigger("Attack");
        
        animator.SetBool("isAttack",true);
        
        
        
    }

    public void TakeDamage(float damage)
    {
        if (animator.GetBool("IsDead")) return;
        currentHealth -= damage;
        
        var go = Instantiate(damageText, TextSpawn.localPosition, Quaternion.identity);
        go.transform.SetParent(TextSpawn.transform,true);
        go.GetComponent<TMPro.TextMeshPro>().SetText("-"+damage.ToString("F0"));
        go.name = "-" + damage.ToString("F0");
        Destroy(go,0.5f);
        
        if (currentHealth <= 0)
        {
            attackDamage = 0;
            healthBar.GameObject().SetActive(false);
            backGround.GameObject().SetActive(false);
            animator.SetBool("isDead", true);
            return;
        }
        hitSound.Play();
        animator.SetTrigger("Attacked");
        
        //animator.SetBool("isDead", true);
        healthBar.size = new Vector2(currentHealth / maxHealth, 0.8f);
        backGround.size = new Vector2(1, 0.8f);// изменение размера спрайта
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + 1f, transform.parent.position.z); // установка положения спрайта
    }
    
    void Attack()
    {
        attackSound.Play();
        var player = Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer);
        if (player != null)
        player.GetComponent<PlayerCombat>().TakeDamage(attackDamage);
        nextAttackTime = Time.time + 1f / attackRate;
    }
    
    void Die()
    {
        Destroy(animator.GameObject());
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
    
    public void endAttack()
    {
        animator.SetBool("isAttack",false);
    }
}
