using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject Record;
    [SerializeField] private AudioSource selectSound;


    public Text level1;
    public Text level2;
    public Text level3;


    private void OnEnable()
    {
        level1.text = $@"Лучшее время: {TimeSpan.FromSeconds(PlayerPrefs.GetInt("time1")).ToString("mm':'ss")}
Максимум очков: {PlayerPrefs.GetInt("score1")}
Максимум убийств: {PlayerPrefs.GetInt("kills1")}
Максимум монет: {PlayerPrefs.GetInt("money1")}";
        level2.text = $@"Лучшее время: {TimeSpan.FromSeconds(PlayerPrefs.GetInt("time2")).ToString("mm':'ss")}
Максимум очков: {PlayerPrefs.GetInt("score2")}
Максимум убийств: {PlayerPrefs.GetInt("kills2")}
Максимум монет: {PlayerPrefs.GetInt("money2")}";
        level3.text = $@"Лучшее время: {TimeSpan.FromSeconds(PlayerPrefs.GetInt("time3")).ToString("mm':'ss")}
Максимум очков: {PlayerPrefs.GetInt("score3")}
Максимум убийств: {PlayerPrefs.GetInt("kills3")}
Максимум монет: {PlayerPrefs.GetInt("money3")}";
    }

    // Update is called once per frame
    public void back()
    {
        selectSound.Play();
        menu.SetActive(true);
        Record.SetActive(false);
    }

    public void deleteAll()
    {
        selectSound.Play();
        for (var i = 1; i < 4; i++)
        {
            PlayerPrefs.DeleteKey("time" + i);
            PlayerPrefs.DeleteKey("score" + i);
            PlayerPrefs.DeleteKey("kills" + i);
            PlayerPrefs.DeleteKey("money" + i);
        }
        PlayerPrefs.DeleteKey("kills");
        OnEnable();
    }
}
