using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{ 
    private Transform target; // Цель преследования (игрок)
    public float speed = 5f; // Скорость передвижения
    
    
    public float attackRange;
    public GameObject attackPoint;
    public LayerMask playerLayer;
    
    private Rigidbody2D rb;
    private Animator anim;

    void Start () {
       rb = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       attackRange = GetComponent<EnemyCombat>().attackRange;
       attackPoint = GetComponent<EnemyCombat>().attackPoint;
       playerLayer = GetComponent<EnemyCombat>().playerLayer;
       
       
    }

    void Update()
    {
        if (Physics2D.OverlapCircle(attackPoint.transform.position, attackRange, playerLayer) != null || anim.GetBool("isAttack"))
        {
           rb.velocity = Vector3.zero;
           return;
        }

        var direction = (Vector2) target.position - rb.position; 
    
        direction.Normalize();
    
        
        float rd = new System.Random().Next(1, 6);
        direction.y += rd/10;
    
        rb.velocity = direction * speed;
        

        transform.rotation = Quaternion.Euler(0, rb.velocity.x > 0 ? 0 : 180, 0);

       // Поворачиваем в сторону цели
    }
}
