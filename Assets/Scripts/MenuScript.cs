using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool AudioOn;
    public GameObject cam;
    public GameObject audioOn;
    public GameObject audioOff;
    public GameObject credits;
    // Start is called before the first frame update
    void Start()
    {
        AudioOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioOn == true)
        {
            cam.GetComponent<AudioListener>().enabled = true;
            audioOff.SetActive(false);
            audioOn.SetActive(true);
        }
        if (AudioOn == false)
        {
            cam.GetComponent<AudioListener>().enabled = false;
            audioOff.SetActive(true);
            audioOn.SetActive(false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Audio()
    {
        if (AudioOn == true)
        {
            AudioOn = false;
        }
        else if (AudioOn == false)
        {
            AudioOn = true;
        }
    }

    public void Credits()
    {
        credits.SetActive(true);
    }

    public void Back()
    {
        credits.SetActive(false);
    }
}
