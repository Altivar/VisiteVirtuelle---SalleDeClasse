using UnityEngine;
using System.Collections;

public class LanguageScript : UnlightedTarget {
	
	public string LanguageKey = "";
	
	override public void LaunchAction()
	{
		Localization.SetLanguage(LanguageKey);
	}
}
