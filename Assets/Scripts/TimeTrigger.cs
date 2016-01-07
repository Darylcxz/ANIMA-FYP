using UnityEngine;
using System.Collections;

public class TimeTrigger : MonoBehaviour {
	public float _t;
	bool slowT;

	// Use this for initialization
	void Start () {
		_t = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			slowT = false;
			_t = 0;
			Time.timeScale = 1;
		}
		if (slowT)
		{
			_t += Time.deltaTime / 3f;
			Time.timeScale = Mathf.Lerp(1f, 0f, _t);
		}
		if (_t > 1)
		{
			_t = 1;
		}
		
		
		
	
	}
	void OnTriggerEnter(Collider _col)
	{
		if (_col.tag == "Player")
		{
			slowT = true ;
		}
	}
}
