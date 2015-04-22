using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour 
{

	public Material[] mats;

	public float alphaMax = 1.0f;
	public float highlightTimeInterval = 1.0f;

	public float DistanceOfInteraction = 3.0f;

	public bool IsColorForAll = true;
	public Color ColorForAll = new Color(1.0f, 0.75f, 0.0f, 1.0f);

	private float timeKeeper;
	private float alpha;

	private bool highlightingEnabled = false;

	public bool IsDoor;

	// Use this for initialization
	void Start () 
	{
		timeKeeper = 0.0f;
		alpha = 0.0f;

		foreach (Material mat in mats)
		{
			if(IsColorForAll)
			{
				mat.SetColor("_HColor", ColorForAll);
			}
			mat.SetFloat("_HColor", alpha);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

		if( highlightingEnabled )
		{
			timeKeeper += Time.deltaTime;
			alpha = Mathf.Pow(timeKeeper/highlightTimeInterval, 2) * alphaMax;
			if( timeKeeper >= highlightTimeInterval )
			{
				timeKeeper -= 2 * highlightTimeInterval;
			}
			foreach (Material mat in mats)
			{
				mat.SetFloat("_BlendAlpha", alpha);
			}
		}

	}

	public void EnableHighlight(bool hl)
	{

		if( !hl )
		{
			timeKeeper = 0.0f;
			alpha = 0.0f;
			foreach (Material mat in mats)
			{
				mat.SetFloat("_BlendAlpha", alpha);
			}
		}
		highlightingEnabled = hl;

	}
}
