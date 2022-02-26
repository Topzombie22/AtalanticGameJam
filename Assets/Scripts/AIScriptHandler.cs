using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScriptHandler : MonoBehaviour
{   
    public enum MonsterAI
    {
        None,
        Melee,
        Spitter,
        Driller,
        Bomber,
        TerrorFormer,
    }
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Rigidbody rb;
    private GameObject gate;
    public int aiChooser;
    [SerializeField]
    private MonsterAI currentAI;

    private float height;

    private GameObject groundAnim;

    private bool monsterSetup = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundAnim = GameObject.Find("GroundAnim");
        gate = GameObject.Find("Gate");
        AiSelector();
    }

    // Update is called once per frame
    void Update()
    {
        AIBrain();
    }

    private void AiSelector()
    {

        switch (aiChooser)
        {
            case 1:
                currentAI = MonsterAI.Melee;
                break;
            case 2:
                currentAI = MonsterAI.Spitter;
                break;
            case 3:
                currentAI = MonsterAI.Driller;
                break;
            case 4:
                currentAI = MonsterAI.Bomber;
                break;
        }
    }

    private void AIBrain()
    {
        if (currentAI == MonsterAI.Melee)
        {
            Melee();
        }
        else if (currentAI == MonsterAI.Spitter)
        {
            Spitter();
        }
        else if (currentAI == MonsterAI.Driller)
        {
            Driller();
        }
        else if (currentAI == MonsterAI.Bomber)
        {
            Bomber();
        }
    }

    private void Melee()
    {
        if (monsterSetup == false)
        {
            float y = Random.Range(0f, 361f);
            Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            monsterSetup = true;
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void Spitter()
    {
        if (monsterSetup == false)
        {
            float y = Random.Range(0, 361);
            Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            monsterSetup = true;
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void Driller()
    {
        if (monsterSetup == false)
        {
            float y = Random.Range(0, 361);
            Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            monsterSetup = true;
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void Bomber()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = (new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, -10, transform.position.z)).normalized;
        rb.AddForce(dir * Vector3.Distance(player.transform.position, transform.position) / 2f, ForceMode.Impulse);
        currentAI = MonsterAI.None;
    }
}
