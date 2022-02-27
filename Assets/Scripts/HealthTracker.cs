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
    public bool invincible;
    [SerializeField]
    private bool oneMore;
    public bool tookDamage;
    [SerializeField]
    float timer = 100;
    float timerDrain = 25;

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
        if (tookDamage == true && invincible != true)
        {
            invincible = true;
            health = health - 1;
            StartCoroutine(invulTimer());
        }
    }

    IEnumerator invulTimer()
    {
        yield return new WaitForSeconds(3f);
        invincible = false;
        tookDamage = false;
    }
    
    IEnumerator FadeBack()
    {
        yield return new WaitForSeconds(2f);
        hit = false;
        yield return new WaitForSeconds(2f);
    }
}
