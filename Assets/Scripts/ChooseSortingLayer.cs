using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]

public class ChooseSortingLayer : MonoBehaviour 
{
	// Use this for initialization
	void Enable () 
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		ParticleSystem particleSystem = GetComponent<ParticleSystem>();

		particleSystem.renderer.sortingLayerID = spriteRenderer.sortingLayerID;
		particleSystem.renderer.sortingOrder = spriteRenderer.sortingOrder;
	}	

}
