using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRestarter : MonoBehaviour {

    [SerializeField] Text LoseText;
    bool lose;
    string textToDisplay;
	// Use this for initialization
	void Start () {
        LoseText = LoseText.GetComponent<Text>();
        textToDisplay = "";
	
	}
	
	// Update is called once per frame
	void Update () {
        LoseText.text = textToDisplay;
        if(lose)
        {
            LoseText.gameObject.SetActive(true);
            LoseText.CrossFadeAlpha(1f, 2.5f, true);
            if(GamepadManager.buttonBDown)
            {
                Time.timeScale = 1;
                textToDisplay = "Restarting...";
                Invoke("RestartGame", 1f);
            }
        }
	
	}
    void OnTriggerEnter(Collider col)
    {
       if(col.CompareTag("Player"))
        {
            lose = true;
            textToDisplay = "You have fallen out the map...\nPress B to continue...";
           // Time.timeScale = 0.2f;
        }
       if(col.CompareTag("monstaa"))
        {
            lose = true;
            textToDisplay = "A friendly creature has met its demise...\nPlease do not kill the friendly creatures.\nPress B to continue...";
          //  Time.timeScale = 0.2f;
        }
    }
    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        CancelInvoke("RestartGame");
    }
}
