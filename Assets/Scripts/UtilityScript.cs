using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UtilityScript : MonoBehaviour {

	public static UtilityScript instance;
	Image _img;
	bool bFade;
	float _alpha;

	Vector3 _originalPos;
	
	public Transform _cam;
	float camShake;
	float shakeAmt;

	// Use this for initialization
	void Start () 
	{
		instance = this;
		shakeAmt = 2f;
	
	}
	public void FlashToFade(Image _image)
	{
		if (!bFade)
		{
			bFade = true;
			_img = _image;
			StartCoroutine("Flash");
		}
	}
	public void FadeInFadeOut(Image _image)
	{
		if (!bFade)
		{
			bFade = true;
			_img = _image;
			StartCoroutine("Fade");
		}
	}
	IEnumerator Flash()
	{
		_alpha = 1;
		yield return new WaitForSeconds(0.5f);
		for (float j = 1; j > 0; j -= Time.deltaTime)
		{
			_alpha = j;
			yield return null;
		}
		_alpha = 0;
		yield return new WaitForSeconds(2f);
		bFade = false;

	}
	IEnumerator Fade()
	{
		for (float i = 0; i < 1; i += Time.deltaTime * 3.5f)
		{
			_alpha = i;
			yield return null;
		}
		_alpha = 1;
		yield return new WaitForSeconds(2);
		for (float j = 1; j > 0; j -= Time.deltaTime)
		{
			_alpha = j;
			yield return null;
		}
		_alpha = 0;
		yield return new WaitForSeconds(1f);
		bFade = false;
			
	}
	// Update is called once per frame
	void Update () 
	{

        _originalPos = _cam.position;
        if (_img != null)
		{
			_img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _alpha);
		}

		 //Camera Shake
		if (camShake > 0)
		{
			Vector3 shakePos = _originalPos + Random.insideUnitSphere * shakeAmt;
		//	shakePos.z = -10;
			_cam.position = Vector3.Lerp(_cam.position, shakePos, 1);
			camShake -= Time.deltaTime;
		}		
	}
	public void CameraShake(float shakeTime, float shakeIntensity)
	{
		camShake += shakeTime;
		shakeAmt = shakeIntensity;
	
	}
	public static Vector3 OnUnitCircle(float x,float y)
	{

		float newX = Random.Range(0, x);
		float newY = Random.Range(0, y);

		return new Vector3(newX, newY, 0);
	}

}
