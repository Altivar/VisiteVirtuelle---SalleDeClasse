using UnityEngine;
using System.Collections;

public class SoundTickScript : UnlightedTarget
{

	protected bool _isEnable = false;
	public Transform TickTransform;

	void Start()
	{
		_isEnable = !OptionsManager.Instance.IsSoundEnable;
		LaunchAction();
	}

	override public void LaunchAction()
	{
		
		if( _isEnable )
		{
			TickTransform.localPosition = new Vector3(0, -0.2f, 0.001f);
			_isEnable = false;
		}
		else
		{
			TickTransform.localPosition = new Vector3(0, -0.2f, -0.001f);
			_isEnable = true;
		}

		OptionsManager.Instance.IsSoundEnable = _isEnable;
		if( OptionsManager.Instance.IsSoundEnable )
			AudioManager.Instance.PlaySound("PaperWriting");
	}

}
