using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GameControl : MonoBehaviour {

    private GameObject flame;
    private Transform Seriksplace;
	private GameObject character;
	public static bool spiritmode = false;
    public static bool freeze = false;
    [SerializeField]private Image possesionmode;
	[SerializeField]private Image flashImage;
    [SerializeField]private Image backcardspin;
    [SerializeField]private Image cardfx;
    private Collider[] hitcolliders;
    private int ordernum = 0;
    private Vector3 heightplus = new Vector3(0, 1.5f, 0);
    private int enemylayer;
    [SerializeField]private ColorCorrectionCurves ccc;

    //Collectibles

    //[SerializeField]private Image[] cards;
    [SerializeField]private Image cardpanel;
    private bool cardshowing;
    int mycardnumber;
    
	//Screen Flash stuff
	float flashTimer;
	float flashAlpha = 0;
	bool bFlash;

	//Fireball
	[SerializeField]
	GameObject fireBall;

	//Possession Vignette
	float vignetteTimer;
	float minV = 1f;
	float maxV = 1.8f;
	Vector3 currScale = new Vector3(1,1,1);
	bool bVignette;

    //audio stuff
    AudioSource sfx;
    [SerializeField]AudioClip screenflashsound;
    [SerializeField]AudioClip firereturn;


	// Use this for initialization
	void Start () {

        //possesionmode.enabled = false;
        character = GameObject.Find("Character");
        flame = GameObject.Find("TargetSerik");
        Seriksplace = GameObject.Find("Seriksplace").transform;
        enemylayer = 1 << LayerMask.NameToLayer("DetectPossess");
        sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
		flashAlpha = 0;
        cardpanel.gameObject.SetActive(false);
        backcardspin.gameObject.SetActive(false);
        cardfx.gameObject.SetActive(false);
        fireBall.SetActive(false);
		possesionmode.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetMouseButtonDown (1)) 

		if(GamepadManager.buttonYDown || Input.GetMouseButtonDown(1))
		{
			possessModeToggle();
		}
		if (GamepadManager.buttonYUp)
		{
			
		}
		
        if(GamepadManager.dpadRightDown && spiritmode || Input.GetKeyDown("k") && spiritmode)
        {
            NextPossessTarget();
        }

        if (GamepadManager.dpadLeftDown && spiritmode)
        {
            PrevPossessTarget();
        }


		if (bFlash)
		{
			flashTimer += Time.deltaTime;
			flashImage.color = new Color(1, 1, 1, flashAlpha);
			//lerp damping ranges that are okay are between 3-8
			flashAlpha = Mathf.Lerp(flashAlpha, 0, flashTimer/3);
			if (flashAlpha == 0)
			{
				bFlash = false;
			}
		}
		if (bVignette)
		{
		//	vignetteTimer += Time.deltaTime;
			possesionmode.rectTransform.localScale = currScale*(Mathf.PingPong(Time.time, maxV - minV) + minV);

		}

        if(cardshowing)
        {
            if(GamepadManager.buttonBDown)
            {
                cardpanel.transform.GetChild(mycardnumber).gameObject.SetActive(true);
                cardpanel.gameObject.SetActive(false);
                cardshowing = false;
                backcardspin.gameObject.SetActive(false);
                cardfx.gameObject.SetActive(false);
            }
        }
	
	}

	public void possessModeToggle() {

		if (!spiritmode && !freeze) {

			spiritmode = true;
            freeze = true;
            hitcolliders = Physics.OverlapSphere(character.transform.position, 5, enemylayer);
            flame.transform.SetParent(null);
            flame.transform.localPosition = hitcolliders[ordernum].transform.position + heightplus;
			ScreenFlash();
            sfx.PlayOneShot(screenflashsound);
            fireBall.SetActive(true);
			bVignette = true;
			possesionmode.enabled = true;
            ccc.saturation = 0;

		} else if (spiritmode) {
            sfx.PlayOneShot(firereturn);
            Camerafollow.targetUnit = character;
            Invoke("Firecallback", 0.3f);
            ordernum = 0;
            spiritmode = false;
			fireBall.SetActive(false);
			bVignette = false;
			possesionmode.enabled = false;
            ccc.saturation = 1;
            if(freeze)
            {
                freeze = false;
            }
		}

	}

    void NextPossessTarget()
    {
        sfx.PlayOneShot(firereturn);
        if (ordernum != hitcolliders.Length - 1)
        {
            ordernum++;
            flame.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }

        else
        {
            ordernum = 0;
            flame.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
    }

    void PrevPossessTarget()
    {
        sfx.PlayOneShot(firereturn);
        if (ordernum - 1 != -1)
        {
            ordernum -= 1;
            flame.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
        else
        {
            ordernum = hitcolliders.Length - 1;
            flame.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
    }

    void Firecallback()
    {
        flame.transform.position = Seriksplace.position;
        flame.transform.SetParent(character.transform);
    }
	void ScreenFlash()
	{
		flashAlpha = 1;
		flashTimer = 0;
		bFlash = true;
	}

    public void ShowCard(int cardnumber)
    {
        cardshowing = true;
        cardpanel.gameObject.SetActive(true);
        cardpanel.transform.GetChild(cardnumber).gameObject.SetActive(true);
        mycardnumber = cardnumber;
        backcardspin.gameObject.SetActive(true);
        cardfx.gameObject.SetActive(true);
    }

	public void RevertSaturation()
    {
        ccc.saturation = 1;
    }
}
