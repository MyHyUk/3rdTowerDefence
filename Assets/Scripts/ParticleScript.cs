using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {
	
	private float lifetime = 2.0f;
	private float passtime   = 0.0f;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		passtime += Time.deltaTime;
		if(passtime > lifetime)
		{
			Destroy(this.gameObject);
			passtime = 0.0f;
		}
	
	}
}
