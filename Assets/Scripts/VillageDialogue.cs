using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VillageDialogue : DialogueScript {

    public GameObject ruslan;
    public GameObject temir;
    public GameObject serik;
    public GameObject inzhu;
    [SerializeField]private GameObject exclaim;
    [SerializeField]Transform haystacks;
    [SerializeField]Transform haydestination;
    [SerializeField]Transform dummies;
    [SerializeField]GameObject gate;
    public Transform newpos;
    public Image tutImage;
    public Sprite pressb;
    public Sprite analogstick;
    public Image itemicon;
    public static bool hitDummy = false;
    public static bool interactOn = false;
    public GameObject sword;
    private bool canleave1 = false;
    public static bool cockblock1 = false;
    private bool finishdummy = false;
    private bool ruslan2;
    private short serikcount = 0;
    public GameObject serikcalls;
    public GameObject helpserik1;
    public GameObject helpserik2;
    public GameObject helpserik3;
    AudioSource bgm;

    public override void Start()
    {
        base.Start();
        sword.SetActive(false);
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        tutImage.enabled = false;
        itemicon.enabled = false;
        exclaim.transform.position = ruslan.transform.position + Vector3.up * 2.0f;
    }

	// Use this for initialization
	
	// Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(hitDummy && temir.name == "Temir2")
        {
            temir.name = "Temir3";
            exclaim.transform.position = temir.transform.position + Vector3.up * 2.0f;
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
            exclaim.transform.position = serik.transform.position + Vector3.up * 2.0f;
        }

        if(NPCname == "Temir" && _seqNum == 4)
        {
            sword.SetActive(true);
            charanim.SetBool("bVictory", true);
        }

        if(NPCname == "Ruslan")
        {
            if (interactOn)
            {
                interactOn = false;
                tutImage.enabled = false;
            }
        }
    }

    public override void CheckNames()
    {
        base.CheckNames();
        if(NPCname == "Ruslan")
        {
            exclaim.transform.position = inzhu.transform.position + Vector3.up * 2.0f;
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

        else if(NPCname == "Temir")
        {
            temir.name = "Temir2";
            exclaim.transform.position = dummies.transform.position + Vector3.up * 2.0f;
        }

        else if(NPCname == "Temir3")
        {
            serik.transform.position = newpos.position;
            serik.name = "Serik4";
            exclaim.transform.position = newpos.position + Vector3.up * 2.0f;
            cockblock1 = true;
            serikcalls.transform.position += new Vector3(0, -11.0f, 0);
        }

        else if(NPCname == "Inzhu")
        {
            exclaim.transform.position = temir.transform.position + Vector3.up * 2.0f;
            inzhu.name = "Inzhu2";
            itemicon.enabled = false;
        }

        else if(NPCname == "Serik4")
        {
            tutImage.enabled = true;
            Invoke("TutorialOff", 5);
            Destroy(helpserik1);
            Destroy(helpserik2);
            Destroy(helpserik3);
            exclaim.transform.position = haystacks.transform.position + Vector3.up * 1.5f;
        }

        else if(NPCname == "Serik5")
        {
            exclaim.SetActive(false);
            gate.SetActive(false);
            inzhu.SetActive(false);
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

    public void helpserikOn()
    {
        helpserik1.transform.position += new Vector3(0, -11.0f, 0);
        helpserik2.transform.position += new Vector3(0, -11.0f, 0);
        helpserik3.transform.position += new Vector3(0, -11.0f, 0);
    }

    public void InteractTrigger()
    {
        tutImage.enabled = true;
        tutImage.sprite = pressb;
        //Invoke("TutorialOff", 5);
        interactOn = true;
    }

    void SerikImpressed()
    {
        exclaim.SetActive(true);
    }

    public void StartDialogue()
    {
        NPCname = "Exittent";
        string textData = dialogue.text;
        ParseDialogue(textData);
        bgm.Play();
        Debug.Log("StartofGame");
    }
}
