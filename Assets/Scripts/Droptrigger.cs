using UnityEngine;
using System.Collections;

public class Droptrigger : MonoBehaviour {
	// Use this for initialization
    private bool drop;
    public GameObject tile;
	void Start () {

	}

    void Update()
    {
        if (drop)
        {
            tile.transform.Translate(Vector3.forward * -2 * Time.deltaTime);
        }
    }
	
	// Update is called once per frame
    void OnTriggerEnter()
    {
        StartCoroutine(Dropthetile());
    }

    IEnumerator Dropthetile()
    {
        yield return new WaitForSeconds(1);
        drop = true;

    }
}
