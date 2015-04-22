using UnityEngine;
using System.Collections;

public class CircleBarShader : MonoBehaviour {

	[SerializeField]
	Color start;
	[SerializeField]
	Color end;
	
	[SerializeField]
	Material CircleMaterial;
	
	public void SetNewLoadingState(float state)
	{
		CircleMaterial.SetFloat("_Angle", Mathf.Lerp(-3.15f, 3.15f, state));
		CircleMaterial.SetColor("_Color", Color.Lerp(start, end, state));
	}

}
