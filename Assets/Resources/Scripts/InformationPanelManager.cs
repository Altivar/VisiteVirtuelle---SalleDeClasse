using UnityEngine;
using System.Collections;

public class InformationPanelManager : MonoBehaviour {

	public Transform Visitor;
	public float EnableDistance = 3.0f;
	public float DisableDistance = 4.0f;
	private bool _isActivated = false;
	private bool _changingState = false;

	public Material Mat;
	private float _alpha;

	// Update is called once per frame
	void Update () 
	{
		Vector3 diffLocation = this.transform.position - (Visitor.transform.position - this.transform.position);
		this.transform.LookAt(diffLocation);




		if( _changingState )
		{
			if(_isActivated)
			{
				if( _alpha > 0)
				{
					_alpha -= 2 * Time.deltaTime;
				}
				else
				{
					_changingState = false;
					_isActivated = false;
				}
			}
			else
			{
				if( _alpha < 1)
				{
					_alpha += 2 * Time.deltaTime;
				}
				else
				{
					_changingState = false;
					_isActivated = true;
				}
			}
			Mat.SetFloat("_Alpha", _alpha);
		}
		else
		{
			float distance = Mathf.Pow( this.transform.position.x - Visitor.transform.position.x ,  2.0f) + Mathf.Pow( this.transform.position.z - Visitor.transform.position.z ,  2.0f);
			distance = Mathf.Pow( distance , 0.5f );

			if(_isActivated)
			{
				if( distance > DisableDistance)
					_changingState = true;
				
			}
			else
			{
				if( distance < EnableDistance)
					_changingState = true;
			}
		}



	}


}
