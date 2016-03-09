using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CardSelector : MonoBehaviour {
    Transform originalPos; //original position of the card
    [SerializeField] Transform newPosition; //new position of the card
    [SerializeField] GameObject Card;
    [SerializeField] GameObject EventSystem;
    Button _thisButton;
    bool isShown = false; //is card selected or not?
    float _t = 0;

	// Use this for initialization
	void Start () {
        newPosition = newPosition.GetComponent<Transform>(); //gets position of the newPos transform
        originalPos = gameObject.GetComponent<Transform>();  //gets original position of the card to go back to
        _thisButton = gameObject.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        if(isShown)
        {
            //EventSystem.SetActive(false);
            EventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "temp";
            EventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "temp";
            //  _thisButton.navigation = Navigation.Mode.None;
            _t += Time.deltaTime;
            Card.transform.position = Vector3.Lerp(originalPos.position, newPosition.position, _t*2);
                 
        }
        if(!isShown)
        {
            
            _t += Time.deltaTime;
            Card.transform.position = Vector3.Lerp(Card.transform.position, originalPos.position, _t);  
        }
        if (_t > 1)
        {
            _t = 1;
        }
        
    }

    public void ShowCard()
    {
        EventSystem.GetComponent<StandaloneInputModule>().horizontalAxis = "Horizontal";
        EventSystem.GetComponent<StandaloneInputModule>().verticalAxis = "Vertical";
        _t = 0;
        Card.gameObject.transform.SetAsLastSibling();
        isShown = !isShown;
    }
}
