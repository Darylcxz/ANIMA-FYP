using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewDeshAI : NewAIBA {

    enum DeshStates
    {
        SHAKE,
        DESH,
        HIT,
        DEATH
    }
    DeshStates States;
    Animator _deshAnim;
    float gameTime;
    Vector3 lastPos;
    [SerializeField] Canvas ExclamationUI;
    AudioSource deshSound;
    // Use this for initialization
    protected override void Start()
    {
        ExclamationUI.enabled = false;
        healthAI = 3f;
        _deshAnim = GetComponent<Animator>();
        deshSound = GetComponent<AudioSource>();
        base.Start();
    }

    protected override void ActivateAbility()
    {
        //triggers when the player is within range of the AI
        Debug.Log("Desh health = " + healthAI);
        ExclamationUI.enabled = true;
        gameTime += Time.deltaTime;
        switch(States)
        {
            case DeshStates.SHAKE:
                _deshAnim.Play("DeshTallSHAKE");
                lastPos = player.position;
                ChangeState(3f, DeshStates.DESH);
                break;
            case DeshStates.DESH:
                _deshAnim.Play("DeshTallWALK");
                DashToPlayer();
                ChangeState(2f, DeshStates.SHAKE);
                break;
            case DeshStates.HIT:
                _deshAnim.Play("DeshTallHIT");
                ChangeState(1f, DeshStates.SHAKE);
                break;
            case DeshStates.DEATH:
                //kill the desh
                if(!gameObject.GetComponent<Collider>().isTrigger)
                {
                    deshSound.Play();
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    gameObject.GetComponent<Collider>().isTrigger = true;
                }
                break;

        }
        if(healthAI <1)
        {
            States = DeshStates.DEATH;
            ExclamationUI.enabled = false;
        }
    }
    protected override void AIStateMachine()
    {
        base.AIStateMachine();
        switch(AIState)
        {
            case StateMachine.IDLE:
                _deshAnim.Play("DeshTallSHAKE");
                break;
            case StateMachine.WALK:
                _deshAnim.Play("DeshTallWALK");
                break;
        }
    }
    protected override void PassiveAbility()
    {
        //technically an update function

    }
    void ChangeState(float timeBeforeStateChange, DeshStates nextState)
    {
        if (gameTime > timeBeforeStateChange)
        {
            States = nextState;
            gameTime = 0;
        }
    }
    void DashToPlayer()
    {
        Vector3 _dir = (player.position - transform.position).normalized;
        _dir.y = 0;
        Quaternion endRot = Quaternion.LookRotation(_dir);
        _dir = (lastPos + transform.forward - transform.position);
        _dir.y = 0;
        endRot = Quaternion.LookRotation(_dir);

        transform.rotation = endRot;
        transform.position += transform.forward * Time.deltaTime * speedAI*5;
        if (Vector3.Distance(transform.position, lastPos + transform.forward) < 0.5f || Vector3.Distance(transform.position, player.position) < 0.5f)
        {
            States = DeshStates.SHAKE;
        }

    }
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag("dagger"))
        {
            States = DeshStates.HIT;
            healthAI--;
        }
    }

}
