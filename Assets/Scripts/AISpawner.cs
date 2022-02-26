using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject player;
    public int mobType;
    private int dist;
    [SerializeField]
    private float timeChecker;

    private float distVariable;

    private int gameStage;
    private int mobQueue;

    [SerializeField]
    private float timer = 100f;
    private float timerDrain = 25f;

    // Start is called before the first frame update
    void Start()
    {
        gameStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timeChecker = gameObject.GetComponent<Timer>().timer;
    }

    private void FixedUpdate()
    {
        SpawnMobs();
    }

    void SpawnMobs()
    {
        if (timeChecker >= 90)
        {
            gameStage = 1;
        }
        
        if (timeChecker >= 180)
        {
            gameStage = 2;
        }



        if (timer >= 0.01)
        {
            timer = timer - timerDrain * Time.deltaTime;
        }
        else if (timer < 0.01)
        {
            mobQueue = Random.Range(0, 200);
            MobsToSpawn();

        }
    }

    void MobsToSpawn()
    {
        if (gameStage == 0)
        {
            if (mobQueue >= 0 && mobQueue <= 89)
            {
                mobType = 1;
                distVariable = Random.Range(10, 30);
            }
            else if (mobQueue >= 90 && mobQueue <= 159)
            {
                mobType = 2;
                distVariable = Random.Range(25, 80);
            }
            else if (mobQueue >= 160 && mobQueue <= 200)
            {
                mobType = 3;
                distVariable = Random.Range(10, 80);
            }
        }
        else if (gameStage == 1)
        {
            if (mobQueue >= 0 && mobQueue <= 79)
            {
                mobType = 1;
                distVariable = Random.Range(10, 30);
            }
            else if (mobQueue >= 80 && mobQueue <= 139)
            {
                mobType = 2;
                distVariable = Random.Range(25, 80);
            }
            else if (mobQueue >= 145 && mobQueue <= 179)
            {
                mobType = 3;
                distVariable = Random.Range(10, 80);
            }
            else if (mobQueue >= 180 && mobQueue <= 200)
            {
                mobType = 4;
            }
        }
        else if (gameStage == 2)
        {
            if (mobQueue >= 0 && mobQueue <= 79)
            {
                mobType = 1;
                distVariable = Random.Range(10, 30);
            }
            else if (mobQueue >= 80 && mobQueue <= 129)
            {
                mobType = 2;
                distVariable = Random.Range(25, 80);
            }
            else if (mobQueue >= 130 && mobQueue <= 169)
            {
                mobType = 3;
                distVariable = Random.Range(10, 80);
            }
            else if (mobQueue >= 170 && mobQueue <= 189)
            {
                mobType = 4;
            }
            else if (mobQueue >= 190 && mobQueue <= 200)
            {
                mobType = 5;
            }
        }
        MobSpawnHandler();
    }

    void MobSpawnHandler()
    {

        if (mobType != 4 || mobType != 5)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 20f;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 20f, 1);
            var mob = Instantiate(monsters[0], hit.position, Quaternion.identity);
            mob.GetComponent<AIScriptHandler>().aiChooser = mobType;
        }
        timer = 100;
    }

}
