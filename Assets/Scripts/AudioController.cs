using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
	
	public AudioClip StartAclip;
	public AudioClip DieAclip;
	public AudioClip Click;
	public AudioClip Flame;
	public AudioClip Water;
	public AudioClip Laser;
	
	
	
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	
	}
	void StartAudio()
	{
		GetComponent<AudioSource>().PlayOneShot(StartAclip);
	}
	void DieSound()
	{
		GetComponent<AudioSource>().PlayOneShot(DieAclip);
	}
	void ClickSound()
	{
		GetComponent<AudioSource>().PlayOneShot(Click);
	}
	void FlameSound()
	{
		GetComponent<AudioSource>().PlayOneShot(Flame);
	}
	void WaterSound()
	{
		GetComponent<AudioSource>().PlayOneShot(Water);
	}
	void LaserSound()
	{
		GetComponent<AudioSource>().PlayOneShot(Laser);
	}
}
