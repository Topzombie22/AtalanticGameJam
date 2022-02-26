using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public float timer;
    public bool startTimer;

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
        if (startTimer == true)
        {
            timer += 1 * Time.deltaTime;

            float timers = Mathf.Round(timer);
            timerText.text = timers.ToString();
        }
    }
}
