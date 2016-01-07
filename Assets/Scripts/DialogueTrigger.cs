using UnityEngine;
using System.Collections;

public class DialogueTrigger : DialogueScript {
	//bool _triggered = false;

//	public MovementController _mScript2;

	//public override void Start()
	//{
	//	base.Start();
	//	//_mScript2 = GetComponent<MovementController>();
	//}

	// Use this for initialization
	//public override void Start () {
	//	base.Start();
	
	//}
	
	// Update is called once per frame
	//public override void Update () {
	//	base.Update();
	//}
	void OnTriggerEnter(Collider _col)
	{
		if (_col.tag == "Player" )
		{
			NPCname = gameObject.name;
			string textData = dialogue.text;
			ParseDialogue(textData);
			//_triggered = true;
		//	Destroy(gameObject, 5.0f);
		}
	}
	void OnTriggerExit(Collider _c)
	{
		if (_c.tag == "Player")
		{
			Destroy(gameObject, 0.1f);
		}
	}
}
