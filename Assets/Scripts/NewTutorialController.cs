using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewTutorialController : MonoBehaviour {

	[SerializeField] DialogueScript _dScript;
    [SerializeField] DeshTutorial _deshScript;
    [SerializeField] NewPigScript _pigScript;
    [SerializeField] GameObject jumpSequence;
    [SerializeField] GameObject _Serik;
    [SerializeField] Animator _waterWheel;
    [SerializeField] Animator _sawBlade;

	[SerializeField]enum Part
	{
		ENTER,
		DESH,
		SHAKE,
		DEFEAT,
		SHOES,
		SHOES2,
		PIG,
		POSSESS,
		SERIKPIG,
        FIREGATE,
        LINEOFDESH
	};
  
	[SerializeField] Part DialoguePart;
    [SerializeField] List<Transform> CamPositions = new List<Transform>();
    [SerializeField] List<Image> TutorialImages = new List<Image>();
    

    string _dialogueName;
	bool textPlay = true;
    bool statechange;
    
    //Kinky camera movements

    [SerializeField]
    Transform _camera;
    Quaternion _orignalRot;
    bool panCam;
    int _panIndex;

    //Camera panning values
    float _timer;
    [SerializeField] int _speedMultiplier = 1;

    //Time slow stuff
    bool _bulletTime;
    bool _hasSlowed;

    bool serikFly;
    float flyTime;
    public bool hasJumped;
    Vector3 serikEnd;

    bool hasPigged;
    bool hasSquealed;
    bool hasPossessed;
    bool hasSneezed;

    //Puzzle sequence booleans
    bool hasVineBroke;
    bool hasLogMoved;

	// Use this for initialization
	void Start () {
		_dScript = _dScript.GetComponent<DialogueScript>();
        _deshScript = _deshScript.GetComponent<DeshTutorial>();
        _pigScript = _pigScript.GetComponent<NewPigScript>();
        _waterWheel = _waterWheel.GetComponent<Animator>();
        _sawBlade = _sawBlade.GetComponent<Animator>();
        jumpSequence.SetActive(false);
        _camera = _camera.GetComponent<Transform>();
        _dScript.hasDialogueEnd = true;
        _orignalRot = _camera.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
		DialogueStateMachine();
        Debug.Log(DialogueScript._seqNum);
    }
 
	void DialogueStateMachine()
	{
		switch (DialoguePart)
		{
 			case Part.ENTER:
				_dialogueName = "enter1-1";
                _camera.GetComponent<Camerafollow>().enabled = true;
                ChangeState(Part.DESH);
				break;
			case Part.DESH:
				_dialogueName = "desh1-1";
                ChangeState(Part.SHAKE);
                break;
			case Part.SHAKE:
                if (DialogueScript._seqNum == 3 && !panCam)
                {
                    _panIndex = 0;
                    _timer = 0;
                    panCam = true;

                }
                if (DialogueScript._seqNum == 4)
                {
                    panCam = false;
                    _camera.GetComponent<Camerafollow>().enabled = true;
                    _camera.transform.rotation = _orignalRot;
                }
                _dialogueName = "Shake1-1";
                ChangeState(Part.DEFEAT);
                break;
			case Part.DEFEAT:
                if(!_bulletTime && DialogueScript._seqNum == 1)
                {
                    StartCoroutine("SlowTime",20f);
                    _deshScript._move = true;
                    _bulletTime = true;
                }
 
                if(_hasSlowed)
                {
                    TutorialImages[0].enabled = true;
                    if (GamepadManager.triggerR > 0)
                    {
                        TutorialImages[0].enabled = false;
                        _deshScript.hasRolled = true;
                        _hasSlowed = false;
                        TutorialImages[1].enabled = true;
                    }
                }
                if(_deshScript.deshDead)
                {
                    TutorialImages[1].enabled = false;
                    DialogueEnd();
                }
                //AI moves while time is slowing.
                //Time stops when AI reaches it's location
                //Press RT to roll UI pops up 
                //Time is frozen and player can only press RT to roll
                //Once player presses the trigger, time unfreezes
                //Desh does weird shit, maybe gets stuck or something
                //Player then kills desh to proceed
        
				_dialogueName = "defeated1-1";
                ChangeState(Part.SHOES);
                break;
			case Part.SHOES:
				_dialogueName = "shoes1-1";
                ChangeState(Part.SHOES2);
                break;
			case Part.SHOES2:
                //if seq = 1 serik flies
                if(DialogueScript._seqNum == 1)
                {
                    serikFly = true;
                    serikEnd = new Vector3(43, 0, -15);
                }
                if(serikFly && DialogueScript._seqNum ==0 &&!hasJumped)
                {
                    TutorialImages[2].enabled = true;
                    jumpSequence.SetActive(true);
                }
                if(GamepadManager.buttonADown && _dScript.hasDialogueEnd)
                {
                    TutorialImages[2].enabled = false;
                    hasJumped = true;
                    serikFly = false;
                    flyTime = 0;
                }
                _dialogueName = "shoes21-1";
                ChangeState(Part.PIG);
                break;
			case Part.PIG:
                if(DialogueScript._seqNum == 2)
                {
                    serikFly = true;
                    serikEnd = new Vector3(38, 0, -6);
                }
                if(flyTime ==1)
                {
                    _Serik.SetActive(false);
                }
				_dialogueName = "pig1-1";
                ChangeState(Part.POSSESS);
                break;
			case Part.POSSESS:
                //if player within X range of pig, dialogue triggers?
                //then halfway during dialogue, press Y to possess etc.
                //make sure other stuff are hidden & disabled as well
                if(DialogueScript._seqNum==1 && !hasPigged)
                {
                    _pigScript.GoToPoint(new Vector3(40, 0, -12), true);
                    hasPigged = true;
                }
                if(DialogueScript._seqNum ==4 && !hasSquealed)
                {
                    _pigScript.PlaySqueal();
                    hasSquealed = true;
                }
                else if (DialogueScript._seqNum > 5)
                {
                    _pigScript.GoToPoint(Vector3.zero, false);
                    serikFly = false;
                    _Serik.SetActive(true);
                    _Serik.GetComponent<SerikFollow>().enabled = true;
                    DialogueEnd();
                }
				_dialogueName = "posession1-1";
                ChangeState(Part.SERIKPIG);
                break;
			case Part.SERIKPIG:
               if(_dScript.hasDialogueEnd && !hasPossessed)
                {
                    TutorialImages[3].enabled = true;
                }
               if(_pigScript.isPossessed)
                {
                    TutorialImages[3].enabled = false;
                    hasPossessed = true;
                    if(!hasSneezed)
                    {
                        TutorialImages[4].enabled = true;
                        if (GamepadManager.buttonBDown)
                        {
                            TutorialImages[4].enabled = false;
                            hasSneezed = true;
                        }
                    }
                   
                }
                if (hasPossessed && !_pigScript.isPossessed)
                {
                    DialogueEnd();
                }
                _dialogueName = "seriksapig1-1";
                ChangeState(Part.FIREGATE);
                break;
            case Part.FIREGATE:
                _dialogueName = "secndgate1-1"; 
                ChangeState(Part.LINEOFDESH);
                break;
            case Part.LINEOFDESH:


                _dialogueName = "dontidiot";
                break;
		}
		if(!_dScript.hasDialogueEnd && !textPlay)
		{
			PlayText(_dialogueName);
            textPlay = true;
		}
        if(panCam)
        {
            _timer += Time.deltaTime;
            _camera.position = Vector3.Lerp(_camera.transform.position, CamPositions[_panIndex].position, _timer * _speedMultiplier);
            _camera.rotation = Quaternion.Slerp(_camera.transform.rotation, CamPositions[_panIndex].rotation, _timer * _speedMultiplier);
            if (_timer > 1)
            {
                _timer = 1;
            }
        }
        if (serikFly)
        {
            flyTime += Time.deltaTime;
            _Serik.GetComponent<SerikFollow>().enabled = false;
            
            Vector3 serikLerp = Vector3.Lerp(_Serik.transform.position, serikEnd, flyTime);
            _Serik.transform.position = serikLerp;
            if(flyTime > 1)
            {
                flyTime = 1;
            }
        }
        
	}
   public void DialogueEnd()
    {
        textPlay = false;
        _dScript.hasDialogueEnd = false;
        statechange = true;
    }
    void ChangeState(Part _state)
    {
        if(statechange)
        {
            statechange = false;
            DialoguePart = _state;
        }
    }
	void PlayText(string _NPCNAME)
	{
		DialogueScript.NPCname = _NPCNAME;
		string textData = _dScript.dialogue.text;
		_dScript.ParseDialogue(textData);
	}

    IEnumerator SlowTime(float _timeScale)
    {
        for (float i = 100; i > _timeScale; i -= Time.deltaTime*50)
        {
            Time.timeScale = i / 100;
            yield return null;
        }
        _hasSlowed = true;
    }
}
