using UnityEngine;
using System.Collections;

public class BossNepto : MonoBehaviour {

    GameObject player;
    [SerializeField]
    Rigidbody projectile;
    [SerializeField]
    Transform shootPoint;
    public float range = 5f;
    Rigidbody _rb;
    float timer;
    float distance;
    public bool shoot;
    Vector3 targetSpeed;
    Vector3 futurePos;
    Vector3 direction;
    float iterations = 30f;
    [SerializeField] enum NeptoAI
    {
       IDLE,
       AIM,
       SHOOT
    }
    NeptoAI NeptoState = NeptoAI.IDLE;
    Animator NeptoController;


    [SerializeField] GameObject BossBeetle;
    public bool beetleControl;
    public int shootCount;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody>();
        NeptoController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //	Debug.Log(timer);
        distance = Vector3.Distance(player.transform.position, transform.position);
        if(!beetleControl)
        {
            AILogic();
        }
        
    }
    void LookAtPlayer(bool lockY = false)
    {
        
        targetSpeed = player.GetComponent<Rigidbody>().velocity;
        futurePos = player.transform.position + (targetSpeed * (distance / iterations));
        direction = futurePos - transform.position;
        if(lockY)
        {
            direction.y = 0;
        }
        Quaternion lookRot = Quaternion.LookRotation(direction);
        transform.rotation = lookRot;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 2f * Time.deltaTime);
    }
    void AILogic()
    {
        timer += Time.deltaTime;
        switch (NeptoState)
        {
            case NeptoAI.IDLE:
                shoot = false;
               if(DistanceBetween(player.transform.position,transform.position) < range)
                {
                    NeptoController.SetBool("isAttacking", true);
                    ChangeState(0f, NeptoAI.AIM);
                }
                break;
            case NeptoAI.AIM:
                LookAtPlayer(true);
                ChangeState(3f, NeptoAI.SHOOT);
                break;
            case NeptoAI.SHOOT:
                LookAtPlayer();
                if(!shoot)
                {
                    NeptoController.SetTrigger("tAttack");
                    Fire();
                }
                break;

        }
    }
    void ChangeState(float timeBeforeStateChange, NeptoAI nextState)
    {
        if(timer > timeBeforeStateChange)
        {
            NeptoState = nextState;
            timer = 0;
        }
    }
    void Fire()
    {
        Rigidbody projectileClone = Instantiate(projectile, shootPoint.position, transform.rotation) as Rigidbody;
        projectileClone.SendMessage("OriginPos", transform.position,SendMessageOptions.DontRequireReceiver);
        projectileClone.velocity = transform.forward * 20;
        shootCount++;
        shoot = true;
        NeptoState = NeptoAI.IDLE;
    }
    float DistanceBetween(Vector3 A,Vector3 B)
    {
        return Vector3.Distance(A, B);
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("dagger"))
        {
            BossBeetle.SendMessage("TrapCardActivated", SendMessageOptions.DontRequireReceiver);
        }
        if(col.collider.CompareTag("Ball"))
        {
            BossBeetle.SendMessage("GotDeflected", SendMessageOptions.DontRequireReceiver);
        }
    }
}
