using UnityEngine;
using System.Collections;

public class TwoOneDialogue : DialogueScript {
    public BoxCollider achuracol;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void TriggerDialogue()
    {
        NPCname = "archura2-1";
        string textData = dialogue.text;
        ParseDialogue(textData);
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if (NPCname == "archura2-1")
        {
            achuraAnim.SetInteger("ArchuraChat", 4);
            achuracol.enabled = false;
            print("archura fucks off");
        }
    }

    public void BossDies()
    {

    }
}
