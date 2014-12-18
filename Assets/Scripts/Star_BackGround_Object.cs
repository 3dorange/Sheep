using UnityEngine;
using System.Collections;

public class Star_BackGround_Object : MonoBehaviour 
{
	//Класс отвечающий за генерацию префабов звезд на заднем плане
	public Transform StarLine;
	public Transform[] Stars;

	public void CreateStar()
	{
		//устанавливаем звезду
//		SetPosition();
		TurnStarOn();
		SetScale();
	}

	private void SetScale()
	{
		//устанавливаем длину звезды

		StarLine.localScale = new Vector3(StarLine.localScale.x,CalculateScaleForLine(),1);
	}

	private float CalculateScaleForLine()
	{
		//рассчитываем на сколько должна изменится длина линии держащей звезду
		float newScale = 5;

		return newScale;
	}

	private void TurnStarOn()
	{
		//включаем случайную картинку звезды
		int RandomNumber = (int) Random.Range(0.0f,Stars.Length);

		foreach (Transform element in Stars)
		{
			element.gameObject.SetActive(false);
		}

		Stars[RandomNumber].gameObject.SetActive(true);
	}
}
