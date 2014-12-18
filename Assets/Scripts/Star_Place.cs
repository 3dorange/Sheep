using UnityEngine;
using System.Collections;

public class Star_Place : MonoBehaviour 
{
	//Скрипт отвечающий за перемещение места для генерации звезд
	private float MovingSpeed = 2; 
	public Transform RestartPos; 		//позиция в которой они генерятся заново
	public GameObject Star;
	private GameObject currentStar;
	private MainLogic logic;

	void Start()
	{
		logic = GameObject.Find("_MainLogic").GetComponent<MainLogic>();
		MovingSpeed = logic.Star_Speed;

		float randomN = Random.Range(0,1000);
		
		if (randomN > 500)
		{
			CreateStar();
		}
	}


	void Update () 
	{
		transform.position = new Vector3(transform.position.x + MovingSpeed*Time.deltaTime,transform.position.y,transform.position.z);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.name == "RightTriggerZone")
		{
			transform.position = RestartPos.position;
			Destroy(currentStar);
		}
		else if (other.name == "LeftTriggerZone")
		{
			//Создаем звезду если коллидимся с левым триггером

			float randomN = Random.Range(0,1000);

			if (randomN > 500)
			{
				CreateStar();
			}
		}
	}

	private void CreateStar()
	{
		//Создаем звезду
		currentStar = Instantiate(Star,CalculateStarPos(),Quaternion.identity) as GameObject;
		currentStar.transform.parent = this.transform;
		currentStar.GetComponent<Star_BackGround_Object>().CreateStar();
	}

	private Vector3 CalculateStarPos()
	{
		Vector3 result = Vector3.zero;
		float Ypos = Random.Range(-1.5f,4);

		result = new Vector3(transform.position.x,Ypos,transform.position.z);

		return result;
	}
}
