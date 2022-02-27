using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfBoundsHandler : MonoBehaviour
{
    public Transform respawn;
    public Image fade;
    public GameObject gameManager;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        fade.canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            gameManager.GetComponent<HealthTracker>().hit = true; 
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        fade.CrossFadeAlpha(1f, 0.1f, false);
        yield return new WaitForSeconds(0.1f);
        player.transform.position = respawn.position;
        yield return new WaitForSeconds(0.1f);
        fade.CrossFadeAlpha(0f, 0.1f, false);
    }
}
