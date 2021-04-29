using UnityEngine;
using System.Collections;

public class GUIMannager : MonoBehaviour {
	public GUISkin gSkin;
	public GameObject GManager;
	
	
	public bool isStart;
	
	
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		isStart = GManager.GetComponent<GameManager>().isStart;
	
	}
	void OnGUI () {
		
		GUI.skin = gSkin;
		if( GUI.Button(new Rect(20,20,120,35),"Start Wave") )
		{
			GManager.SendMessage("WaveStart",SendMessageOptions.DontRequireReceiver);
		}
		
		if ( isStart == false )
		{
			GUI.Label(new Rect(140,20,120,35),"<- Click!!");
		}

		
	}
}
