using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewTutorialController : MonoBehaviour {

	[SerializeField] DialogueScript _dScript;
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
		SERIKPIG
	};
	[SerializeField] Part DialoguePart;
    [SerializeField] List<Transform> CamPositions = new List<Transform>();
    

    string _dialogueName;
	bool textPlay = true;
    bool statechange;
    [SerializeField]
    Transform _camera;
    Quaternion _orignalRot;
    bool panCam;
    int _panIndex;

    //Camera panning values
    float _timer;
    [SerializeField] int _speedMultiplier = 1;
	// Use this for initialization
	void Start () {
		_dScript = _dScript.GetComponent<DialogueScript>();
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
				_dialogueName = "defeated1-1";
                ChangeState(Part.SHOES);
                break;
			case Part.SHOES:
				_dialogueName = "shoes1-1";
                ChangeState(Part.SHOES2);
                break;
			case Part.SHOES2:
				_dialogueName = "shoes21-1";
                ChangeState(Part.PIG);
                break;
			case Part.PIG:
				_dialogueName = "pig1-1";
                ChangeState(Part.POSSESS);
                break;
			case Part.POSSESS:
				_dialogueName = "posession1-1";
                ChangeState(Part.SERIKPIG);
                break;
			case Part.SERIKPIG:
				_dialogueName = "seriksapig1-1";
         //       ChangeState(Part.DESH);
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
            _camera.position = Vector3.Lerp(transform.localPosition, CamPositions[_panIndex].position, _timer * _speedMultiplier);
            _camera.rotation = Quaternion.Slerp(transform.localRotation, CamPositions[_panIndex].rotation, _timer * _speedMultiplier);
            if (_timer > 1)
            {
                _timer = 1;
             //   panCam = false;
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
}
