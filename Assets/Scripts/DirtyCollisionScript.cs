using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCollisionScript : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.GetComponent<HealthTracker>().tookDamage = true;
            gameManager.GetComponent<HealthTracker>().hit = true;
        }
    }
}
