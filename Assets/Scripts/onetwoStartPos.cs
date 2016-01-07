using UnityEngine;
using System.Collections;

public class onetwoStartPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int gulnazpos = 0;
        if(PlayerPrefs.HasKey("caveExit"))
        {
            gulnazpos = PlayerPrefs.GetInt("caveExit");
            if(gulnazpos == 2)
            {
                gameObject.transform.position = new Vector3(-126.31f, 75.45f, -55.32f);
            }

             else if(gulnazpos == 1)
            {
                gameObject.transform.position = new Vector3(-242.77f, 63.11f, -28.32f);
            }
          
        }
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("caveExit", 0);
        } 
    }
}
