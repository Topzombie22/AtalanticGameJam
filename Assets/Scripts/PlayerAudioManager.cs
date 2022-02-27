using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioClip footSteps;
    public AudioClip dash;
    public AudioClip jump;

    public AudioSource _dash;
    public AudioSource _footSteps;
    public AudioSource _jump;
    // Start is called before the first frame update
    void Start()
    {
        _footSteps.clip = footSteps;
        _dash.clip = dash;
        _jump.clip = jump;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDash()
    {
        _dash.Play();
    }

    public void PlayJump()
    {
        _jump.Play();
    }

}
