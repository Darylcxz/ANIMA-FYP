using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Foreground : MonoBehaviour {
    private RectTransform foreground;
    private float horizontal;
    private float vertical;
	// Use this for initialization
	void Start () {

        foreground = gameObject.GetComponent<RectTransform>();
	
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        vertical *= -1;
        horizontal *= -1;
        Vector2 movedirection = new Vector2(horizontal, vertical);
        foreground.anchoredPosition += movedirection * 5;
	
	}
}
