using UnityEngine;
using System.Collections;

public class EndCloudBlock : MonoBehaviour 
{
	//Скрипт элемента замыкающего блок
	private Clouds clouds_logic;

	void Start()
	{
		clouds_logic = transform.parent.transform.parent.GetComponent<Clouds>();
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.name == "RightTriggerZone")
		{
			clouds_logic.CreateBlock();
			Destroy(transform.parent.gameObject);
		}
	}

}
