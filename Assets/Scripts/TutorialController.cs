using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	//public GameObject _charCam;
	//public GameObject _camMovie;
	public Image _rtText;
	public Image _aText;
	public Image _bText;
	public Image _xText;
	public Image _yText;

	public Camera _camChar;
	public Camera _movieCam;

	public CameraPanning CamPanScript;
	public DialogueScript _dScript;
	public List<GameObject> mountList = new List<GameObject>();
	public MovementController _moveScript;
	public DeshTutorial _dTutScript;
	public GameObject Serik;
	public GameObject TargetSerik;
	GameObject _Gulnaz;

	public int sequenceNum;

	public Animator deshControl;

	public Transform _target;
	public NavMeshAgent _desh;

	//public DialogueScript _dScript;

	bool ready;
	float _camSize = 4.0f;
	float _t;
	bool bTimer = true;
	float _temp;

	bool slowT;

	// Use this for initialization
	void Start () {
		
		//_charCam.SetActive(false);
		CamPanScript = GameObject.FindGameObjectWithTag("Camera").GetComponent<CameraPanning>();
		_dScript = GameObject.FindGameObjectWithTag("GulnazBody").GetComponent<DialogueScript>();
		_camChar.GetComponent<Camera>();
		_movieCam.GetComponent<Camera>();
		_camChar.enabled = false;
		deshControl = GameObject.FindGameObjectWithTag("desh").GetComponent<Animator>();
		_desh = GameObject.FindGameObjectWithTag("desh").GetComponent<NavMeshAgent>();
		_moveScript = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
		_dTutScript = GameObject.FindGameObjectWithTag("desh").GetComponent<DeshTutorial>();
		_Gulnaz = GameObject.FindGameObjectWithTag("Player");

	
	}
	
	// Update is called once per frame
	 void Update () {
		 Debug.Log(DialogueScript._seqNum + "up");
		
		if (Input.GetKeyDown(KeyCode.T))
		{
			sequenceNum++;
		}
		CamPanScript.SetMount(mountList[sequenceNum].transform);
		if (DialogueScript._seqNum == 3 && DialogueScript.NPCname.Contains("desh"))
		{
			sequenceNum = 1;
			CamPanScript.SetMount(mountList[sequenceNum].transform);
			//Debug.Log("dwed");
		}
		
		
		if (bTimer)
		{
			_t += Time.deltaTime / 1.5f ;
			_temp = Mathf.Lerp(13.0f, _camSize, _t);
		}
		if (slowT)
		{
			_t += Time.deltaTime / 2f;
			Time.timeScale = Mathf.Lerp(1f, 0f, _t);
		}
		if (_t > 1)
		{
			_t = 1;
			bTimer = false;
		}
		
		switch (sequenceNum)
		{
 			case 0:
				//Zoom when enter

				Invoke("Wait", 5.0f);
				
				_movieCam.orthographicSize = _temp;//Mathf.Lerp(13.0f, _camSize, 0.1);
				bTimer = true;
				//if (!ready)
				//{
				//	_moveScript.bForcedMove = true;
				//}
				//_camChar.enabled = true;
				if (ready)
				{
					_camChar.enabled = true;
					ready = false;
					bTimer = false;
					_moveScript.bForcedMove = false;
				//	_t = 0;
					CancelInvoke();
				}
				break;
			case 1:
				//zoom to desh
				_camChar.enabled = false;
				ready = false;
				if (DialogueScript._seqNum == 4 && DialogueScript.NPCname.Contains("desh"))
				{
					sequenceNum = 2;
				}
				break;
			case 2:
				//back to player
				if (DialogueScript.NPCname.Contains("Shake"))
				{
					sequenceNum = 3;
					_t = 0;
					ready = false; 
				}
				break;
			case 3:
				slowT = true;
				if (DialogueScript._seqNum == 1)
				{
					sequenceNum = 4;
				}
				_desh.SetDestination(_target.position);
				//float waittime = 2.0f;
				
				//AHH 
				//slow-mo
				//Invoke("Wait", waittime);
	
				//if (ready)
				//{
				//	ready = false;
				//	sequenceNum = 4;
				//	Debug.Log("move to unfreeze");
				//	CancelInvoke();
				//}
				break;
			case 4:
				//it's coming for you!
				//player roll
				//show graphic
				if (DialogueScript._seqNum == 0)
				{
					_rtText.enabled = true;
 
				}
				

				if ((Input.GetKeyDown(KeyCode.LeftShift) || GamepadManager.triggerR > 0 )&& _moveScript.bForcedMove == false)
				{
					slowT = false;
					_t = 0;
					Time.timeScale = 1;
					_Gulnaz.GetComponent<Transform>().transform.localEulerAngles = new Vector3(0, 90, 0);
					sequenceNum = 5;

					
				}
				break;
			case 5:
				//back to player + roll
				//close graphic
				_rtText.enabled = false;
				_xText.enabled = true;
				//Debug.Log(_dTutScript.deshDead);
				if (_dTutScript.deshDead == true)
				{
					_xText.enabled = false;
					DialogueScript.NPCname = "defeated1-1";
					string textData = _dScript.dialogue.text;
					_dScript.ParseDialogue(textData);
					_t = 0;
					sequenceNum = 6;
				}
				
				break;
			case 6:
				//player kills desh
				Serik.GetComponent<SerikFollow>().enabled = false;
				Vector3 serikEnd = new Vector3(43, 0, -15);
				Vector3 serikLerp = Vector3.Lerp(TargetSerik.transform.position, serikEnd, _t*2);
				Serik.transform.position = serikLerp;
				
				if (DialogueScript._seqNum == 1 && DialogueScript.NPCname.Contains("shoes"))
				{
					///Serik.SetActive(false);// serik dashes away		
					bTimer = true;
				//	sequenceNum = 7;
				}
				if (_t >= 1)
				{
					bTimer = false;
					_t = 0;
					sequenceNum = 7;
					
					//_t = 0;
				}
				
				//Debug.Log("dwedwfew");
				//NPCname = gameObject.name;
			    
				
				break;
			case 7:
				if (DialogueScript._seqNum != 1)
				{
					_aText.enabled = true;
				}
				Debug.Log(_t + "_t");
				Vector3 endJumpPos = new Vector3(18, 0, -17);
				Vector3 jumpLerp = Vector3.Lerp(_Gulnaz.transform.position, endJumpPos, _t/10);
				_Gulnaz.transform.position = jumpLerp;
			//	_moveScript.bForcedMove = true;
				if (Input.GetKeyDown(KeyCode.Space) || GamepadManager.buttonA)
				{
					_Gulnaz.GetComponent<Animator>().SetBool("bLeap", true);
					_Gulnaz.GetComponent<Transform>().localEulerAngles = new Vector3(0, 90, 0);
					bTimer = true;
									
				}
				if (_t >= 1)
				{
					bTimer = false;
					sequenceNum = 8;
					_t = 0;
				}
				break;
			case 8:
				_aText.enabled = false;
				_Gulnaz.GetComponent<Animator>().SetBool("bLeap", false);
				sequenceNum = 9;
				
				break;
			case 9:
				//Serik at shoes before dashing off AGAIN
				serikEnd = new Vector3(38, 0, -6);
				serikLerp = Vector3.Lerp(Serik.transform.position, serikEnd, _t);
				Serik.transform.position = serikLerp;
				if (DialogueScript._seqNum == 2)
				{
					bTimer = true;
				//	Debug.Log("Serik runs away again");
				}
				if (_t >= 1)
				{
					bTimer = false;
					Serik.SetActive(false);
					sequenceNum = 10;
					_t = 0;
				}
				break;
			case 10:
				if (DialogueScript.NPCname.Contains("posession"))
				{
					Serik.SetActive(true);
					Serik.GetComponent<SerikFollow>().enabled = true;
					if (DialogueScript._seqNum == 7)
					{
						sequenceNum = 11;
					}
				}
				
				
				break;
			case 11:
				if (DialogueScript._seqNum == 0 )
				{
					_yText.enabled = true;
					
				}
				if (GamepadManager.buttonY || Input.GetMouseButtonDown(1))
				{
					sequenceNum = 12;
				}
				break;
			case 12:
				_yText.enabled = false;
				break;


		}
	
	}
	void Wait()
	{
		ready = true;
	}
}
