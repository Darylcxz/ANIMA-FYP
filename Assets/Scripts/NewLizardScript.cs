using UnityEngine;
using System.Collections;

public class NewLizardScript : NewAIBA {
    LineRenderer tongue;
    GameObject tongueEnd;
    Transform tongueStart;
    Vector3 targetdir;
    Vector3 mouth;
    [SerializeField]float maxTongueLength;
    float tongueforce;
    public LayerMask handlelayer;
    bool lick = false;
    bool returnlick = false;
    // Use this for initialization
    protected override void Start()
    {
        tongue = transform.GetChild(3).GetComponent<LineRenderer>();
        tongueEnd = transform.GetChild(4).gameObject;
        tongueStart = transform.GetChild(5).gameObject.transform;
        Debug.Log(transform.position);
        base.Start();
	}

    // Update is called once per frame
    protected override void ActivateAbility()
    {
        //Debug.Log("Mirzard tried to use lick!");
        tongueforce = 0;
        Collider[] handles = Physics.OverlapSphere(transform.position, 10, handlelayer);
        if (handles.Length > 0 && !lick)
        {
            Debug.Log("Mirzard used lick!");
            targetdir = handles[0].transform.position;
            mouth = tongueStart.transform.position;
            lick = true;
        }

        else if(lick)
        {
            lick = false;
            returnlick = true;
            
        }
	}

    protected override void PassiveAbility()
    {
        tongue.SetPosition(0, tongueStart.transform.position);
        tongue.SetPosition(1, tongueEnd.transform.position);

        if(lick)
        {
            tongueforce += Time.deltaTime * 2.0f;
            tongueEnd.transform.position = Vector3.Lerp(mouth, targetdir, tongueforce);
            _rbAI.velocity = Vector3.zero;
        }

        else if(!lick && returnlick)
        {
            _rbAI.velocity = Vector3.zero;
            tongueforce += Time.deltaTime * 2.0f;
            tongueEnd.transform.position = Vector3.Lerp(targetdir, mouth, tongueforce);
            if(tongueEnd.transform.position == mouth)
            {
                returnlick = false;
            }
        }


        float distsq = (tongueEnd.transform.position - tongue.gameObject.transform.position).sqrMagnitude;
        if (distsq > maxTongueLength * maxTongueLength)
        {
            
        }
    }
}
