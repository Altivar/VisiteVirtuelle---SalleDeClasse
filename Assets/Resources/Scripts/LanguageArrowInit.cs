using UnityEngine;
using System.Collections;

public class LanguageArrowInit : MonoBehaviour {

	public Transform EnglishTrsf;
	public Transform FrenchTrsf;

	// Use this for initialization
	void Start ()
	{
		if(Localization.GetCurrentKey() == 0) // if french
		{
			this.transform.position = FrenchTrsf.position;
			this.transform.rotation = FrenchTrsf.rotation;
		}	
		else if(Localization.GetCurrentKey() == 1) // if english
		{
			this.transform.position = EnglishTrsf.position;
			this.transform.rotation = EnglishTrsf.rotation;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
