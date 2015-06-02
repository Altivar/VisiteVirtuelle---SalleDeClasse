using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioData : MonoBehaviour {
	
	public List<AudioClip> clips = null;

	public void Awake()
	{
		AudioManager.Instance.RegisterClip (name, this);
	}

	internal AudioClip GetClip()
	{
		return clips[Random.Range(0, clips.Count)];
	}

	internal bool IsMultiClip()
	{
		if(clips.Count == 1)
			return false;
		return true;
	}
}
