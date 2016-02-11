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
	// Use this for initialization
	void Start () {
		_dScript = _dScript.GetComponent<DialogueScript>();
	}
	
	// Update is called once per frame
	void Update () {
		DialougeStateMachine();
	}
	void DialougeStateMachine()
	{
		switch (DialoguePart)
		{
 			case Part.ENTER:
				_dialogueName = "enter1-1";
				break;
			case Part.DESH:
				_dialogueName = "desh1-1";
				break;
			case Part.SHAKE:
				_dialogueName = "Shake1-1";
				break;
			case Part.DEFEAT:
				_dialogueName = "defeated1-1";
				break;
			case Part.SHOES:
				_dialogueName = "shoes1-1";
				break;
			case Part.SHOES2:
				_dialogueName = "shoes21-1";
				break;
			case Part.PIG:
				_dialogueName = "pig1-1";
				break;
			case Part.POSSESS:
				_dialogueName = "posession1-1";
				break;
			case Part.SERIKPIG:
				_dialogueName = "seriksapig1-1";
				break;
		}
		if(!textPlay)
		{
			PlayText(_dialogueName);
			textPlay = true;
		}
	}
	void PlayText(string _NPCNAME)
	{
		DialogueScript.NPCname = _NPCNAME;
		string textData = _dScript.dialogue.text;
		_dScript.ParseDialogue(textData);
	}
}
