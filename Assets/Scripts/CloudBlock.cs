using UnityEngine;
using System.Collections;

public class CloudBlock : MonoBehaviour 
{
	//Блоки облаков
	private MainLogic logic;
	private float Speed;

	void Start () 
	{
		logic = GameObject.Find("_MainLogic").GetComponent<MainLogic>();
		Speed = logic.Cloud_Speed;
	}	

	void Update () 
	{
		Speed = logic.Cloud_Speed;
		transform.position = new Vector3(transform.position.x + Speed*Time.deltaTime,transform.position.y,transform.position.z);
	}
}
