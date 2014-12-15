using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject question;
	public GameObject shooter;
	public GameObject unlockScreen;

	public CameraFollow cameraFollow;
	public GameObject camera;

	int currentScreen = 0;

	// Iniciar Jogo

	bool start = false;

  
    int currentPandaIndex;
    public SlingShot slingshot;

   
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Pandas;
	private List<GameObject> Bamboos;
    private List<GameObject> Targets;

	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;




	private bool won;
	public bool gameended;


    // Use this for initialization
    void Start()
    {
		TurnOffOnSound ();
		question.SetActive (false);
		Pandas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Panda"));
		Bamboos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bamboo"));
		Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));


		if (GameController.SHOOTER_RESTARTING == false) {
			introScreen.SetActive (true);
			explanationScreen.SetActive(false);
			unlockScreen.SetActive (false);
			shooter.SetActive(false);

			slingshot.enabled = false;
			GameController.SHOOTER_RESTARTING = true;
			currentScreen = (GameController.SHOOTER_TUT == true) ? 0 : 1;
		}
		else {
			introScreen.SetActive (false);
			shooter.SetActive(true);
			unlockScreen.SetActive (true);
			question.SetActive (true);
			question.animation.Play("QuestionIn");

			camera.transform.position = new Vector3(18, 0, -20);



		}
    }

	void TurnOffOnSound()
	{
		
		AudioSource audio = transform.GetComponent<AudioSource> ();
		audio.enabled = GameController.BG_SOUND;
		
		if (audio.enabled)
			audio.Play ();
		else {
			audio.Stop();
		}
	}

	void Init() 
	{
		CurrentGameState = GameState.Start;
		won = false;	
		gameended = false;

	
		slingshot.PandaThrown -= Slingshot_PandaThrown; 
		slingshot.PandaThrown += Slingshot_PandaThrown;

		//AnimatePandaToSlingshot();


	}

	// Update is called once per frame
	void Update()
    {
		if (start == false) {
			Init();
			AutoResize(1920, 1080);
		}

		switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the pandas 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                   
					
					
                }
                break;
   
            case GameState.Playing:
               

				if (slingshot.slingshotState == SlingshotState.PandaFlying && (PandasBamboosTargetsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
				{
				    slingshot.enabled = false;
				    AnimateCameraToStartPosition();
				    CurrentGameState = GameState.PandaMovingToSlingshot;
				}

            break;
           
        }

    }

	void ChangeScreen()
	{
		if (currentScreen == 0) {
			explanationScreen.SetActive(true);
			explanationScreen.animation.Play("Explanation");
			currentScreen ++;
			GameController.SHOOTER_TUT = false;
		}
		else if(currentScreen == 1)
		{

			CurrentGameState = GameState.Start;
			camera.transform.position = new Vector3(18, 0, -20);
			shooter.SetActive(true);
			currentScreen ++;
			question.SetActive (true);
			question.animation.Play("QuestionIn");
			unlockScreen.SetActive (true);
		}
	}

	void UnlockClick()
	{



		slingshot.enabled = true;
		unlockScreen.SetActive (false);
		start = true;
		CurrentGameState = GameState.Start;
		Vector3 posicaoInicial = new Vector3(0, 0, -1);
		//testar camara mobvimento
		camera.transform.positionTo(2f, posicaoInicial);

		if (startGame != null)
		{
			startGame();			
		}

		slingshot.PandaThrown -= Slingshot_PandaThrown; 
		slingshot.PandaThrown += Slingshot_PandaThrown;
		AnimatePandaToSlingshot();
		
	}
	
	IEnumerator timeCount (float seconds) {
		yield return new WaitForSeconds(seconds);
		camera.GetComponent<CameraMove>().SendMessage("SetZoom", true);
		Vector3 posicaoInicial = new Vector3(18, 0, -20);
		camera.transform.positionTo(2f, posicaoInicial);
	}

    /// Animates the camera to the original location
	/// When it finishes, it checks if we have lost, won or we have other pandas available to throw
    private void AnimateCameraToStartPosition()
    {
		print("VOU VOULTAR");
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start

        Camera.main.transform.positionTo
            (duration,
            cameraFollow.StartingPosition). //end position
            setOnCompleteHandler((x) =>
	        {
				
	            cameraFollow.IsFollowing = false;

				if (currentPandaIndex == Pandas.Count - 1)
				{
					//no more birds, go to finished
					Vector3 posicaoInicial = new Vector3(0, 0, -20);
					camera.transform.positionTo(1f, posicaoInicial);

					camera.GetComponent<CameraMove>().SendMessage("SetZoom", true);
				
					endGame("Errado");
				}

				if (gameended == true) {
					StartCoroutine (timeCount(3));

					if (won == true) {						
						endGame("Certo");
					}
					else {
						
						endGame("Errado");
					}
				}

				else {
					print("Continua...");
					slingshot.slingshotState = SlingshotState.Idle;
					//panda to throw is the next on the list
					currentPandaIndex++;
					AnimatePandaToSlingshot();
				}
	        });
	}

    /// Animates the panda from the waiting position to the slingshot
	void AnimatePandaToSlingshot()
	{
		CurrentGameState = GameState.PandaMovingToSlingshot;

	
		Pandas[currentPandaIndex].transform.positionTo
			(Vector2.Distance(Pandas[currentPandaIndex].transform.position / 10,
			                  slingshot.PandaWaitPosition.transform.position) / 2, //duration
			 slingshot.PandaWaitPosition.transform.position). //final position
				setOnCompleteHandler((x) =>
				                     {
					x.complete();
					x.destroy(); //destroy the animation
					CurrentGameState = GameState.Playing;
					slingshot.enabled = true; //enable slingshot
					//current panda is the current in the list
					slingshot.PandaToThrow = Pandas[currentPandaIndex];
				});
	}
	
    private void Slingshot_PandaThrown(object sender, System.EventArgs e)
    {
        cameraFollow.PandaToFollow = Pandas[currentPandaIndex].transform;
        cameraFollow.IsFollowing = true;
    }
	
	bool PandasBamboosTargetsStoppedMoving()
    {

        foreach (var item in Bamboos.Union(Pandas).Union(Targets))
        {
			if(item!=null && item.rigidbody2D !=null)
			{
				if (item != null && item.rigidbody2D.velocity.sqrMagnitude > Constants.MinVelocity)
				{
					return false;
				}
			}          
        }
        return true;
    }
	
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }
			
	void AnswerHit(bool correct)
	{
		//Handheld.Vibrate ();
		gameended = true;
		if(correct == true)
		{
			if(endGame != null)
			{
				won = true;
			}
		}
		else
		{
			if(endGame != null)
			{
				won = false;
			}
		}
	}

	void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	
	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += RestartGame;
		ClickToUnlock.unlockScreen += UnlockClick;
		GameController.RestartGame += RestartGame;

		
	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGame;
		ClickToUnlock.unlockScreen -= UnlockClick;
		GameController.RestartGame -= RestartGame;
	}
}