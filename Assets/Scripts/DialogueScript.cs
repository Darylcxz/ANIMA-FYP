using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.IO;

public class DialogueScript : MonoBehaviour
{
	public TextAsset dialogue;
    private XmlNode texttoshow;
    public Text dialogs;
    public Image characterpic;
    public GameObject dialarrow;
    public Sprite[] chara1;
    public Sprite[] chara2;
    public Sprite chara3;
    public Sprite chara4;
    public Sprite chara5;
    public Sprite[] chara6;
    public Sprite[] chara7;
    public Sprite chara8;
    public Image textbox;
    private string charaname;
    private RaycastHit hit;
    private RaycastHit hit2;
    private bool textcomplete = false;
    private bool istalking = false;
    private bool lookatme = false;
    public AudioSource beepsound;
    public AudioClip[] voices;
    public static string NPCname;
    public static bool cantalk;
    private Vector3 center;
    private Vector3 side1;
    private Vector3 side2;
    public MovementController _mScript;
	public static int _seqNum;
    public bool hasDialogueEnd;
    //private bool faceme = false;
    public Text showname;
    public Animator charanim;
	public Animator serikAnim;
    public Animator achuraAnim;
    Animator talktarget;
	
	public virtual void Start() {
        textbox.enabled = false;
        dialogs.enabled = false;
        characterpic.enabled = false;
        showname.enabled = false;
        _mScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        //charanim = GameObject.Find("Character").GetComponent<Animator>();
		//serikAnim = GameObject.FindGameObjectWithTag("SerikGhost").GetComponent<Animator>();

	}

    public virtual void Update()
    {
        center = transform.position + new Vector3(0, 0.5f, 0);
        side1 = center + new Vector3(0.2f, 0, 0);
        side2 = center + new Vector3(-0.2f, 0, 0);
        Debug.DrawRay(center, transform.forward * 5);
        Debug.DrawRay(side1, transform.forward * 5);
        Debug.DrawRay(side2, transform.forward * 5);
    //    Debug.Log(hasDialogueEnd);
        if (Physics.Raycast(center, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side1, transform.forward, out hit, 1) && !istalking || Physics.Raycast(side2, transform.forward, out hit, 1) && !istalking)
        {

			if (GamepadManager.buttonBDown && hit.collider.tag == "talking")
            {
                NPCname = hit.collider.name;
                string textData = dialogue.text;
                ParseDialogue(textData);
                if (hit.collider.name != "Grave")
                {
                    Vector3 looktarget = transform.position;
                    looktarget.y = hit.collider.gameObject.transform.position.y;
                    hit.collider.gameObject.transform.LookAt(looktarget);
                    talktarget = hit.collider.gameObject.GetComponent<Animator>();
                    talktarget.SetBool("Talking", true);
                }
            }
			
        }

        else if (Input.GetButtonDown("Action") && textcomplete && istalking)
        {
            textcomplete = false;
            if(texttoshow.NextSibling != null)
            {
                //beepsound.PlayOneShot(beep);
           //     hasDialogueEnd = false;
                string tempstr = Nextnode(texttoshow);
                StartCoroutine(Printletters(tempstr));
			//	Debug.Log(_seqNum + "if");
				_seqNum++;
            }

            else
            {
                if(talktarget != null)
                    talktarget.SetBool("Talking", false);
                CheckNames();
                hasDialogueEnd = true;
                dialarrow.SetActive(false);
                textbox.enabled = false;
                dialogs.enabled = false;
                characterpic.enabled = false;
                showname.enabled = false;
                istalking = false;
                _mScript.bForcedMove = false;
				_seqNum = 0;
                StopAnim();
			//	Debug.Log(_seqNum + "else");
                //charanim.SetBool("isTalking", false);
                //charanim.SetBool("bVictory", false);
            }
        }

        else if (Input.GetButtonDown("Action") && !textcomplete && istalking)
        {
            textcomplete = true;
        }

        if (textcomplete)
            dialarrow.SetActive(true);

        else if (!textcomplete)
            dialarrow.SetActive(false);
    }

