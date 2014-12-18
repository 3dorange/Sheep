using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour 
{
	//Класс отвечающий за прыжки и анимации овцы

	private bool Jumping;
	private float SpeedY;
	private MainLogic logic;
	private Rigidbody2D rBody;
	public bool Grounded = false;
	private bool DoubleJumping = false;
	private GameObject LastCloud = null;
	public GameObject DoubleJumpEffect;

	public GameObject JumpOverParticle;
	public GameObject ZaborDeathParticle;
	private Animator SheepAnimator;

	private float LastPosY;

	private float JumpStartTime;
	private float JumpingWaitTime = 0.25f;

	public bool CanRun = false;
	private bool AlreadyJumped = false;

	private bool GameStarted = false;

	private float StartGameTime;
	private float StartGameWaitTime = 0.3f;

	//звук
	private AudioSource audioS;
	public AudioClip[] JumpSound;
	public AudioClip JumpLandSound;
	public AudioClip HitSound;
	public AudioClip GoingDownSound;

	void Start()
	{
		logic = GameObject.Find("_MainLogic").GetComponent<MainLogic>();
		rBody = transform.GetComponent<Rigidbody2D>();
		audioS = GetComponent<AudioSource>();

		SheepAnimator = GetComponent<Animator>();

		LastPosY = transform.position.y;

		SpeedY = logic.Sheep_Speed;
		StartGameTime = Time.time;
	}

	void Update()
	{
		if (Grounded)
		{
			if (logic.Running)
			{
				PlayRunAnimation();
			}
		}
		else
		{
//			SheepAnimator.SetBool("Run",false);
			if (CanRun)
			{
				if (transform.position.y < LastPosY)
				{
					PlayJumpDownAnimation();
				}
			}
		}

		if (CanRun)
		{
			GetKeys();
		}

		LastPosY = transform.position.y;

		if (!GameStarted)
		{
			if (Time.time - StartGameTime > StartGameWaitTime)
			{
				GameStarted = true;

				EasyTouch.On_TouchStart += On_TouchStart;
				EasyTouch.On_TouchDown += On_TouchDown;
				EasyTouch.On_TouchUp += On_TouchUp;
			}
		}
//		Debug.Log(CanRun);
	}

	void FixedUpdate()
	{
		if (Jumping)
		{
			if (Time.time - JumpStartTime < JumpingWaitTime)
			{
				JumpingMove();
			}
			else
			{
				Jumping = false;
			}			
		}
	}

	private void GetKeys()
	{
		if(!Application.isMobilePlatform)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				StartJump();
			}
			
			if (Input.GetKeyUp(KeyCode.Space))
			{
				Jumping = false;
			}
		}
		else
		{

		}
	}

	private void StartJump()
	{
		if (!AlreadyJumped && Grounded)
		{
			AlreadyJumped = true;
			Jumping = true;
			Grounded = false;
			PlayJumpAnimation();
			PlaceEffect(JumpOverParticle,null).transform.parent = GameObject.FindGameObjectWithTag("Cloud").transform;
			rBody.AddForce(Vector2.up*SpeedY*2000);
			JumpStartTime = Time.time;
		}
		else
		{
			if (!DoubleJumping)
			{
				DoubleJump();
				PlayJumpAnimation();
			}
		}
	}

	private void JumpingMove()
	{
		LastCloud = null;

//		if (Grounded)
//		{
//			Jumping = true;
//			Grounded = false;
//			PlayJumpAnimation();
//			PlaceEffect(JumpOverParticle,null).transform.parent = GameObject.FindGameObjectWithTag("Cloud").transform;
			rBody.AddForce(Vector2.up*SpeedY*350);
//		}
//		else
//		{
//			if (!DoubleJumping)
//			{
//				DoubleJump();
//				PlayJumpAnimation();
//			}
//		}
	}

	private void DoubleJump()
	{
		DoubleJumping = true;
		rBody.velocity = Vector2.zero;

		PlaceEffect(DoubleJumpEffect,null).transform.parent = GameObject.FindGameObjectWithTag("Cloud").transform;

		rBody.AddForce(Vector2.up*SpeedY*5000);
	}

	private void GameOver()
	{
//		Debug.Log("GameOver");
		logic.OpenStartMenu();

		if (logic.SoundEffectsOnOff)
		{
			audioS.Stop();
			audioS.clip = GoingDownSound;
			audioS.Play();
		}

		Destroy(this.gameObject,1.5f);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
//		Debug.Log("OnTriggerEnter2D " +other.name );
		if (other.name == "GameOverTrigger")
		{
			GameOver();
		}
	}

	private GameObject PlaceEffect(GameObject effect,GameObject target)
	{
		Vector3 Pos = Vector3.zero;

		if (target)
		{
			Pos = target.transform.position;
		}
		else
		{
			Pos = new Vector3(transform.position.x,transform.position.y - GetComponent<BoxCollider2D>().size.y/2,0);
		}

		
		GameObject newEffect = Instantiate(effect,Pos,Quaternion.identity) as GameObject;

		if (newEffect.GetComponent<ParticleSystem>())
		{
			Destroy(newEffect,newEffect.GetComponent<ParticleSystem>().startLifetime*10);
		}
		else
		{
			Destroy(newEffect,3);
		}

		return newEffect;
	}

	private void JumpOverCloud(GameObject colliderObject)
	{
		PlaceEffect(JumpOverParticle,null).transform.parent = colliderObject.transform;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
//		Debug.Log("OnCollisionEnter2D");
		if (coll.gameObject.tag == "Cloud")
		{
			Grounded = true;
			Jumping = false;
			AlreadyJumped = false;
			DoubleJumping = false;

			if (LastCloud != coll.gameObject)
			{
				LastCloud = coll.gameObject;
				JumpOverCloud(LastCloud);
			}
			logic.PlusCloud();

			if (logic.SoundEffectsOnOff)
			{
				audioS.Stop();
				audioS.clip = JumpLandSound;
				audioS.Play();
			}
		}
		else if (coll.gameObject.tag == "Zabor")
		{
			HitZabor(coll.gameObject);
			Destroy(coll.transform.parent.gameObject);
		}
	}

	private void HitZabor(GameObject target)
	{
//		rBody.AddForce(Vector2.right*8000);
		PlaceEffect(ZaborDeathParticle,target).transform.parent = GameObject.FindGameObjectWithTag("Cloud").transform;
		logic.Hit();
		PlayHitZaborAnimation();
		CanRun = false;
	}

	void OnCollisionExit2D(Collision2D coll) 
	{
//		Debug.Log("OnCollisionExit2D");
		if (coll.gameObject.tag == "Cloud")
		{
			Grounded = false;
		}
	}

	private void PlayJumpAnimation()
	{
//		Debug.Log("PlayJumpAnimation");
		SheepAnimator.SetBool("Jump",true);
		SheepAnimator.SetBool("FlyUp",false);
		SheepAnimator.SetBool("Run",false);
		SheepAnimator.SetBool("FlyDown",false);
		SheepAnimator.SetBool("JumpOver",false);
		SheepAnimator.SetBool("HitZabor",false);

		if (logic.SoundEffectsOnOff)
		{
			audioS.Stop();
			int nR = (int) Random.Range(0.0f,3.0f);
			audioS.clip = JumpSound[nR];
			audioS.Play();
		}
	}

	private void PlayJumpUpAnimation()
	{
		SheepAnimator.SetBool("Jump",false);
		SheepAnimator.SetBool("FlyUp",true);
		SheepAnimator.SetBool("Run",false);
		SheepAnimator.SetBool("FlyDown",false);
		SheepAnimator.SetBool("JumpOver",false);
		SheepAnimator.SetBool("HitZabor",false);
	}

	private void PlayJumpDownAnimation()
	{
		SheepAnimator.SetBool("Jump",false);
		SheepAnimator.SetBool("FlyUp",false);
		SheepAnimator.SetBool("Run",false);
		SheepAnimator.SetBool("FlyDown",true);
		SheepAnimator.SetBool("JumpOver",false);
		SheepAnimator.SetBool("HitZabor",false);
	}

	private void PlayJumpEndAnimation()
	{
		SheepAnimator.SetBool("Jump",false);
		SheepAnimator.SetBool("FlyUp",false);
		SheepAnimator.SetBool("Run",false);
		SheepAnimator.SetBool("FlyDown",false);
		SheepAnimator.SetBool("JumpOver",true);
		SheepAnimator.SetBool("HitZabor",false);
	}

	private void PlayRunAnimation()
	{
		SheepAnimator.SetBool("Jump",false);
		SheepAnimator.SetBool("FlyUp",false);
		SheepAnimator.SetBool("Run",true);
		SheepAnimator.SetBool("FlyDown",false);
		SheepAnimator.SetBool("JumpOver",false);
		SheepAnimator.SetBool("HitZabor",false);
	}

	private void PlayHitZaborAnimation()
	{
		SheepAnimator.SetBool("Jump",false);
		SheepAnimator.SetBool("FlyUp",false);
		SheepAnimator.SetBool("Run",false);
		SheepAnimator.SetBool("FlyDown",false);
		SheepAnimator.SetBool("JumpOver",false);
		SheepAnimator.SetBool("HitZabor",true);

		if (logic.SoundEffectsOnOff)
		{
			audioS.Stop();
			audioS.clip = HitSound;
			audioS.Play();
		}
	}

	public void JumpAnimationEnds()
	{
//		Debug.Log("JumpAnimationEnds");
		PlayJumpUpAnimation();
	}

	// At the touch beginning 
	public void On_TouchStart(Gesture gesture)
	{		
		// Verification that the action on the object
		if (CanRun)
		{
			StartJump();	
		}
	}
	
	// During the touch is down
	public void On_TouchDown(Gesture gesture)
	{		
		// Verification that the action on the object

	}

	// At the touch end
	public void On_TouchUp(Gesture gesture)
	{		
		// Verification that the action on the object
		Jumping = false;
	}

	void OnDisable()
	{
		UnsubscribeEvent();
	}
	
	void OnDestroy()
	{
		UnsubscribeEvent();
	}
	
	void UnsubscribeEvent()
	{
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_TouchUp -= On_TouchUp;
	}
}
