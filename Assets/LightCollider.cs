using UnityEngine;
using System.Collections;

public class LightCollider : MonoBehaviour {
    [SerializeField]TwoOneDialogue dialscript;
	void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("goo"))
        {
            col.gameObject.SendMessage("DeathByLight", SendMessageOptions.DontRequireReceiver);
            dialscript.TriggerDialogue();
            gameObject.SetActive(false);
        }
    }
}
