using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VillageDialogue : DialogueScript {

    public GameObject ruslan;
    public GameObject temir;
    public GameObject serik;
    public GameObject inzhu;
    public Transform newpos;
    public Image tutImage;
    public Sprite pressb;
    public Sprite analogstick;
    public Image itemicon;
    public static bool hitDummy = false;
    public static bool interactOn = false;
    public GameObject sword;
    private bool canleave1 = false;
    private bool cockblock1 = false;
    private bool finishdummy = false;
    private bool ruslan2;
    private short serikcount = 0;
    public GameObject serikcalls;
    public GameObject helpserik1;
    public GameObject helpserik2;
    public GameObject helpserik3;

    public override void Start()
    {
        base.Start();
        sword.SetActive(false);
        NPCname = "Exittent";
        string textData = dialogue.text;
        ParseDialogue(textData);
        tutImage.enabled = false;
        itemicon.enabled = false;
    }

	// Use this for initialization
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(hitDummy && temir.name == "Temir2")
        {
            temir.name = "Temir3";
        }

        if(hitDummy && !finishdummy)
        {
            finishdummy = true;
            NPCname = "Temirhashadit";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
        }

        if(Checkhay.got2hay && Input.GetButtonDown("Action") && serik.name == "Serik4")
        {
            serik.name = "Serik5";
        }

        if(interactOn)
        {
            tutImage.enabled = true;
            tutImage.sprite = pressb;
            Invoke("TutorialOff", 5);
            interactOn = false;
        }

        if(NPCname == "Temir" && _seqNum == 4)
        {
            sword.SetActive(true);
            charanim.SetBool("bVictory", true);
        }
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if(NPCname == "Ruslan")
        {
            ruslan.name = "Ruslan2";
            canleave1 = true;
            itemicon.enabled = true;
        }

        else if(NPCname == "Exittent")
        {
            tutImage.enabled = true;
            tutImage.sprite = analogstick;
            Invoke("TutorialOff", 5);
        }

        else if(NPCname == "Serik")
        {
            serikcount += 1;
            serik.name = "Serik2";
        }

        else if (NPCname == "Serik2")
        {
            serikcount++;
            if(serikcount >= 3)
            {
                serik.name = "Serik3";
            }
        }

        else if(NPCname == "Serik4")
        {
            tutImage.enabled = true;
            Invoke("TutorialOff", 5);
        }

        else if(NPCname == "Temir")
        {
            temir.name = "Temir2";
        }

        else if(NPCname == "Temir3")
        {
            serik.transform.position = newpos.position;
            serik.name = "Serik4";
            cockblock1 = true;
            serikcalls.transform.position += new Vector3(0, -12, 0);
        }

        else if(NPCname == "Inzhu")
        {
            inzhu.name = "Inzhu2";
            itemicon.enabled = false;
        }

        else if(NPCname == "Serik4")
        {
            Destroy(helpserik1);
            Destroy(helpserik2);
            Destroy(helpserik3);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "talktoRuslan" && !canleave1)
        {
            NPCname = "leavearea1";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
        }

        else if(other.gameObject.name == "talktoRuslan" && canleave1)
        {
            Destroy(other.gameObject);
        }

        else if(other.gameObject.name == "talktoinzhu")
        {
            NPCname = "enterarea4";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
            Destroy(other.gameObject);
        }

        else if(other.gameObject.name == "talktotemir" && !cockblock1)
        {
            NPCname = "inzhucockblock";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
        }

        else if(other.gameObject.name == "talktotemir" && cockblock1)
        {
            Destroy(other.gameObject);
        }

        else if(other.gameObject.name == "talktoserik")
        {
            NPCname = "Serikcalls";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
            Destroy(other.gameObject);
            Invoke("helpserikOn", 3);
        }

        else if (other.gameObject.name == "helpserik" || other.gameObject.name == "helpserik2" || other.gameObject.name == "helpserik3")
        {
            NPCname = "Inzhucockblocksagain";
            string textdata = dialogue.text;
            ParseDialogue(textdata);
        }
    }

    void TutorialOff()
    {
        if(tutImage.enabled)
        {
            tutImage.enabled = false;
        }
        
    }

    void helpserikOn()
    {
        helpserik1.transform.position += new Vector3(0, -12, 0);
        helpserik2.transform.position += new Vector3(0, -12, 0);
        helpserik3.transform.position += new Vector3(0, -12, 0);
    }


}
