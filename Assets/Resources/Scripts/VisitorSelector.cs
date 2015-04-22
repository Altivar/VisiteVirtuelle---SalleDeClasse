﻿using UnityEngine;
using System.Collections;

public class VisitorSelector : MonoBehaviour 
{

	RaycastHit hit;
	Ray ray;
	TargetManager target;
	UnlightedTarget uTarget;

	public SpriteRenderer SelectorSprite;
	public Color BaseSpriteColor = new Color(1.0f,1.0f, 1.0f, 0.5f);
	public Color SpriteColor = new Color(0.8f,0.5f, 0.5f, 1.0f);

	public float LoadingTime = 1.5f;
	public float WaitingTime = 1.5f;
	private float _loadingState = 0.0f;
	public CircleBarShader CircleBar;

	private bool _isFading = false;
	private int _alphaFade = 1;
	private float _alphaValue = 1.0f;
	public float FadeTime = 2.0f;
	public Material FadeMaterial;
	private string _sceneToLoad;

	// Use this for initialization
	void Start () 
	{
		_loadingState = -WaitingTime;
		CircleBar.SetNewLoadingState(_loadingState/LoadingTime);
		StartScene();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if( !_isFading )
		{

			bool isDoorTargeted = false;
			bool isUnlightedTargeted = false;

			ray = new Ray (transform.position, transform.rotation * Vector3.forward * 10.0f);
			Debug.DrawLine(transform.position, transform.position + transform.rotation * Vector3.forward * 10.0f, Color.red);
			if (Physics.Raycast(ray, out hit))
			{
				target = hit.transform.gameObject.GetComponent<TargetManager>();
				if( target != null )
				{

					if( hit.distance <= target.DistanceOfInteraction )
					{

						if( target.IsDoor )
						{
							isDoorTargeted = true;
						}
						SelectorSprite.color = SpriteColor;

					}
					else
						SelectorSprite.color = BaseSpriteColor;

				}
				else // if it is not a door, check if it is an UnlightedTarget
				{
					uTarget = hit.transform.gameObject.GetComponent<UnlightedTarget>();
					if( uTarget != null )
					{
						
						if( hit.distance <= uTarget.DistanceOfInteraction )
						{

							isUnlightedTargeted = true;
							SelectorSprite.color = SpriteColor;
							
						}
						else
							SelectorSprite.color = BaseSpriteColor;
						
					}
				}

			}


			if( isDoorTargeted || isUnlightedTargeted )
			{
				_loadingState += Time.deltaTime;
				CircleBar.SetNewLoadingState(_loadingState/LoadingTime);
				SelectorSprite.color = SpriteColor;
				// if a door has been selected
				if( _loadingState/LoadingTime >= 1.0f )
				{

					// if it is door to main menu
					if( hit.transform.gameObject.tag.ToString() == "DoorMainMenu" ) 
					{
						EndScene("MainMenu");
					}

					// if it is a door to go to a room
					else if( hit.transform.gameObject.tag.ToString() == "DoorLibrary1" )
					{
						EndScene("Bibli1");
					}
					else if( hit.transform.gameObject.tag.ToString() == "DoorKinderGarten1" )
					{
						EndScene("Mater1");
					}

					// if it is an object to interact with
					else if(   hit.transform.gameObject.tag.ToString() == "ObjectTickSound"
					        || hit.transform.gameObject.tag.ToString() == "ObjectTickMouse")
					{
						uTarget.LaunchAction();
						_loadingState = -WaitingTime;
						CircleBar.SetNewLoadingState(_loadingState/LoadingTime);
					}

				}
			}
			else
			{
				_loadingState = -WaitingTime;
				CircleBar.SetNewLoadingState(_loadingState/LoadingTime);
				SelectorSprite.color = BaseSpriteColor;
			}

		}
		else
		{
			// fade in -> change scene
			if( _alphaFade > 0 )
			{
				if( _alphaValue < 1 )
					_alphaValue += Time.deltaTime / FadeTime * _alphaFade;
				else
				{
					Application.LoadLevel(_sceneToLoad);
					_isFading = false;
				}
			}
			else if( _alphaFade < 0 )
			{
				if( _alphaValue > 0 )
					_alphaValue += Time.deltaTime / FadeTime * _alphaFade;
				else
				{
					_isFading = false;
				}
			}
			FadeMaterial.SetFloat("_Alpha", _alphaValue);

		}



	}

	void StartScene()
	{
		_alphaFade = -1;
		_isFading = true;
	}

	void EndScene(string newScene)
	{
		_alphaFade = 1;
		_isFading = true;
		_sceneToLoad = newScene;
	}

}
