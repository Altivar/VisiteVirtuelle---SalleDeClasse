using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour {


	#region SINGLETON IMPLEMENTATION
	private static OptionsManager _instance;

	public static OptionsManager Instance
	{
		get
		{
			if( _instance == null )
			{
				_instance = GameObject.FindObjectOfType<OptionsManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}


	void Awake ()
	{
		if( _instance == null )
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if( this != _instance )
			{
				Destroy(this.gameObject);
			}
		}
	}
	#endregion


	public bool IsSoundEnable = false;
	public bool IsMouseEnable = false;



}
