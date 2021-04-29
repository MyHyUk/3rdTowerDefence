using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float lifetime;
	private float passtime   = 0.0f;
	private string thistower;

	void Start()
	{
		
	}
	void Update () {
		
		passtime += Time.deltaTime;
		Delete();
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy")
		{
			Debug.Log("HIT!!");
			Delete();
		}
	}

	void Delete()
	{
		if(passtime > lifetime)
		{
			Destroy(this.gameObject);
			passtime = 0.0f;
		}
	}
	
}
