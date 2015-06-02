using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	private int _soundID = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(_soundID == -1)
			{
				_soundID = AudioManager.Instance.PlaySound("Toccata");
			}
			else
			{
				AudioManager.Instance.StopSound(_soundID);
				_soundID = -1;
			}
		}
	}
}
