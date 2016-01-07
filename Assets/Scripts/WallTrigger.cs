using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour {

    public GameObject _fence;
    bool _animate;
    float _t = 0f;

	// Use this for initialization
	void Start () {
        _t = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (_animate)
        {
            _t += Time.deltaTime/5.0f;
            Vector3 _newPos = new Vector3(_fence.transform.localPosition.x, _fence.transform.localPosition.y, 0.1f);
            _fence.transform.localPosition = Vector3.Lerp(_fence.transform.localPosition, _newPos, _t);
        }
        if (_t > 1.0f)
        {
            _t = 1;
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            _animate = true;
            
        }
    }
}
