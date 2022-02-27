using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBombScript : MonoBehaviour
{
    public GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.GetComponent<HealthTracker>().tookDamage = true;
            gameManager.GetComponent<HealthTracker>().hit = true;
        }
    }
}
