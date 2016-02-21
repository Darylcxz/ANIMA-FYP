using UnityEngine;
using System.Collections;

public class LogsBreak : MonoBehaviour {
    [SerializeField] GameObject deshWallCollider;
    [SerializeField] NewTutorialController _tutScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "dagger")
		{
			gameObject.GetComponent<Animator>().SetBool("bFall", true);
			GameObject.FindGameObjectWithTag("DeshWall").GetComponent<Animator>().SetBool("bDie", true);
			Destroy(GameObject.FindGameObjectWithTag("DeshWall").gameObject, 10f);
            Destroy(deshWallCollider);
            _tutScript.SendMessage("OnLogsFall", SendMessageOptions.DontRequireReceiver);
		}
	}
}
