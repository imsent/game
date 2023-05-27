using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource selectSound;
    private void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    public void quit()
    {
        selectSound.Play();
        Application.Quit();
    }
    
    public void playGame()
    {
        selectSound.Play();
        Invoke("play",0.25f);
    }

    private void play()
    {
        SceneManager.LoadScene("level1");
    }
}
