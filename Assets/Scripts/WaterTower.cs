using UnityEngine;
using System.Collections;

public class WaterTower : MonoBehaviour {
	private string ttag = "Enemy";
	private Transform target;
	public Transform closestEnemy ;
	public float  dist;
	
	private float startTime ;
	private float  shootTimeLeft ;
	private float shootTimeSeconds = 0.8f;
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
		Transform Waters = this.transform.FindChild("ParticleSpawn");
		GameObject instantiatedProjectile  = Instantiate(projPrefab, Waters.transform.position, Waters.rotation) as GameObject;
		instantiatedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 1000.0f);
		AManager.SendMessage("WaterSound",SendMessageOptions.DontRequireReceiver);
	
		

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
			if (dist < 100.0)
			{	if (dist < closestDistSqr)
				{	closestDistSqr = dist;
					closestEnemy = taggedEnemy.transform;
				}
			}
		}	
		target = closestEnemy;
		
				
	}
}
