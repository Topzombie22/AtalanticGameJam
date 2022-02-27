using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timer;
    public float timerdown = 6;

    private bool startTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeTracker();
    }

    void TimeTracker()
    {
        if (startTimer == false)
        {
            timerdown -= 1 * Time.deltaTime;
            if (timerdown <= 0)
            {
                startTimer = true;
            }
        }
        if (startTimer == true)
        {
            timer += 1 * Time.deltaTime;

            float timers = Mathf.Round(timer);
            timerText.text = timers.ToString();
        }
    }
}
