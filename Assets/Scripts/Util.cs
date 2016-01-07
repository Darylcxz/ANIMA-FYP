using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {
	static float shakeAmt = 0.7f;
	static float shake;
	float decreaseNum = 1.0f;
	// Use this for initialization
	//public static void Shake(GameObject _obj,float _t)
	//{
	//	Vector3 _origin = _obj.transform.localPosition;
	//	_obj.transform.localPosition = _origin + Random.insideUnitSphere * shakeAmt;


	//}
	void Update()
	{
		Debug.Log(shake + "wahh");
		if (shake > 0)
		{
			shake -= Time.deltaTime * decreaseNum;
			Debug.Log("sleep");
		}
		else
		{
			shake = 0f;
		}
		
	}
	public static void Shake(GameObject _obj, float _t)
	{
		
		Vector3 _origin = _obj.transform.localPosition;
		shake = _t;
		if (shake > 0)
		{
			_obj.transform.localPosition = _origin + Random.insideUnitSphere * shakeAmt;
		}
		else
		{
			_obj.transform.localPosition = _origin;
		}
		Debug.Log(shake);
	}
	
}
