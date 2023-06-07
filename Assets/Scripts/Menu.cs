using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource selectSound;

    public GameObject menu;
    public GameObject Record;
    public GameObject selectLvl;
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
        selectLvl.SetActive(true);
        menu.SetActive(false);
    }

    public void Recrods()
    {
        selectSound.Play();
        Record.SetActive(true);
        menu.SetActive(false);
    }
    
}
