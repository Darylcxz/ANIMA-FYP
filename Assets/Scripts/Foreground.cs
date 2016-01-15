using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Foreground : MonoBehaviour {
    private RectTransform foreground;
    [SerializeField]private Camera maincam;
    [SerializeField]private Transform foregroundpos;
	// Use this for initialization
	void Start () {

        foreground = gameObject.GetComponent<RectTransform>();
        
	}
	
	// Update is called once per frame
	void Update () {
        foreground.anchoredPosition = maincam.WorldToScreenPoint(foregroundpos.position);
	
	}
}
