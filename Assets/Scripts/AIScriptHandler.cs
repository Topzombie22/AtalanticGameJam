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

    private NavMeshAgent agent;

    private bool attacking = false;

    private bool justSpawned = true;

    private GameObject groundAnim;
    [SerializeField]
    private GameObject gooBall;
    [SerializeField]
    private GameObject gooBallTracker;

    private Vector3 playerPos;

    private bool underGround;
    private bool monsterTimeout;
    private bool timerStarted;

    private bool monsterSetup = false;
    private bool hasShot;
    private bool playerPosFound;


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
        if (attacking == false)
        {
            attacking = true;
            StartCoroutine(Shoot());
        }
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
        if (attacking == false)
        {
            attacking = true;
            StartCoroutine(Drill());
        }
        if (underGround == true)
        {
            Vector3 playPos = (new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) - new Vector3(transform.position.x, transform.position.y, transform.position.z)).normalized;
            Vector3 playPosFinal = new Vector3(playPos.x, 0f, playPos.z);
            transform.Translate(playPosFinal * 10f * Time.deltaTime);
            float dist = Vector3.Distance(player.transform.position, gameObject.transform.position);
            if(dist <= 2f)
            {
                timerStarted = true;
                StartCoroutine(DrillAttack());
            }
            if (timerStarted == false)
            {
                StartCoroutine(DrillerTimer());
                timerStarted = true;
            }
        }
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

    IEnumerator Shoot()
    {
        if (justSpawned == true)
        {
            yield return new WaitForSeconds(3f);
            justSpawned = false;
        }
        _anim.SetTrigger("AttackThrow");
        yield return new WaitForSeconds(0.75f);
        Transform goopDispenser = gameObject.transform.GetChild(3);
        Instantiate(gooBall, goopDispenser.position, Quaternion.identity);
        StartCoroutine(ShotCooldown());
    }

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(4f);
        attacking = false;
    }

    IEnumerator DrillAttack()
    {
        _anim.SetTrigger("AttackBurrow");
        underGround = false;
        attacking = false;
        timerStarted = false;
        yield return new WaitForSeconds(3f);
    }

    IEnumerator DrillerTimer()
    {
        yield return new WaitForSeconds(5f);
        if (attacking == false)
        {
            yield break;
        }
        _anim.SetTrigger("AttackBurrow");
        yield return new WaitForSeconds(3f);
        if (attacking == false)
        {
            yield break;
        }
        underGround = false;
        attacking = false;
        timerStarted = true;
    }

    IEnumerator Drill()
    {
        _anim.SetTrigger("StartBurrow");
        yield return new WaitForSeconds(5f);
        underGround = true;
    }
}
