using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VisitorBezierManager : MonoBehaviour {

	public BezierRail rail = null;
	[Range(0f, 1f)]
	public float alpha = 0f;
	
	public float Speed = 1.0f;
	public bool Loop = false;

	private bool _isInDecelrator = false;
	private float temporaryMultiplicator = 1.0f;

	private bool _isRailOver = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(OptionsManager.Instance.IsMouseEnable)
			return;

		if (_isRailOver)
			return;

		if( _isInDecelrator )
		{
			if( temporaryMultiplicator > 0.1f )
				temporaryMultiplicator -= Time.deltaTime * 0.5f;
		}
		else
		{
			if( temporaryMultiplicator < 1.0f )
				temporaryMultiplicator += Time.deltaTime * 0.5f;
		}
		if( temporaryMultiplicator > 1.0f) temporaryMultiplicator = 1.0f;
		if( temporaryMultiplicator < 0.1f) temporaryMultiplicator = 0.1f;

		alpha += Time.deltaTime * Speed * 0.01f * temporaryMultiplicator;
		if(alpha >= 1f)
		{
			if(Loop)
				alpha = 0f;
			else
			{
				alpha = 0.999f;
				_isRailOver = true;
			}
		}

		Transform trsf = rail.GetPosition (alpha, transform);
		transform.position = trsf.position;
		transform.rotation = trsf.rotation;
	}

	void OnTriggerEnter(Collider other) 
	{
		// do actions here
		if(other.tag != "RailDecelerator")
			return;
		_isInDecelrator = true;
	}

	void OnTriggerExit(Collider other) 
	{
		// do actions here
		if(other.tag != "RailDecelerator")
			return;
		_isInDecelrator = false;
	}



}
