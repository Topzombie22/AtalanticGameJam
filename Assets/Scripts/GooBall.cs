using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBall : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        //  playerPos = new Vector3(player.transform.position.x, player.transform.position.y - 1f, player.transform.position.z);
        playerPos = (new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
    }

    private void Awake()
    {
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(playerPos * 25f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            Destroy(this.gameObject);
        }
    }
}
