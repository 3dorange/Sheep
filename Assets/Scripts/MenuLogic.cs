using UnityEngine;
using System.Collections;

public class MenuLogic : MonoBehaviour 
{
	public int gamePlayed = 0;

	public GameObject SoundOn;
	public GameObject SoundOff;
	public GameObject MusicOn;
	public GameObject MusicOff;

	public GameObject PlayIcon;
	public GameObject RestartIcon;

	public MainLogic logic;

	public void TurnSoundOn()
	{
		NGUITools.SetActive(SoundOn,true);
		NGUITools.SetActive(SoundOff,false);

		logic.TurnSoundEffectsOnOff(true);

		PlayerPrefs.SetString("Sound","true");
	}


	public void TurnSoundOff()
	{
		NGUITools.SetActive(SoundOn,false);
		NGUITools.SetActive(SoundOff,true);

		logic.TurnSoundEffectsOnOff(false);

		PlayerPrefs.SetString("Sound","false");
	}

	public void TurnMusicOn()
	{
		NGUITools.SetActive(MusicOn,true);
		NGUITools.SetActive(MusicOff,false);

		logic.TurnMusicOnOff(true);

		PlayerPrefs.SetString("Music","true");
	}
	
	
	public void TurnMusicOff()
	{
		NGUITools.SetActive(MusicOn,false);
		NGUITools.SetActive(MusicOff,true);

		logic.TurnMusicOnOff(false);

		PlayerPrefs.SetString("Music","false");
	}

	public void Open()
	{
		if (PlayerPrefs.GetString("Sound") == "true")
		{
			TurnSoundOn();
		}
		else
		{
			TurnSoundOff();
		}

		if (PlayerPrefs.GetString("Music") == "true")
		{
			TurnMusicOn();
		}
		else
		{
			TurnMusicOff();
		}

		if (gamePlayed >= 1)
		{
			NGUITools.SetActive(PlayIcon,false);
			NGUITools.SetActive(RestartIcon,true);
		}
		else
		{
			NGUITools.SetActive(PlayIcon,true);
			NGUITools.SetActive(RestartIcon,false);
		}
	}
}
