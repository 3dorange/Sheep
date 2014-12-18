using UnityEngine;
using System.Collections;

public class Camera_Script : MonoBehaviour
{
	//Скрипт отвечающий за поведение камеры
	private bool SheepFounded = false;
	private GameObject Sheep = null;	

	void Update () 
	{
		if (Sheep == null)
		{
			SheepFounded = false;
		}

		if (!SheepFounded)
		{
//			Debug.Log("Sheep = null");
			Sheep = GameObject.Find("SHEEP");

			if (Sheep)
			{
				SheepFounded = true;
			}
		}
		else
		{
//			Debug.Log(transform.position);
//			transform.position = new Vector3(Sheep.transform.position.x -4 ,1,-10);
		}

	}
}
