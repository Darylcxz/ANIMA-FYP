using UnityEngine;
using System.Collections;

public class SpitFlowerAI : MonoBehaviour {
	GameObject player;
	GameObject target;
	[SerializeField]
	Rigidbody projectile;
	[SerializeField]
	Transform shootPoint;
	public float range = 5f;
	Rigidbody _rb;
	float timer;
	float distance;
	bool shoot;
	Vector3 targetSpeed;
	Vector3 futurePos;
	Vector3 direction;
	float iterations = 30f;
	public enum NeptoAI
	{
 		IDLE,
		AIM,
		SHOOT,
        DEAD
	}
	public NeptoAI NeptoState = NeptoAI.IDLE;
	Animator NeptoController;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		_rb = GetComponent<Rigidbody>();
		NeptoController = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//	Debug.Log(timer);
		distance = Vector3.Distance(player.transform.position, transform.position);
		AILogic();
		
	}
	void LookAtPlayer(bool lockY = false)
	{
		target = player.gameObject;
		targetSpeed = target.GetComponent<Rigidbody>().velocity;
		futurePos = target.transform.position + (targetSpeed * (distance/iterations));
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
                if (DistanceBetween(player.transform.position, transform.position) < range)
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
                if (!shoot)
                {
                    NeptoController.SetTrigger("tAttack");
                    Fire();
                }
                break;
            case NeptoAI.DEAD:
                gameObject.GetComponent<Collider>().isTrigger = true;
                break;

        }
	}
    void ChangeState(float timeBeforeStateChange, NeptoAI nextState)
    {
        if (timer > timeBeforeStateChange)
        {
            NeptoState = nextState;
            timer = 0;
        }
    }
    void Fire()
	{
		Rigidbody projectileClone = Instantiate(projectile, shootPoint.position, transform.rotation) as Rigidbody;
		projectileClone.SendMessage("OriginPos", transform.position);
		projectileClone.velocity = transform.forward*20;
		shoot = true;ChangeState(0f, NeptoAI.IDLE);
	}
    float DistanceBetween(Vector3 A, Vector3 B)
    {
        return Vector3.Distance(A, B);
    }
    void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger")
		{
            //play Death2 anim
            //NeptoController.SetBool("isHit", true);
            NeptoController.Play("Death1");
			_rb.isKinematic = false;
			NeptoState = NeptoAI.DEAD;
		}
		if (col.collider.tag == "Ball")
		{
 			//play Death1 anim
			Debug.Log("DEAD");
            NeptoController.Play("Death2");
            _rb.isKinematic = false;
			NeptoState = NeptoAI.DEAD;
			Destroy(col.collider.gameObject, 0.2f);
		}
	}
}
