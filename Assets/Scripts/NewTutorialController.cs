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
	[SerializeField]
	Part DialoguePart;
	string _dialogueName;
	bool textPlay;
    bool statechange;
    [SerializeField]
    Transform _camera;
	// Use this for initialization
	void Start () {
		_dScript = _dScript.GetComponent<DialogueScript>();
        _camera = _camera.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		DialogueStateMachine();

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
                //
                ChangeState(Part.SHAKE);
                break;
			case Part.SHAKE:
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
