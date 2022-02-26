using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AISpawner : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject player;
    public GameObject gate;
    public int mobType;
    private int dist;
    [SerializeField]
    private float timeChecker;

    private bool Spawned;

    private int attempts;

    private float distVariableX;
    private float distVariableZ;

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
        gameObject.transform.position = player.transform.position;
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
                distVariableX = Random.Range(-20, 20);
                distVariableZ = Random.Range(-20, 20);
            }
            else if (mobQueue >= 90 && mobQueue <= 159)
            {
                mobType = 2;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
            }
            else if (mobQueue >= 160 && mobQueue <= 200)
            {
                mobType = 3;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
            }
        }
        else if (gameStage == 1)
        {
            if (mobQueue >= 0 && mobQueue <= 79)
            {
                mobType = 1;
                distVariableX = Random.Range(-20, 20);
                distVariableZ = Random.Range(-20, 20);
            }
            else if (mobQueue >= 80 && mobQueue <= 139)
            {
                mobType = 2;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
            }
            else if (mobQueue >= 145 && mobQueue <= 179)
            {
                mobType = 3;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
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
                distVariableX = Random.Range(-20, 20);
                distVariableZ = Random.Range(-20, 20);
            }
            else if (mobQueue >= 80 && mobQueue <= 129)
            {
                mobType = 2;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
            }
            else if (mobQueue >= 130 && mobQueue <= 169)
            {
                mobType = 3;
                distVariableX = Random.Range(-60, 60);
                distVariableZ = Random.Range(-60, 60);
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

        if (mobType != 4 && mobType != 5)
        {
            Vector3 randomDirection = new Vector3(player.transform.position.x + distVariableX, 8.6f, player.transform.position.z + distVariableZ);
            RaycastHit hit;
            if (Physics.Raycast(randomDirection, Vector3.down, out hit, 5f))
            {
                if (hit.collider.tag == "Ground" && attempts < 100)
                {
                    if (mobType == 1)
                    {
                        var mob = Instantiate(monsters[0], randomDirection, Quaternion.identity);
                        mob.GetComponent<AIScriptHandler>().aiChooser = mobType;
                    }
                    if (mobType == 2)
                    {
                        var mob = Instantiate(monsters[2], randomDirection, Quaternion.identity);
                        mob.GetComponent<AIScriptHandler>().aiChooser = mobType;
                    }
                    if (mobType == 3)
                    {
                        var mob = Instantiate(monsters[3], randomDirection, Quaternion.identity);
                        mob.GetComponent<AIScriptHandler>().aiChooser = mobType;
                    }
                    Spawned = true;
                    attempts++;
                }
                else if (hit.collider == null)
                {
                     
                }
                else if (attempts >= 100)
                {
                    attempts = 0;
                    Spawned = true;
                }
            }
        }
        else if (mobType == 4)
        {
            var mob2 = Instantiate(monsters[1], gate.transform.position, Quaternion.identity);
            mob2.GetComponent<AIScriptHandler>().aiChooser = mobType;
            Spawned = false;
            timer = 100;
            return;
        }
        if (Spawned != true)
        {
            MobsToSpawn();
        }
        else if (Spawned == true)
        {
            Spawned = false;
            timer = 100;
        }
    }
}