    public void ParseDialogue(string xmlData)
    {
        _mScript.bForcedMove = true;
        textbox.enabled = true;
        dialogs.enabled = true;
        characterpic.enabled = true;
        showname.enabled = true;
        istalking = true;
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load(new StringReader(xmlData));
		
		string xmlPathPattern = "//Dialogue";
		XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);
        foreach (XmlNode node in myNodeList)
        {
            string tempstr = FirstDialogue(node);
            StartCoroutine(Printletters(tempstr));
        }
            
	}
	
	private string FirstDialogue(XmlNode node) {
        XmlNode thenode = node[NPCname];
        XmlNode newtext = thenode.FirstChild;
        Checkchara(newtext);
        texttoshow = newtext;
        return newtext.InnerXml;
	}

    private string Nextnode(XmlNode node)
    {
        XmlNode newtextnext = node.NextSibling;
        Checkchara(newtextnext);
        texttoshow = newtextnext;
        return newtextnext.InnerXml;
    }

    private void Checkchara(XmlNode node)
    {
        string character = node.Attributes["character"].Value;
        int expression = 0;
        int voice = 0;
        int achuraChat = 0;
        if(node.Attributes["expression"] != null)
        {
            int.TryParse(node.Attributes["expression"].Value, out expression);
        }
        if(node.Attributes["voice"] != null)
        {
            int.TryParse(node.Attributes["voice"].Value, out voice);
        }

        if (node.Attributes["animation"] != null)
        {
            int.TryParse(node.Attributes["animation"].Value, out achuraChat);
        }
        switch(character)
        {
            case "Gulnaz":
                characterpic.sprite = chara1[expression];
                beepsound.Stop();
                //beepsound.PlayOneShot(voices[voice]);
                showname.text = character;
                charanim.SetBool("isTalking", true);
             //   Invoke("StopAnim", 0.2f);
                break;

            case "Serik":
                characterpic.sprite = chara2[expression];
                beepsound.Stop();
                beepsound.PlayOneShot(voices[voice]);
                showname.text = character;
                break;

            case "Temir":
                characterpic.sprite = chara4;
                showname.text = character;
                break;

            case "Ruslan":
                characterpic.sprite = chara3;
                showname.text = character;
                break;

            case "Inzhu":
                characterpic.sprite = chara5;
                showname.text = character;
                break;

            case "GhostSerik":
                characterpic.sprite = chara6[expression];
                beepsound.Stop();
                //beepsound.PlayOneShot(voices[voice]);
                showname.text = "Serik";
				serikAnim.SetBool("bSerikTalk", true);
            //    Invoke("StopAnim", 0.2f);
                break;

            case "Archura":
                characterpic.sprite = chara7[expression];
                beepsound.Stop();
                //beepsound.PlayOneShot(voices[voice]);
                showname.text = "Archura";
                achuraAnim.SetInteger("ArchuraChat", achuraChat);
                //Invoke("StopAnim", 0.2f);
                break;

            case "Pig":
                characterpic.sprite = chara8;
                showname.text = character;
                break;

            default:
                characterpic.sprite = null;
                break;
        }
    }

    IEnumerator Printletters(string sentence)
    {
        string str = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            str += sentence[i];
            if (i == sentence.Length - 1)
            {
//                print("truuuuuueeeee");
                textcomplete = true;
            }

            if(textcomplete == true)
            {
                str = sentence;
                i = sentence.Length;
            }
            dialogs.text = str;
            yield return new WaitForSeconds(0.04f);
        }
    }

    public virtual void CheckNames()
    {
        Debug.Log("checking and changing names");
    }

    void StopAnim()
    {
        charanim.SetBool("isTalking", false);
        charanim.SetBool("bVictory", false);
		serikAnim.SetBool("bSerikTalk", false);
    }
}