using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject player;
    public int mobType;

    [SerializeField]
    private float timer = 100f;
    private float timerDrain = 25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 

    }

    private void FixedUpdate()
    {
        SpawnMobs();
    }

    void SpawnMobs()
    {
        if (timer >= 0.01)
        {
            timer = timer - timerDrain * Time.deltaTime;
        }
        else if (timer < 0.01)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 20f;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 20f, 1);
            Instantiate(monsters[0], hit.position, Quaternion.identity);
            timer = 100;
        }
    }
}
