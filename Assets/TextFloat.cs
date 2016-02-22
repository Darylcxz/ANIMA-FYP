using UnityEngine;
using System.Collections;

public class TextFloat : MonoBehaviour {
    float gameTime;
    Vector3 orignalPos;
	// Use this for initialization
	void Start () {
        orignalPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameTime += Time.deltaTime;
        Vector3 newPos = new Vector3(transform.position.x, (Mathf.PingPong(gameTime / 2, orignalPos.y+50 - orignalPos.y-50) + orignalPos.y-50), transform.position.z);
        transform.position = newPos;
        Debug.Log(transform.position);
        Debug.Log(gameTime);
	}
}
