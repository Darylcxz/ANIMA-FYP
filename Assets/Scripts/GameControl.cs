using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	private GameObject character;
	public static bool spiritmode = false;
    public static bool freeze = false;
	public Texture bolangu;
	public Texture spiritjotai;
    public Image possesionmode;
    private Collider[] hitcolliders;
    private int ordernum = 0;
    public GameObject pointer;
    private Vector3 heightplus = new Vector3(0, 1, 0);
    private int enemylayer;
    

	// Use this for initialization
	void Start () {

        possesionmode.enabled = false;
        character = GameObject.Find("Character");
        enemylayer = 1 << LayerMask.NameToLayer("DetectPossess");
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.GetMouseButtonDown (1)) 

		if(GamepadManager.buttonYDown || Input.GetMouseButtonDown(1))
		{
			possessModeToggle();
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
	
	}

	void possessModeToggle() {

		if (!spiritmode && !freeze) {

			spiritmode = true;
            freeze = true;
            //possesionmode.enabled = true;
			print ("possess mode activated");
            hitcolliders = Physics.OverlapSphere(character.transform.position, 5, enemylayer);
            pointer.SetActive(true);
            pointer.transform.position = hitcolliders[ordernum].transform.position + heightplus;
            Debug.Log("desssssu");

		} else if (spiritmode) {

			print ("possess mode deactivated");
			Camerafollow.targetUnit = GameObject.Find("Character");
			spiritmode = false;
            pointer.SetActive(false);
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
            pointer.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }

        else
        {
            ordernum = 0;
            pointer.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
    }

    void PrevPossessTarget()
    {
        if(ordernum - 1 != -1)
        {
            ordernum -= 1;
            pointer.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
        else
        {
            ordernum = hitcolliders.Length - 1;
            pointer.transform.position = hitcolliders[ordernum].transform.position + heightplus;
        }
    }
	
}
