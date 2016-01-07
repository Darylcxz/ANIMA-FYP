using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class onetwoDialogue : DialogueScript {
    public Image climbUI;
    GameObject vines;
    public BoxCollider achuracol;
	// Use this for initialization
    public override void Start()
    {
        base.Start();
        vines = GameObject.Find("ClimbVines");
        climbUI.enabled = false;
        //NPCname = "one2start";
        //string textData = dialogue.text;
        //ParseDialogue(textData);
    }
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        CheckVines();
    }

    void CheckVines()
    {
        float disttoV = Vector3.Distance(transform.position, vines.transform.position);
        if (disttoV <= 2.6)
        {
            climbUI.enabled = true;
        }

        else
            climbUI.enabled = false;
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if(NPCname == "Archuraencounter")
        {
            achuraAnim.SetInteger("ArchuraChat", 4);
            achuracol.enabled = false;
            print("archura fucks off");

            NPCname = "end1-2";
            string textData = dialogue.text;
            ParseDialogue(textData);
        }
    }
}
