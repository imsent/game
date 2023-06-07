using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Device.Application;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    private int min;
    
    
    private Text time;
    
    
    
    public GameObject player;
    private PlayerCombat _playerCombat;
    
    void Start()
    {
        _playerCombat = player.GetComponent<PlayerCombat>();
        time = GetComponent<Text>();
        time.text = TimeSpan.FromSeconds(timer).ToString("mm':'ss");
    }

    void Update()
    {
        if (player.GetComponent<Animator>().GetBool("death")) return;
        timer += Time.deltaTime;
        if ((int) TimeSpan.FromSeconds(timer).TotalMinutes > min)
        {
            min++;
            _playerCombat.score += 1000;
        }  
        time.text = TimeSpan.FromSeconds(timer).ToString("mm':'ss");
    }
}
