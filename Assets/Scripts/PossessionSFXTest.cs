using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PossessionSFXTest : MonoBehaviour {

	
	[SerializeField] Image Flash;
	float flashTimer;
	float flashAlpha = 0;
	bool bFlash;
	[SerializeField]
	Camera mainCam;
	[SerializeField]
	float vignetteIntensity;
	bool bVignette;
	float minV = -3.5f;
	float maxV = 3.5f;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput();

	}
	void CheckInput()
	{
		vignetteIntensity = Mathf.PingPong(Time.time*2, maxV - minV) + minV;
		if (bFlash)
		{
			flashTimer += Time.deltaTime;
			Flash.color = new Color(1, 1, 1, flashAlpha);
			//lerp damping ranges from 3-8
			flashAlpha = Mathf.Lerp(flashAlpha, 0, flashTimer/7);
			if (flashAlpha == 0)
			{
				bFlash = false;
			}
		}
		if (bVignette)
		{
			
			
			mainCam.GetComponent<VignetteAndChromaticAberration>().intensity = vignetteIntensity;
			if(GamepadManager.buttonA)
			{
				bVignette = false;
				mainCam.GetComponent<VignetteAndChromaticAberration>().intensity = 0;
			}
		}
		if (GamepadManager.buttonYUp)
		{
			ScreenFlash();
			TrippyEffects();
		}
	}
	void ScreenFlash()
	{
		flashAlpha = 1;
		flashTimer = 0;
		bFlash = true;
	}
	void TrippyEffects()
	{
		//Mathf.PingPong(_t * speed, maxV - minV) + minV;
		mainCam.GetComponent<VignetteAndChromaticAberration>().intensity = 0;
		bVignette = true;

	}
}
