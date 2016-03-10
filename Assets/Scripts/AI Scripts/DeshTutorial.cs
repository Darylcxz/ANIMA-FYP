using UnityEngine;
using System.Collections;

public class DeshTutorial : MonoBehaviour {
	public bool deshDead;
	[SerializeField]
	AudioSource dieSound;
    [SerializeField]
    GameObject jumpSequence;
    [SerializeField] Canvas exclamationCanvas;
    [SerializeField]
    Transform Player;
    [SerializeField] float rangeOffset;
    public bool hasRolled;
    Vector3 lastPos;

    public bool _move;
    Animator _anim;

    float gameTime;
    enum DeshStates
    {
        SHAKE, //shake while looking at the player
        DESH       //charge at the player recklessly
    };
    DeshStates States;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        _anim = GetComponent<Animator>();
        deshDead = false;
		dieSound = GetComponent<AudioSource>();
        exclamationCanvas.enabled = false;
      //  InvokeRepeating("GetPlayerPos", 0, 2);
	}
	
	// Update is called once per frame
	void Update () {
        if(_move && !hasRolled) //if the player has yet to unfreeze time by rolling
        {
            exclamationCanvas.enabled = true;
            _anim.Play("DeshTallWALK");
            DashToPlayer();
        }
        if (hasRolled) //if the player has resumed time, do normal AI stuff
        {
            Time.timeScale = 1;
            StateMachine();
        }
	
	}
	void OnCollisionEnter(Collision _col)
	{
		if (_col.collider.tag == "dagger")
		{
            _anim.Play("DeshTallHIT");
            dieSound.Play();
			deshDead = true;
            exclamationCanvas.enabled = false;
			gameObject.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            jumpSequence.GetComponent<Collider>().isTrigger = true;
			gameObject.GetComponent<BoxCollider>().enabled = false;
            Invoke("Hide", 2f);
           
		}
	}
    void StateMachine()
    {
        gameTime += Time.deltaTime;
        switch (States)
        {
            case DeshStates.SHAKE:
                _anim.Play("DeshTallSHAKE");
                lastPos = Player.transform.position;
                ChangeState(3f, DeshStates.DESH);
                break; 
            case DeshStates.DESH:
                _anim.Play("DeshTallWALK");
                DashToPlayer(true);
                ChangeState(2f, DeshStates.SHAKE);
                break;
        }
    }

    void ChangeState(float timeBeforeStateChange, DeshStates nextState)
    {
        if (gameTime > timeBeforeStateChange)
        {
            States = nextState;
            gameTime = 0;
        }
    }
    void DashToPlayer(bool isStateMachine = false)
    {
        Vector3 _dir = (Player.position - transform.position).normalized;
        _dir.y = 0;
        Quaternion endRot = Quaternion.LookRotation(_dir);
        if(isStateMachine)
        {
            _dir = (lastPos+transform.forward - transform.position);
            _dir.y = 0;
            endRot = Quaternion.LookRotation(_dir);
          
            transform.rotation = endRot;
            transform.position += transform.forward * Time.deltaTime * 15;
            if (Vector3.Distance(transform.position,lastPos + transform.forward) < 0.5f || Vector3.Distance(transform.position,Player.position) < 0.5f)
            {
                States = DeshStates.SHAKE;
            }
        }
        if (!isStateMachine && Vector3.Distance(transform.position, Player.position) > rangeOffset)
        {
            //if the player is not so close to the AI, move the AI to the player
            transform.rotation = endRot;
            transform.position += transform.forward * Time.deltaTime * 2;
        }
        else if (!hasRolled)
        {
            Time.timeScale = 0;
        }
      
    }
    void Hide()
    {
        gameObject.SetActive(false);
        CancelInvoke("Hide");
    }
   //void GetPlayerPos()
   // {
   //     lastPos = Player.transform.position;
   // }
}
