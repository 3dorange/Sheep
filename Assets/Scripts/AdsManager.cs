using UnityEngine;
using System.Collections;

public class AdsManager : MonoBehaviour 
{
	//Рекламный менеджер
	public enum Platform
	{
		Android,
		IOS,
		Amazon,
		WindowsPhone
	}

	public Platform currentPlatform;

	public enum ADsNetwork
	{
		ChartBoost,
		Appnext,
		UnityAds
	}
	
	public ADsNetwork currentADsNetwork;

	void Start()
	{

	}

}
