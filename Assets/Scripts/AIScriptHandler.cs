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
    }
    [SerializeField]
    private GameObject player;
    public int aiChooser;
    [SerializeField]
    private MonsterAI currentAI;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
    }

    private void Melee()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void Spitter()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }

    private void Driller()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
    }
}
