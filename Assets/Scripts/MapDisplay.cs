using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private AudioSource selectSound;
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapDescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button play;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Map startMap;
    
    public GameObject menu;
    public GameObject selectLvl;

    public void Start()
    {
        mapName.text = startMap.mapName;
        mapDescription.text = startMap.mapDescription;
        mapImage.sprite = startMap.mapImage;
    }

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapDescription.text = map.mapDescription;
        mapImage.sprite = map.mapImage;
        startMap = map;
        switch (map.mapIndex)
        {
            case 0:
                lockImage.SetActive(false);
                play.interactable = true;
                break;
            case 1:
                if (!PlayerPrefs.HasKey("kills") || PlayerPrefs.GetInt("kills") < 100)
                {
                    lockImage.SetActive(true);
                    play.interactable = false;
                    mapDescription.text = "Убейте 100 врагов для открытия\n(Осталось убить " +
                                          (100 -
                                           PlayerPrefs.GetInt("kills")) + " врагов)";
                }
                else
                {
                    lockImage.SetActive(false);
                    play.interactable = true;
                }
                break;
            case 2:
                if (!PlayerPrefs.HasKey("kills") || PlayerPrefs.GetInt("kills") < 500)
                {
                    lockImage.SetActive(true);
                    play.interactable = false;
                    mapDescription.text = "Убейте 500 врагов для открытия\n(Осталось убить " + (500 -
                        PlayerPrefs.GetInt("kills")) + " врагов)";
                }
                else
                {
                    lockImage.SetActive(false);
                    play.interactable = true;
                }
                break;
        }
        
        //var mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= map.mapIndex;
        
        //lockImage.SetActive(!mapUnlocked);

        //play.interactable = mapUnlocked;
        
    }

    public void playGame()
    {
        selectSound.Play();
        SceneManager.LoadScene(startMap.sceneToLoad);
    }

    public void Back()
    {
        selectSound.Play();
        selectLvl.SetActive(false);
        menu.SetActive(true);
    }
}
