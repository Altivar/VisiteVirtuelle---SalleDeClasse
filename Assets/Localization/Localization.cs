using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class Localization {

	private static int curLanguage = 0;
	private static List<string> keys = null;
	private static Dictionary<string, string[]> locTab = null;
	private static List<Localize> registerText = new List<Localize>();

	internal static int LanguageID
	{ 
		get 
		{ 
			return curLanguage;
		}
	}

	internal static bool LoadLocalization()
	{
		keys = new List<string> ();
		locTab = new Dictionary<string, string[]> ();
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader("Assets/Resources/Localization.txt", Encoding.Default);

			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						string[] entries = line.Split(',');

						if(keys.Count == 0)
							LoadLanguages(entries);
						else
							AddEntry(entries);
					}
				}
				while (line != null);

				theReader.Close();
				return true;
			}
		}
		catch (System.Exception e)
		{
			Debug.LogError(e.Message);
			return false;
		}
	}

	static void LoadLanguages(string[] stream)
	{
		foreach (string each in stream)
			keys.Add (each);
	}

	internal static void RegisterText(Localize text)
	{
		registerText.Add (text);
	}

	internal static void UnregisterText(Localize text)
	{
		registerText.Remove (text);
	}

	static void UpdateTexts()
	{
		foreach(Localize each in registerText)
		{
			each.UpdateText();
		}
	}

	static void AddEntry(string[] stream)
	{
		string[] line = new string[keys.Count];
		for (int i=1; i<stream.Length; i++)
			line[i-1] = stream[i];
		locTab.Add (stream[0], line);
	}

	internal static void SetLanguage(string newLanguage)
	{
		if (keys == null)
			LoadLocalization ();
		if(keys.Contains(newLanguage))
		{
			curLanguage = keys.FindIndex(s => s == newLanguage);
		}
		else
		{
			Debug.LogError("No Language found for: " + newLanguage);
		}
		UpdateTexts ();
	}

	internal static string Get(string key)
	{
		if (keys == null)
			LoadLocalization ();
		string[] values = null;
		locTab.TryGetValue (key, out values);
		if (values != null)
			return values [curLanguage];
		Debug.LogWarning ("No Key for: "+key);
		return "";
	}

	internal static int GetCurrentKey()
	{
		return curLanguage;
	}
}
