using UnityEngine;
using System.Collections;

public class BeetleHandle : MonoBehaviour {
    [SerializeField]
    GameObject boss;

	public void HandleStuff()
    {
        boss.SendMessage("TongueAction", SendMessageOptions.DontRequireReceiver);
    }
}
