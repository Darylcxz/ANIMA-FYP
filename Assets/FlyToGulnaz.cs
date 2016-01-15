using UnityEngine;
using System.Collections;

public class FlyToGulnaz : MonoBehaviour {

	Transform player;
	[SerializeField]
	float effectRange;
	float _distBetweenPlayer;
	[SerializeField]
	float flySpeed = 7f;

	bool move;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	
	}
	
	// Update is called once per frame
	void Update () {
		_distBetweenPlayer = Vector3.Distance(player.position, gameObject.transform.position);
		if (_distBetweenPlayer < effectRange)
		{
			move = true;
		}
	}
	void FixedUpdate()
	{
		if (move)
		{
			Move();
		}
	}
	void Move()
	{
		Vector3 direction = player.transform.position - transform.position;
		direction.y = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 5 * Time.deltaTime);
		if (direction.magnitude > 1)
		{
			Vector3 moveVector = direction.normalized * 7 * Time.deltaTime;
			transform.position += moveVector;
		}
	}
}
