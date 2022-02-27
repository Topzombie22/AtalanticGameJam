using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthTracker : MonoBehaviour
{
    public GameObject music;
    public GameObject musicDestroy;
    public GameObject player;
    public GameObject cam;
    public GameObject enemies;
    public RawImage cthulu;
    public RawImage gameOver;
    public RawImage playAgain;
    public RawImage Quit;
    public RawImage HP1;
    public RawImage HP2;
    public RawImage HP3;
    public int health;
    public bool dead;
    public RawImage hitEffect;
    public RawImage deathEffect;
    public Text GameOver;
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
        Quit.canvasRenderer.SetAlpha(0);
        playAgain.canvasRenderer.SetAlpha(0);
        gameOver.canvasRenderer.SetAlpha(0);
        cthulu.canvasRenderer.SetAlpha(0);
        deathEffect.canvasRenderer.SetAlpha(0);
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
        if (dead == true)
        {
            StartCoroutine(Death());
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
            player.GetComponent<PlayerAudioManager>().PlayDamage();
            StartCoroutine(invulTimer());
        }
        HPHandle();
    }

    void HPHandle()
    {
        if (health == 3)
        {
            HP1.gameObject.SetActive(true);
            HP2.gameObject.SetActive(false);
            HP3.gameObject.SetActive(false);
        }
        if (health == 2)
        {
            HP1.gameObject.SetActive(false);
            HP2.gameObject.SetActive(true);
            HP3.gameObject.SetActive(false);
        }
        if (health == 1)
        {
            HP1.gameObject.SetActive(false);
            HP2.gameObject.SetActive(false);
            HP3.gameObject.SetActive(true);
        }
        if (health == 0)
        {
            HP1.gameObject.SetActive(false);
            HP2.gameObject.SetActive(false);
            HP3.gameObject.SetActive(false);
        }
    }

    IEnumerator Death()
    {
        GetComponent<Timer>().stopTimer = true;
        Destroy(gameObject.GetComponent<AISpawner>());
        music.SetActive(true);
        Destroy(musicDestroy);
        Destroy(enemies);
        player.GetComponent<PlayerAudioManager>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        deathEffect.CrossFadeAlpha(1f, 0.1f, false);
        yield return new WaitForSeconds(0.5f);
        deathEffect.CrossFadeColor(Color.black, 0.5f, false, true);
        yield return new WaitForSeconds(0.5f);
        cthulu.CrossFadeAlpha(1f, 1f, false);
        yield return new WaitForSeconds(1f);
        gameOver.CrossFadeAlpha(1, 1f, false);
        yield return new WaitForSeconds(0.5f);
        Quit.CrossFadeAlpha(1f, 1f, false);
        playAgain.CrossFadeAlpha(1f, 1f, false);
        GameOver.text = ("You survived for... " + Mathf.Round(GetComponent<Timer>().timer) + " Seconds");
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
