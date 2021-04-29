using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	public float rotationDamping = 6.0f;
	private float accel = 10.0f;
	public Transform[] wpT;
	private string[] wpS = {"WayPoint_01","WayPoint_02","WayPoint_03","WayPoint_04","WayPoint_05","WayPoint_06","WayPoint_07",};
	private int currentWP = 1;
	private bool isDead = false;
	
	public int currentHP;
	
	public GameObject Gmanager;
	public GameObject Pmanager;
	
	
	public int thisClass;
	
	public GameObject DeadParticle;
	
	
	
	
	
	
	void Start () {
		Pmanager = GameObject.Find("Path");
		int length = Pmanager.GetComponent<PathManager>().waypoints.Length;
		for(int i = 0 ; i < length ; i++)
		{
			wpT[i] = GameObject.Find(wpS[i]).transform;
		}
		Gmanager = GameObject.Find("GameManager");

		
	}

	// Update is called once per frame
	void Update () {
		
		if(isDead == false)
			Move();
		
		if(currentHP <= 0)
		{
			currentHP =0;
			Instantiate(DeadParticle,transform.position,transform.rotation);
			isDead= true;
			Gmanager.SendMessage("SendSound",SendMessageOptions.DontRequireReceiver);
			Gmanager.SendMessage("GetScore",SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}

	}
	
	void Move()
	{
		Quaternion rotation = Quaternion.LookRotation(    wpT[currentWP].position-transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
		Vector3 waypointDirection = wpT[currentWP].position - transform.position;
		float speedFactor = Vector3.Dot(waypointDirection.normalized, transform.forward);
		float speed = accel * speedFactor;
		transform.Translate(0,0, Time.deltaTime * speed);		
	}
	void OnTriggerEnter(Collider col)
	{
		if( col.tag == "wpNode" )
			currentWP++;
		if( col.tag == "DeadZone" )
		{
			Gmanager.SendMessage("LostLife",SendMessageOptions.DontRequireReceiver);
			Destroy(this.gameObject);
		}
		switch(col.tag)
		{
		case "Flame" :
			if(thisClass == 1)
				currentHP -= 2;
			if(thisClass == 2)
				currentHP -= 1;
			if(thisClass == 3)
				currentHP -= 3;
			break;
			
		case "Water" :
			if(thisClass == 1)
				currentHP -= 10;
			if(thisClass == 2)
				currentHP -= 4;
			if(thisClass == 3)
				currentHP -= 2;
			if(accel >= 4.0f)
			{
				accel -= 1.0f;
			}
			Gmanager.SendMessage("WaterSound",SendMessageOptions.DontRequireReceiver);
			break;
			
		case "Light" :
			if(thisClass == 1)
				currentHP -= 20;
			if(thisClass == 2)
				currentHP -= 50;
			if(thisClass == 3)
				currentHP -= 25;
			break;
		
			
			
		}
	}

}
