using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour 
{
	//Скрипт отвечающий за поведение самих облаков
	private Collider2D Cloud_Collider;

	void Start () 
	{
		Cloud_Collider = transform.GetComponent<BoxCollider2D>();
		Cloud_Collider.enabled = false;
	}
	

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Sheep")
		{
			if(other.transform.position.y < transform.position.y+0.6f)
			{
				Cloud_Collider.enabled = false;
			}
			else
			{
				Cloud_Collider.enabled = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.tag == "Sheep")
		{
			if(other.transform.position.y < transform.position.y+0.6f)
			{
				Cloud_Collider.enabled = false;
			}
			else
			{
				Cloud_Collider.enabled = true;
			}
		}
	}


}
