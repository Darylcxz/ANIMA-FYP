﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    private GameObject flame;
    private Transform Seriksplace;
	private GameObject character;
	public static bool spiritmode = false;
    public static bool freeze = false;
    [SerializeField]private Image possesionmode;
    private Collider[] hitcolliders;
    private int ordernum = 0;
    private Vector3 heightplus = new Vector3(0, 1, 0);
    private int enemylayer;
    

	// Use this for initialization
	void Start () {

        //possesionmode.enabled = false;
        character = GameObject.Find("Character");
        flame = GameObject.Find("TargetSerik");
        Seriksplace = GameObject.Find("Seriksplace").transform;
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

	public void possessModeToggle() {

		if (!spiritmode && !freeze) {

			spiritmode = true;
            freeze = true;
            hitcolliders = Physics.OverlapSphere(character.transform.position, 5, enemylayer);
            flame.transform.SetParent(null);
            flame.transform.localPosition = hitcolliders[ordernum].transform.position + heightplus;

		} else if (spiritmode) {

            Camerafollow.targetUnit = character;
            Invoke("Firecallback", 0.3f);
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
	
}
