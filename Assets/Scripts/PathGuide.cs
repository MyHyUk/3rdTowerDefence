using UnityEngine;
using System.Collections;

public class PathGuide : MonoBehaviour {

	private ParticleSystem Guider;
	public Transform GuiderT;
	public float stepDist=1;
	public float updateRate=0.25f;
	private int wpCounter=0;
	
	
	private PathManager path;
	
	// Use this for initialization
	void Start () {
		GuiderT=(Transform)Instantiate(GuiderT);
		Guider=GuiderT.gameObject.GetComponent<ParticleSystem>();
		Guider.emissionRate=0;
		
		path=gameObject.GetComponent<PathManager>();
		
		StartCoroutine(EmitRoutine());
	}
	
	IEnumerator EmitRoutine(){
		
		yield return null;
		
		Transform[] waypoints=path.GetPath();

		
		while(true){
			
			float dist=Vector3.Distance(waypoints[wpCounter].position, GuiderT.position);
			
			float thisStep=stepDist;
			if(dist<stepDist) {
				thisStep=stepDist-dist;
				GuiderT.position=waypoints[wpCounter].position;
				
				wpCounter+=1;
				if(wpCounter>=waypoints.Length){
					wpCounter=0;
					GuiderT.position=waypoints[wpCounter].position;
				}
			}
			
			if(thisStep>0){
				//rotate towards destination
				Vector3 pos=new Vector3(waypoints[wpCounter].position.x, waypoints[wpCounter].position.y, waypoints[wpCounter].position.z);
				Vector3 dir=pos-GuiderT.position;
				//~ Quaternion wantedRot;
				if(dir!=Vector3.zero){
					Quaternion wantedRot=Quaternion.LookRotation(dir);
				
					//set particlesystem to wantedRot
					Guider.startRotation=(wantedRot.eulerAngles.y-45)*Mathf.Deg2Rad;
					
					GuiderT.LookAt(waypoints[wpCounter]);
					
					//move, with speed take distance into accrount so the unit wont over shoot
					GuiderT.Translate(Vector3.forward*thisStep);
					
					Guider.Emit(1);
				}
			}
			
			yield return new WaitForSeconds(updateRate*Time.timeScale);
		}
	}
	
	
}
