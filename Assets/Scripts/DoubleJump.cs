using UnityEngine;
using System.Collections;

public class DoubleJump : MonoBehaviour 
{
	//Скрипт отвечает за эффект двойного прыжка
	public Vector3 StartScale;
	public Vector3 EndScale;
	private Vector3 CurrentScale;
	public float ScaleSpeed;
	private MeshRenderer render;
	private float StartAlpha;
	private float CurrentAlpha;
	private float EndAlpha = 0;

	void Start () 
	{
		render = GetComponentInChildren<MeshRenderer>();
		StartAlpha = render.material.color.a;
		CurrentAlpha = StartAlpha;

		CurrentScale = StartScale;
		transform.localScale = CurrentScale;
	}
	

	void Update () 
	{
		CurrentScale = Vector3.Lerp(CurrentScale,EndScale,Time.deltaTime*ScaleSpeed);
		transform.localScale = CurrentScale;

		CurrentAlpha = Mathf.Lerp(CurrentAlpha,EndAlpha,Time.deltaTime*ScaleSpeed);

		render.material.color = new Color(render.material.color.r,render.material.color.g,render.material.color.b,CurrentAlpha);
	}
}
