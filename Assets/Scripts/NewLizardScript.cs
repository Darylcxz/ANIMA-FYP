﻿using UnityEngine;
using System.Collections;

public class NewLizardScript : NewAIBA {
    LineRenderer tongue;
    GameObject tongueEnd;
    Transform tongueStart;
    Vector3 targetdir;
    Vector3 mouth;
    Animator lizardAnim;
    [SerializeField]float maxTongueLength;
    float tongueforce;
    public LayerMask handlelayer;
    bool lick = false;
    bool returnlick = false;
    RaycastHit hit;
    // Use this for initialization
    protected override void Start()
    {
        tongue = transform.GetChild(3).GetComponent<LineRenderer>();
        tongueEnd = transform.GetChild(4).gameObject;
        tongueStart = transform.GetChild(5).gameObject.transform;
        lizardAnim = GetComponent<Animator>();
        Debug.Log(transform.position);
        base.Start();
	}

    // Update is called once per frame
    protected override void ActivateAbility()
    {
        //Debug.Log("Mirzard tried to use lick!");
        tongueforce = 0;
        lizardAnim.SetTrigger("sticktongue");
        Collider[] handles = Physics.OverlapSphere(transform.position, 10, handlelayer);
        if (handles.Length > 0 && !lick)
        {
            float angle = Vector3.Angle(transform.forward, targetdir - mouth);
            if(angle < 100)
            {
                Debug.Log("Mirzard used lick!");
                targetdir = handles[0].transform.position;
                mouth = tongueStart.transform.position;
                lick = true;
                immobilize = true;
            }
            
        }

        else if(lick)
        {
            Vector3 pulldir = transform.position - targetdir;
            handles[0].GetComponent<Rigidbody>().AddForce(pulldir * 10, ForceMode.Impulse);
            lick = false;
            returnlick = true;
        }
	}

    protected override void PassiveAbility()
    {
        lizardAnim.SetFloat("speed", _rbAI.velocity.magnitude);
        tongue.SetPosition(0, tongueStart.transform.position);
        tongue.SetPosition(1, tongueEnd.transform.position);

        if(lick)
        {
            tongueforce += Time.deltaTime * 2.0f;
            tongueEnd.transform.position = Vector3.Lerp(mouth, targetdir, tongueforce);
        }

        else if(!lick && returnlick)
        {
            tongueforce += Time.deltaTime * 2.0f;
            tongueEnd.transform.position = Vector3.Lerp(targetdir, mouth, tongueforce);
            if(tongueEnd.transform.position == mouth)
            {
                returnlick = false;
                immobilize = false;
            }
        }
        float distsq = (tongueEnd.transform.position - tongueStart.gameObject.transform.position).sqrMagnitude;
        if (distsq > maxTongueLength * maxTongueLength)
        {
            lick = false;
            returnlick = true;
        }

        if(Physics.Raycast(transform.position, Vector3.up, out hit, 20))
        {
            if(hit.collider.CompareTag("Lightsource"))
            {

            }
        }
    }
}
