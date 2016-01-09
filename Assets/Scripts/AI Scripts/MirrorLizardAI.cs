using UnityEngine;
using System.Collections;

public class MirrorLizardAI : AIbase {
    private RaycastHit kk;
    private LineRenderer laser;
    public static bool gotLight;
    private GameObject[] handles;
    private Vector3 shootDir;
    public static bool attachedtoHandle;
    private Animator liz;
    bool losttongue;
	// Use this for initialization
	protected override void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        liz = gameObject.GetComponent<Animator>();
        //agent = gameObject.GetComponent<NavMeshAgent>();
        origin = gameObject.transform.position;
        //laser = GetComponent<LineRenderer>();
        handles = GameObject.FindGameObjectsWithTag("Handle");
	}

    protected override void ActivateAbility()
    {
        if(!attachedtoHandle)
        {
            _rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            for (int i = 0; i < handles.Length; i++)
            {
                Vector3 direction = handles[i].transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                float dist = Vector3.Distance(transform.position, handles[i].transform.position);

                if (dist <= 7 && angle <= 30 || dist <= 5 && angle >= -30)
                {
                    TongueRenderscript.shootTongue(direction);
                }
                else
                {
                    TongueRenderscript.shootTongue(transform.forward);
                }
            }
        }

        else if(attachedtoHandle)
        {
            //TongueRenderscript.lt.gameObject.transform.position = transform.position;
            TongueRenderscript.Move();
            _rigidBody.constraints = RigidbodyConstraints.None;
            attachedtoHandle = false;
        }
        
    }

    protected override void PassiveAbility()
    {
        float movespeed = _rigidBody.velocity.magnitude;
        liz.SetFloat("speed", movespeed);
        //if(gotLight)
        //{
        //    if (Physics.Raycast(transform.position, transform.forward, out kk, 200))
        //    {
        //        laser.SetPosition(0, transform.position);
        //        laser.SetPosition(1, kk.point);

        //        if(kk.collider.name == "GooPoly")
        //        {
        //            Destroy(kk.collider.gameObject);
        //        }
        //    }

        //    else
        //    {
        //        laser.SetPosition(0, transform.position);
        //        laser.SetPosition(1, transform.forward * 200);
        //    }
        //}

        //else
        //{
        //    laser.SetPosition(0, transform.position);
        //    laser.SetPosition(1, transform.position);
        //}
    }

    public static void SeekTongue()
    {
        TongueRenderscript.lt.gameObject.transform.position = TongueRenderscript.origin;
    }
}
