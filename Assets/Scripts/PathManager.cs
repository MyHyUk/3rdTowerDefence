using UnityEngine;
using System.Collections;

public class PathManager : MonoBehaviour {

	public Transform[] waypoints;
	private Transform thisT;
	public bool generatePathObject=true;
	
	void Awake(){
		thisT=transform;
	}
	
	void Start(){
		if(generatePathObject){
			CreateLinePath();
		}
	}
	
	void CreateLinePath(){
		
		Vector3 offsetPos=new Vector3(0, 0, 0);
		
		for(int i=1; i<waypoints.Length; i++){
				GameObject obj=new GameObject();
				obj.name="path"+i.ToString();
				
				Transform objT=obj.transform;
				objT.parent=thisT;
				
				LineRenderer line=obj.AddComponent<LineRenderer>();
				line.material=(Material)Resources.Load("PathMaterial");
				line.SetWidth(0.3f, 0.3f);
				
				line.SetPosition(0, waypoints[i-1].position+offsetPos);
				line.SetPosition(1, waypoints[i].position+offsetPos);
		}
		
		for(int i=1; i<waypoints.Length-1; i++){
			GameObject obj=(GameObject)Instantiate((GameObject)Resources.Load("wpNode"), waypoints[i].position+offsetPos, Quaternion.identity);
			obj.transform.parent=transform;
		}
		
	}
	public Transform[] GetPath(){
		return waypoints;
	}
}
