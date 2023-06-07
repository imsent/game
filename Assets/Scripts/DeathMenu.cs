using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private AudioSource selectSound;

    public Text statistics;
    // Start is called before the first frame update
    private void Start()
    {
        Cursor.visible = true;
        statistics.text = Data._Statistics;
    }

    public void restart()
    {
        selectSound.Play();
        Invoke("res",0.25f);
    }

    private void res()
    {
        SceneManager.LoadScene(Data._PrevScene);
    }
    public void quit()
    {
        selectSound.Play();
        Application.Quit();
    }
}
