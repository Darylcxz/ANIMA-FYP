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
    private Collider[] hitcolliders;
    private int ordernum = 0;
    private Vector3 heightplus = new Vector3(0, 1, 0);
    private int enemylayer;

    //Collectibles
    [SerializeField]private Image card;
    [SerializeField]private Sprite[] cards;
    [SerializeField]private Image cardpanel;
    private bool cardshowing;
    
	//Screen Flash stuff
	float flashTimer;
	float flashAlpha = 0;
	bool bFlash;

	//Possession Vignette
	float vignetteTimer;
	float minV = 1f;
	float maxV = 1.8f;
	Vector3 currScale = new Vector3(1,1,1);
	bool bVignette;


	// Use this for initialization
	void Start () {

        //possesionmode.enabled = false;
        character = GameObject.Find("Character");
        flame = GameObject.Find("TargetSerik");
        Seriksplace = GameObject.Find("Seriksplace").transform;
        enemylayer = 1 << LayerMask.NameToLayer("DetectPossess");
		flashAlpha = 0;
        card.enabled = false;
        cardpanel.enabled = false;
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

        possesionmode.enabled = freeze;

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
			vignetteTimer += Time.deltaTime;
			possesionmode.rectTransform.localScale = currScale*(Mathf.PingPong(Time.time, maxV - minV) + minV);

		}

        if(cardshowing)
        {
            if(GamepadManager.buttonBDown)
            {
                card.enabled = false;
                cardpanel.enabled = false;
                cardshowing = false;
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

		} else if (spiritmode) {

            Camerafollow.targetUnit = character;
            Invoke("Firecallback", 0.3f);
            ordernum = 0;
            spiritmode = false;
            if(freeze)
            {
                freeze = false;
            }
		}

	}

    void NextPossessTarget()
    {
        if(ordernum != hitcolliders.Length - 1)
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
        if(ordernum - 1 != -1)
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
		bVignette = true;
	}

    public void ShowCard(int cardnumber)
    {
        cardshowing = true;
        card.enabled = true;
        cardpanel.enabled = true;
        card.sprite = cards[cardnumber];
    }

	
}
