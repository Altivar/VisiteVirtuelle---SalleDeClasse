using UnityEngine;
using System.Collections;

public class LanguageScript : UnlightedTarget {
	
	public string LanguageKey = "";
	public Transform FlagTransform;
	public Transform _activationTransform;


	override public void LaunchAction()
	{
		Localization.SetLanguage(LanguageKey);
		FlagTransform.position = _activationTransform.position;
		FlagTransform.rotation = _activationTransform.rotation;
	}
}
