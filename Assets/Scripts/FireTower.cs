using UnityEngine;
using System.Collections;

public class FireTower : MonoBehaviour {
	private string ttag = "Enemy";
	private Transform target;
	public Transform closestEnemy ;
	public float  dist;
	
	private float startTime ;
	private float  shootTimeLeft ;
	private float shootTimeSeconds = 0.2f;
	public  GameObject projPrefab;
	public Vector3 targetDir;
	
	public GameObject AManager;
	

	// Use this for initialization
	void Start () {
		InvokeRepeating("getClosestEnemy",0.0f,1.0f);
		AManager = GameObject.Find("AudioManager");
	}
	
	// Update is called once per frame
	void Update () {
			
		if (target != null)
		{
			Debug.DrawLine (transform.position, target.position, Color.yellow); 
			targetDir = target.position - transform.position;
			transform.LookAt( new Vector3(target.position.x, transform.position.y, target.position.z));
			shootTimeLeft = Time.time - startTime;
			if(shootTimeLeft >= shootTimeSeconds)
			{
				Fire();
				
				startTime = Time.time;
				shootTimeLeft = 0;				
			}
		}

	
	}
	void Fire()
	{
		Transform flames = this.transform.FindChild("ParticleSpawn");
		Instantiate(projPrefab, flames.transform.position, flames.rotation);
		AManager.SendMessage("FlameSound",SendMessageOptions.DontRequireReceiver);
		

	}
	
	void getClosestEnemy()
	{
		GameObject[] taggedEnemys = GameObject.FindGameObjectsWithTag(ttag);
		float closestDistSqr = Mathf.Infinity;
		Transform  closestEnemy  = null;	 
		foreach(GameObject taggedEnemy in taggedEnemys)
		{	
			Vector3 objectPos = taggedEnemy.transform.position;
			dist = (objectPos - transform.position).sqrMagnitude;        
			if (dist < 30.0)
			{	if (dist < closestDistSqr)
				{	closestDistSqr = dist;
					closestEnemy = taggedEnemy.transform;
				}
			}
		}	
		target = closestEnemy;
		
				
	}
}