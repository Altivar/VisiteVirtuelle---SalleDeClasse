using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	#region Singleton
	private static AudioManager _instance = null;
	internal static AudioManager Instance
	{
		get	
		{
			if(_instance == null)
			{
				GameObject audioManagerGameObject = new GameObject();
				audioManagerGameObject.name = "AudioManager";
				_instance = audioManagerGameObject.AddComponent<AudioManager>();
//				_instance._listener = _instance.gameObject.AddComponent<AudioListener>();
			}
			return _instance;
		}
	}
	#endregion

	#region Properties
	private List<AudioSource> _audioSrcList = new List<AudioSource>();
	private Dictionary<string, AudioData> _audioClipList = new Dictionary<string, AudioData>();
//	private AudioListener _listener = null;
	#endregion

	#region Methods
	internal bool RegisterClip(string a_name, AudioData a_data)
	{
		//On verifie si le son n'as pas déjà été register
		if(!_audioClipList.ContainsKey(a_name))
		{
			_audioClipList.Add(a_name, a_data);
			return true;
		}
		return false;
	}

	internal int PlaySound(string a_name, bool loop = false)
	{
		if(_audioClipList.ContainsKey(a_name))
		{
			int srcID = GetFreeAudioSourceID ();
			AudioSource src = _audioSrcList[srcID];
			AudioData data;
			_audioClipList.TryGetValue(a_name, out data);
			src.clip = data.GetClip();
			src.loop = loop;
			src.Play();
			return srcID;
		}
		Debug.LogError("No AudioClip named" + a_name + " is register");
		return -1;
	}

	internal void StopSound(int a_SoundID)
	{
		if(_audioSrcList.Count > a_SoundID && _audioSrcList[a_SoundID] != null)
			_audioSrcList [a_SoundID].Stop ();
	}

	private int GetFreeAudioSourceID()
	{
		//On check si une audioSource est libre
		for(int i=0; i<_audioSrcList.Count; i++)
		{
			if(!_audioSrcList[i].isPlaying)
				return i;
		}
		//Sinon, on crée un AudioSource
		GameObject srcGameObject = new GameObject ();
		srcGameObject.name = "AudioSource";
		AudioSource src = srcGameObject.AddComponent<AudioSource> ();
		src.transform.SetParent (transform);
		_audioSrcList.Add (src);
		return _audioSrcList.Count-1;
	}
	#endregion
}
