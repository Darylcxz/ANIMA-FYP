using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSceneScript : MonoBehaviour {
	Transform currentMount;
	bool _trigger;
	bool _fade;
	public float _t;
	public float _t2;
	public CanvasGroup FadeObj;
	Button butt;
	bool clicked;

	public GameObject fadeTablet;
	[SerializeField]GameObject fadeBanner;


	// Use this for initialization
	void Start () {
		currentMount = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (_trigger)
		{			
			_t += Time.deltaTime/10;
		}
		if (_t > 0.08f && _trigger == true)
		{
			_trigger = false;
			clicked = false;
			butt.GetComponent<Button>().Select();
		}
		if (_t > 1)
		{
			_t = 1;
		}
		transform.position = Vector3.Lerp(transform.position, currentMount.position, _t);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, _t);
		
		Debug.Log(clicked + " clicked +" + _t + " _t +" + _t2 + " _t2");
		if (_fade && clicked ==true)
		{
			_t2 += Time.deltaTime/0.8f;
		}
		if(_t2>1 && _fade)
		{
			_t2 = 1;
		}
		if (!_fade && clicked == true)
		{
			_t2 -= Time.deltaTime/0.8f;
			
		}
		if (_t2 < 0 && !_fade)
		{
			_t2 = 0;
		}
		
		
		
		FadeObj.alpha = Mathf.Lerp(0,1,_t2);
		fadeTablet.GetComponent<MeshRenderer> ().material.SetColor ("_Color",(new Color (1, 1, 1, Mathf.Lerp (0, 1, _t2))));
		fadeBanner.GetComponent<MeshRenderer>().material.SetColor("_Color", (new Color(1, 1, 1, Mathf.Lerp(0, 1, _t2))));


	
	}
	public void SetMount(Transform mountPos)
	{
		if (!_trigger)
		{
			_t = 0;
			currentMount = mountPos;
			_trigger = true;
		}
	}
	public void Fade()
	{
		Debug.Log("clicked +" + clicked);
		if (!clicked)
		{
			_fade = !_fade;
			clicked = true;
		}
		
	}
	public void TestSelect(Button buttock)
	{
		butt = buttock;
	}

    public void loadLevel()
    {
        Application.LoadLevel("1-2 Exterior");
    }
}
