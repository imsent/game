using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinRespawn : MonoBehaviour
{
    
    // Start is called before the first frame update

    private CircleCollider2D coin;
    
    

    void Start()
    {
        coin = GetComponent<CircleCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().coinSound.Play();
            other.GetComponent<PlayerMove>().coins += 1;
            other.GetComponent<PlayerCombat>().score += 200;
            coin.gameObject.SetActive(false);
            Invoke("Respawn",30f);
        }
    }
    void Respawn()
    {
        coin.gameObject.SetActive(true);
    }
}
