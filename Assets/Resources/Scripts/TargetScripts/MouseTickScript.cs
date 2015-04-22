using UnityEngine;
using System.Collections;

public class MouseTickScript : UnlightedTarget
{

	protected bool _isEnable = false;
	public Transform TickTransform;

	void Start()
	{
		_isEnable = !OptionsManager.Instance.IsMouseEnable;
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

		OptionsManager.Instance.IsMouseEnable = _isEnable;
		
	}

}
