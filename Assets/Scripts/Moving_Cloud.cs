using UnityEngine;
using System.Collections;

public class Moving_Cloud : MonoBehaviour 
{
	//Скрипт отвечает за движение облаков на переднем и заднем фонах

	private MainLogic logic;
	private float Speed;
	public bool FrontOrBack;
	public Transform RestartPos;

	void Start () 
	{
		logic = GameObject.Find("_MainLogic").GetComponent<MainLogic>();

		if (FrontOrBack)
		{
			Speed = Random.Range(logic.Front_Cloud_Speed_Min,logic.Front_Cloud_Speed_Max);
		}
		else
		{
			Speed = Random.Range(logic.Back_Cloud_Speed_Min,logic.Back_Cloud_Speed_Max);
		}

		RandomiseStartPos();
	}
	

	void Update () 
	{
		transform.position = new Vector3(transform.position.x + Speed*Time.deltaTime,transform.position.y,transform.position.z);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.name == "RightTriggerZone")
		{
			transform.position = RestartPos.position;
			Set_Y_Pos();

		}
		else if (other.name == "LeftTriggerZone")
		{
			

		}
	}

	private void RandomiseStartPos()
	{
		float X_Pos = transform.position.x + (Random.Range(-3,11.0f))/3;
		transform.position = new Vector3(X_Pos,transform.position.y,0);
		Set_Y_Pos();
	}

	private void Set_Y_Pos()
	{
		//Рассчитываем координаты по y для тучки
		float Y_Pos = Random.Range(-3,5.5f);
		transform.position = new Vector3(transform.position.x,Y_Pos,0);
	}
}
