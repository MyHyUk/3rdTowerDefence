using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private float spawnTimeLeft;
	private float spawnTime;
	private int spawnTimeSeconds = 1;
	public int MaxEnemy ;
	private int EnemyClass;
	
	public bool isStart = false;
	
	public GUISkin gSkin;
	public GameObject AManager;
	
	public GUIStyle TowerF_Btn;
	public GUIStyle TowerW_Btn;
	public GUIStyle TowerL_Btn;
	
	

	public Texture2D Element;
	public Texture2D health;
	
	private int playerElement = 200;
	private int baseHealth = 10;
	private int playerScore;
	
	private int towerF_Cost = 50;
	private int towerW_Cost = 100;
	private int towerL_Cost = 150;
	
	public string objectToPlaceNm;
	private RaycastHit hit ;
	private Vector3 placementPos ;
	
	public GameObject Enemy_Fire;
	public GameObject Enemy_Water;
	public GameObject Enemy_Light;
	
	public GameObject FireTower;
	public GameObject WaterTower;
	public GameObject LightTower;
	
	private bool isOver = false;
	private bool isClear = false;
	
	
	public int i = 0;
	public int inc = 1;
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		if( isStart == true )
		{
			EnemyClass = Random.Range(0,3);
			spawnTimeLeft = Time.time - spawnTime;
			if(spawnTimeLeft >= spawnTimeSeconds)
			{
				if (i != MaxEnemy)
				{
					switch(EnemyClass)
					{
					case 0 :
						Instantiate (Enemy_Fire, transform.position,Quaternion.identity);
						break;
					case 1:
						Instantiate (Enemy_Water, transform.position,Quaternion.identity);
						break;
					case 2:
						Instantiate (Enemy_Light, transform.position,Quaternion.identity);
						break;
					}
					spawnTime = Time.time;
					spawnTimeLeft = 0;
					i++;
				}
			}
			if( i%5 == 0 )
				isStart = false;
			if( i == MaxEnemy)
				isClear = true;
		}

		
		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Input.GetMouseButtonDown (0))
		{
			if (Physics.Raycast (ray,out hit, Mathf.Infinity))
			{
				if ( hit.collider.tag == "BuildBox")
				{
					if (objectToPlaceNm == "FireTower")
					{
						ClickSound();
						playerElement = playerElement - towerF_Cost;
						placementPos = hit.transform.position;
						GameObject FTower  = Instantiate(FireTower,placementPos,transform.rotation) as GameObject;
						FTower.transform.Rotate(0,180,0);
						FTower.name = "FireTower" + inc.ToString();
						Destroy(hit.collider.gameObject);
						objectToPlaceNm = "";
					}
					if (objectToPlaceNm == "WaterTower"){
						ClickSound();
						playerElement = playerElement - towerW_Cost;
						placementPos = hit.transform.position;
						GameObject WTower = Instantiate (WaterTower, placementPos, transform.rotation) as GameObject;	
						WTower.transform.Rotate(0,180,0);
						WTower.name = "WaterTower" + inc.ToString();
						Destroy(hit.collider.gameObject);
						objectToPlaceNm = "";
							
					}
					if (objectToPlaceNm == "LightTower"){
						ClickSound();
						playerElement = playerElement - towerL_Cost;
						placementPos = hit.transform.position;
						GameObject LTower = Instantiate (LightTower, placementPos, transform.rotation) as GameObject;	
						LTower.transform.Rotate(0,180,0);
						LTower.name = "LightTower" + inc.ToString();
						Destroy(hit.collider.gameObject);
						objectToPlaceNm = "";
							
					}

				}
			}
		}
	}
	

	public void WaveStart()
	{
		isStart = true;
		AManager.SendMessage("StartAudio",SendMessageOptions.DontRequireReceiver);
	}
	void SendSound()
	{
		AManager.SendMessage("DieSound",SendMessageOptions.DontRequireReceiver);
	}
	void WaterSound()
	{
		AManager.SendMessage("WaterSound",SendMessageOptions.DontRequireReceiver);
	}
	void ClickSound()
	{
		AManager.SendMessage("ClickSound",SendMessageOptions.DontRequireReceiver);
	}
	void LostLife()
	{
		if(baseHealth <= 1 )
		{
			GameOver();
		}
		baseHealth --;
	}
	void GetScore()
	{
		playerScore += 100;
		playerElement += 15;
	}
	void GameOver()
	{
		isOver = true;
		
	}
	void OnGUI()
	{	
		GUI.skin =gSkin;
		
		if(isOver == true)
		{
			GUI.Label(new Rect(300,150,500,500),"Game Over");
			if( GUI.Button( new Rect(250,250,200,80),"Main Menu"))
			{
				Application.LoadLevel(0);
			}
		}
		if(isClear == true)
		{
			
			if(Application.loadedLevel == 3)
				GUI.Label(new Rect(350,100,500,500),"All Clear!!");
			else
			{
				GUI.Label(new Rect(350,100,500,500),"Level Clear!!");
			
				if( GUI.Button( new Rect(320,250,200,80),"Next Level"))
				{
					if(Application.loadedLevel == 1)
						Application.LoadLevel(2);
					
					else
						Application.LoadLevel(3);
				}
				
			}
			if( GUI.Button( new Rect(320,350,200,80),"Main Menu"))
			{
				Application.LoadLevel(0);
			}
			
		}
		
		GUI.Label(new Rect(650,50,50,50), Element);
		GUI.Label(new Rect(650,0,50,50), health);
		GUI.Label(new Rect(700,65,100,100), playerElement.ToString());
		GUI.Label(new Rect(700,15,100,30), baseHealth.ToString());
		GUI.Label(new Rect(350,0,150,30),"Score : " + playerScore.ToString());
		
		if (GUI.Button (new Rect (0,530,80,60),"",TowerF_Btn)) 
		{
			ClickSound();
			if (playerElement >= towerF_Cost)
			{
				objectToPlaceNm = "FireTower";
			}
		}
		if (GUI.Button (new Rect (70,530,80,60),"",TowerW_Btn)) 
		{
			ClickSound();
			if (playerElement >= towerW_Cost)
			{
				objectToPlaceNm = "WaterTower";
			}
		}
		if (GUI.Button (new Rect (150,530,70,60),"",TowerL_Btn)) 
		{
			ClickSound();
			if (playerElement >= towerL_Cost)
			{
				objectToPlaceNm = "LightTower";
			}
		}
		GUI.Label(new Rect(30,590,100,30), "x50");
		GUI.Label(new Rect(100,590,100,30), "x100");
		GUI.Label(new Rect(180,590,100,30), "x150");
		
	
	}
}
