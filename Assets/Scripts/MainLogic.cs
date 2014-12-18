using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour 
{
	//Скрипт отвечающий за основную логику игры
	public float Star_Speed;
	public float Front_Cloud_Speed_Min;
	public float Front_Cloud_Speed_Max;
	public float Back_Cloud_Speed_Min;
	public float Back_Cloud_Speed_Max;

	public float Cloud_Speed;
	public float Sheep_Speed;

	private bool MusicOnOff;
	public bool SoundEffectsOnOff;

	public GameObject StartMenu;
	public GameObject Sheep;
	public Transform SheepStartPlace;
	public float CloudsStartSpeed;
	public float CloudsAcleration;
	public int CloudCounted = 0;
	public UILabel CLoudsLabel;
	public GameObject Clouds;
	private GameObject CurrentClouds;
	public float MaxCloudSpeed;
	public float TimeToWaitAfterHit;
	public float HitTime;
	public bool Running = true;
	public Transform GameObjectWithElements;

	public MenuLogic menu;
	public UILabel TutLabel;

	public AudioClip Music;

	private Sheep currentSheep;

	void Start()
	{
		OpenStartMenu();
	}

	void Update()
	{
		if (Time.time - HitTime > TimeToWaitAfterHit)
		{
			if (!Running)
			{
				Cloud_Speed = CloudsStartSpeed;
				Running = true;
				currentSheep.CanRun = true;
			}
			if (Cloud_Speed > 0 && Cloud_Speed < MaxCloudSpeed)
			{
				Cloud_Speed += CloudsAcleration*Time.deltaTime;
			}
		}
		else
		{

		}

	}

	public void Hit()
	{
		Cloud_Speed = 0;
		HitTime = Time.time;
		Running = false;
	}

	public void StartGamePressed()
	{
		CloseStartMenu();
		CreateSheep();
	}
	 
	private void CheckMusicAndSound()
	{
		AudioSource aS = GetComponent<AudioSource>();

		if (MusicOnOff)
		{
			aS.clip = Music;

			if (!aS.isPlaying)
			{
				aS.Play();
			}
		}
		else
		{
			aS.Stop();
		}
	}

	public void OpenStartMenu()
	{
		if (CloudCounted > PlayerPrefs.GetInt("CloudCountedMAX"))
		{
			PlayerPrefs.SetInt("CloudCountedMAX",CloudCounted);
		}
		CLoudsLabel.text = "Clouds " + CloudCounted + "  Max " + PlayerPrefs.GetInt("CloudCountedMAX") ;

		NGUITools.SetActive(StartMenu,true);

		menu.Open();
		menu.gamePlayed++;

		Reset();
	}

	public void PlusCloud()
	{
		CloudCounted++;
		CLoudsLabel.text = "Clouds " + CloudCounted;
	}

	private void Reset()
	{
		Cloud_Speed = 0;
		CloudCounted = 0;	

		if (CurrentClouds)
		{
			Destroy(CurrentClouds);
		}

		CurrentClouds = Instantiate(Clouds) as GameObject;
		CurrentClouds.transform.parent = GameObjectWithElements;
	}

	private void CreateSheep()
	{
		GameObject newSheep = Instantiate(Sheep,SheepStartPlace.position,Quaternion.identity) as GameObject;
		newSheep.name = "SHEEP";
		currentSheep = newSheep.GetComponent<Sheep>();
		currentSheep.CanRun = true;
//		Debug.Log(currentSheep.name);
		Cloud_Speed = CloudsStartSpeed;
	}

	private void CloseStartMenu()
	{
		NGUITools.SetActive(StartMenu,false);

		ResetTutLabel();
	}

	public void TurnMusicOnOff(bool bState)
	{
		MusicOnOff = bState;

		CheckMusicAndSound();
	}

	public void TurnSoundEffectsOnOff(bool bState)
	{
		SoundEffectsOnOff = bState;
	}

	private void ResetTutLabel()
	{
		TweenAlpha tA = TutLabel.GetComponent<TweenAlpha>();
		tA.ResetToBeginning();
		tA.enabled = true;
		NGUITools.SetActive(TutLabel.gameObject,true);
	}
}
