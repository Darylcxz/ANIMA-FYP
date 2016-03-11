using UnityEngine;
using System.Collections;

public class onetwoStartPos : MonoBehaviour {
    int gulnazpos;
    Vector3 originalpos;
    // Use this for initialization
    void Start () {
        originalpos = transform.position;
        gulnazpos = 0;
        if(PlayerPrefs.HasKey("caveExit"))
        {
            gulnazpos = PlayerPrefs.GetInt("caveExit");

            if (gulnazpos == 1)
            {
                gameObject.transform.position = new Vector3(-245.7f, 62.64f, -28.97f);
                PlayerPrefs.SetInt("caveExit", 0);
            }
            else if(gulnazpos == 0)
                gameObject.transform.position = originalpos;
          
        }
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("caveExit", 0);
            PlayerPrefs.DeleteKey("FirstTime");
        } 
    }
}
