using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool paused;
    public GameObject menu;
    public GameObject player;
    public GameObject audioOn;
    public GameObject audioOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
        player.GetComponent<PlayerController>().inMenu = false;
        player.GetComponent<PlayerController>().canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void AudioOn()
    {
        audioOff.SetActive(false);
        audioOn.SetActive(true);
        player.GetComponentInChildren<AudioListener>().enabled = true;
    }

    public void AudioOff()
    {
        audioOn.SetActive(false);
        audioOff.SetActive(true);
        player.GetComponentInChildren<AudioListener>().enabled = false;
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");

    }
}
