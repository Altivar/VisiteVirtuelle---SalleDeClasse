using UnityEngine;
using System.Collections;

public class Localize : MonoBehaviour {

	public TextMesh text = null;
	public string key = "";

	void Awake()
	{
		Localization.RegisterText (this);
	}

	void OnDestroy()
	{
		Localization.UnregisterText (this);
	}

	void OnEnable()
	{
		UpdateText ();
	}

	internal void UpdateText()
	{
		text.text = Localization.Get (key);
		text.text = text.text.Replace (":n:", "\n");
		text.text = text.text.Replace ("\"", "");
	}
}
