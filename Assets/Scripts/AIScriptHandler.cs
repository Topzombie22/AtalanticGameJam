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

    private Animator _anim;

    private float deathTimer;

    private bool attacking = false;

    private GameObject groundAnim;

    private bool monsterSetup = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        groundAnim = GameObject.Find("GroundAnim");
        gate = GameObject.Find("Gate");
        _anim = GetComponent<Animator>();
        if (aiChooser == 1)
        {
            deathTimer = Random.Range(20f, 30f);
            Destroy(this.gameObject, deathTimer);
        }
        if (aiChooser == 2)
        {
            deathTimer = Random.Range(15f, 30f);
            Destroy(this.gameObject, deathTimer);
        }
        if (aiChooser == 3)
        {
            deathTimer = Random.Range(30f, 40f);
            Destroy(this.gameObject, deathTimer);
        }
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
            var groundAni = Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            Destroy(groundAni, 15f);
            monsterSetup = true;
        }
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
        var playerDist = Vector3.Distance(player.transform.position, transform.position);
        if (attacking == false && playerDist < 10f)
        {
            attacking = true;
            StartCoroutine(Swing());
        }
    }

    private void Spitter()
    {
        if (monsterSetup == false)
        {
            float y = Random.Range(0, 361);
            var groundAni = Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            Destroy(groundAni, 15f);
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
            var groundAni = Instantiate(groundAnim, transform.position, new Quaternion(45f, 0f, 0f, 0f));
            Destroy(groundAni, 15f);
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

    IEnumerator Swing()
    {
        var swingType = Random.Range(1, 3);
        if (swingType == 1)
        {
            _anim.SetTrigger("AttackSlam");
        }
        else if (swingType == 2)
        {
            _anim.SetTrigger("AttackSwing");
        }
        yield return new WaitForSeconds(4f);
        attacking = false;
    }
}
