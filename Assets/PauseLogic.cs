using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseLogic : MonoBehaviour {

    CanvasGroup PauseMenu;
    bool isPaused = false;
    float _t;

	// Use this for initialization
	void Start () {
        PauseMenu = GetComponent<CanvasGroup>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if(GamepadManager.buttonStartDown)
        {
    
          
            if (!isPaused)
            {
                isPaused = true;
            }
        }
        CheckPause();
	}
    void CheckPause()
    {
        if(isPaused)
        {
            _t += Time.fixedDeltaTime;
            PauseMenu.alpha = Mathf.Lerp(0, 1, _t * 2f);
            if(_t >0.2f)
            {           
                Time.timeScale = 0;
                PauseMenu.interactable = true;
            }
        }
        if(!isPaused)
        {
            _t += Time.fixedDeltaTime;
            PauseMenu.alpha = Mathf.Lerp(PauseMenu.alpha, 0, _t);
            if(_t> 0.5f)
            {
                Time.timeScale = 1;
                PauseMenu.interactable = false;
            }
        }
        if(_t > 1)
        {
            _t = 1;
        }
    }

    public void Unpause()
    {
        isPaused = false;
        _t = 0;
    }
    public void MainMenu()
    {
        Application.LoadLevel("MenuScene");
    }
}
