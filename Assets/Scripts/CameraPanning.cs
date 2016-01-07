using UnityEngine;
using System.Collections;

public class CameraPanning : MonoBehaviour {

	public Transform currentMount;
	public float speed;
	public float _t = 0;
	bool _bool;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_bool)
		{
			_t = 0;
			_t += Time.deltaTime*1.5f;
			_bool = false;
		}
		if (_t > 1)
		{
			_t = 1;
		}

		transform.position = Vector3.Lerp(transform.position, currentMount.position, _t);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, _t);
	}
	public void SetMount(Transform newMount)
	{
		currentMount = newMount;
		_bool = true;
	}

}
