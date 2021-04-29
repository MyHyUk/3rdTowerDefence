using UnityEngine;
using System.Collections;

public class HealthBarScript : MonoBehaviour {
	public GameObject healthBarPrefab ;
	public GameObject healthBarObj;
	public float currHealth;
	public float maxHealth ;
	public int healthBarWidth ;

	// Use this for initialization
	void Start () 
	{
		healthBarObj = Instantiate(healthBarPrefab, transform.position,transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		currHealth = GetComponent<EnemyScript>().currentHP;
		healthBarObj.transform.position = Camera.main.WorldToViewportPoint(transform.position);
		healthBarObj.transform.Translate(0f,0.13f,0f);
		float healthPercent = (currHealth/maxHealth) * 20.0f;
		if(healthPercent <0)
		{
			healthPercent=0;
			
		}
		//healthBarWidth = int.Parse((healthPercent).ToString());
        healthBarWidth = (int)healthPercent;
		
        if(healthBarWidth < 1)
		{
			Destroy(healthBarPrefab);
		}
		healthBarObj.GetComponent<GUITexture>().pixelInset =new Rect(10,10,healthBarWidth,5);
	
	}
	
}
