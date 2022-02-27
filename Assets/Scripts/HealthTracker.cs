using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    public int health;
    public bool dead;
    public RawImage hitEffect;
    public bool hit = false;
    private bool faded = false;

    // Start is called before the first frame update
    void Start()
    {
        hitEffect.canvasRenderer.SetAlpha(0);
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            dead = true;
        }
        if (hit == true)
        {
            hitEffect.CrossFadeAlpha(1f, 0.75f, false);
            StartCoroutine(FadeBack());
        }
        if (hit == false)
        {
            hitEffect.CrossFadeAlpha(0f, 0.75f, false);
        }
    }
    
    IEnumerator FadeBack()
    {
        yield return new WaitForSeconds(2f);
        faded = hit = false;
    }
}
