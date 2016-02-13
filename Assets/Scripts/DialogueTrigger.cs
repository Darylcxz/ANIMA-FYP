using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

    [SerializeField]
    NewTutorialController _tutControl;

    void Start()
    {
        _tutControl = _tutControl.GetComponent<NewTutorialController>();
    }

	void OnTriggerEnter(Collider _col)
	{
       if(_col.gameObject.CompareTag("Player"))
        {
            _tutControl.DialogueEnd();
            TurnOff();
        }
	}

   void TurnOff()
    {
        gameObject.SetActive(false);
    }
	//void OnTriggerExit(Collider _c)
	//{
	//	if (_c.tag == "Player")
	//	{
	//		Destroy(gameObject, 0.1f);
	//	}
	//}
}
