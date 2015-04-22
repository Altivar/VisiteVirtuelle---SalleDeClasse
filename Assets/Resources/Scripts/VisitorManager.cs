using UnityEngine;
using System.Collections;

public class VisitorManager : MonoBehaviour {

	public TargetManager[] targets;
	public float maxDistanceForHighlight = 3.0f;


	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

		foreach (TargetManager target in targets)
		{
			// check the distance of the object
			if( target.DistanceOfInteraction >= 
			   Mathf.Pow( 
			          Mathf.Pow( this.transform.position.x - target.transform.position.x, 2.0f) + Mathf.Pow( this.transform.position.z - target.transform.position.z, 2.0f)
			          , 0.5f) )
			{
				target.EnableHighlight(true);
			}
			else
			{
				target.EnableHighlight(false);
			}
		}

	}
}
