using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour {

    [SerializeField]
    NewTutorialController _tutControl;
    [SerializeField] bool isRecurring;
    void Start()
    {
        _tutControl = _tutControl.GetComponent<NewTutorialController>();
    }

	void OnTriggerEnter(Collider _col)
	{
       if(_col.gameObject.CompareTag("Player"))
        {
            _tutControl.DialogueEnd();
            if(!isRecurring)
            {
                TurnOff();
            }
            else if(isRecurring)
            {
                _col.gameObject.GetComponent<Transform>().position -= _col.gameObject.transform.forward * 1f;
            }
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
