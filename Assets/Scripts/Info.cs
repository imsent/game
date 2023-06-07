using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource selectSound;
    public GameObject info;
    public GameObject player;
    public GameObject UI;
    public GameObject spawn;
    void Start()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
    }

    // Update is called once per frame
    public void close()
    {
        selectSound.Play();
        Time.timeScale = 1f;
        info.SetActive(false);
        player.SetActive(true);
        UI.SetActive(true);
        spawn.SetActive(true);
    }
}
