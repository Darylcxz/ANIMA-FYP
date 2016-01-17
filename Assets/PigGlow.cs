using UnityEngine;
using System.Collections;

public class PigGlow : MonoBehaviour
{

	[SerializeField]SkinnedMeshRenderer pigMesh;
	float emissiveCol;
	
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		emissiveCol = (Mathf.PingPong(Time.time/2, 0.9f - 0.3f) + 0.3f);
		pigMesh.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", (new Color(emissiveCol, 0, 0)));
	}

}