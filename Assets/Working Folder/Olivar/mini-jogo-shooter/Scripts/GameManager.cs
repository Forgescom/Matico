using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public GameObject introScreen;
//	public GameObject explanationScreen;
	public GameObject question;
	public GameObject shooter;
	public GameObject target;

	public GameObject camera;

	int currentScreen = 1;

	// Iniciar Jogo
	public TextMesh startText;
	bool start = false;

    public CameraFollow cameraFollow;
    int currentPandaIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Pandas;
	private List<GameObject> Bamboos;
    private List<GameObject> Targets;

	//EVENTS
	public delegate void StartGameDelegate();
	public static event StartGameDelegate startGame;
	public delegate void EndGameDelegate(string outcome);
	public static event EndGameDelegate endGame;

	public delegate void RestartGameDelegate();
	public static event RestartGameDelegate restartGame;

	//EVENTS FOR GAME END
	public delegate void gameEnd(string test);
	public static event gameEnd GameEnded;
	public delegate void GameNewTry();
	public static event GameNewTry GameTryAgain;

	private bool won;
	public bool gameended;
	private bool nolives;

    // Use this for initialization
    void Start()
    {
		if (GameController.SHOOTER_RESTARTING == false) {
			introScreen.SetActive (true);
//			explanationScreen.SetActive(false);
			shooter.SetActive(false);
			slingshot.enabled = false;
			GameController.SHOOTER_RESTARTING = true;
		}
		else {
			introScreen.SetActive (false);
			shooter.SetActive(true);
			slingshot.enabled = true;
		}

    }



	void Init() 
	{
		CurrentGameState = GameState.Start;

//		question.gameObject.SetActive(true);
		//find all relevant game objects
		Pandas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Panda"));
		Bamboos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bamboo"));
		Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
		print (Targets.Count);
		won = false;
		nolives = false;
		gameended = false;
		//unsubscribe and resubscribe from the event
		//this ensures that we subscribe only once
		slingshot.PandaThrown -= Slingshot_PandaThrown; 
		slingshot.PandaThrown += Slingshot_PandaThrown;
		AnimatePandaToSlingshot();

		if (startGame != null)
		{
			startGame();
			
		}
	}

	// Update is called once per frame
	void Update()
    {
		if (start == false) {
			Init();
			AutoResize(1920, 1080);
			startText.text = "Toca no ecra para o jogo iniciar";
			question.gameObject.SetActive(true);
			if(Input.touchCount > 0)
			{
				slingshot.enabled = true;
				startText.transform.position = new Vector3(100, 0, 0);
				start = true;
			}
		}

		switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the pandas 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                    AnimatePandaToSlingshot();
                }
                break;
            case GameState.PandaMovingToSlingshot:
                //do nothing
                break;
            case GameState.Playing:
                //if we have thrown a panda
                //and either everything has stopped moving
                //or there has been 5 seconds since we threw the panda
                //animate the camera to the start position
              

			if (slingshot.slingshotState == SlingshotState.PandaFlying && (PandasBamboosTargetsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
			{
			    slingshot.enabled = false;
			    AnimateCameraToStartPosition();
			    CurrentGameState = GameState.PandaMovingToSlingshot;
			}

            break;
            //if we have won or lost, we will restart the level
            //in a normal game, we would show the "Won" screen 
            //and on tap the user would go to the next level
//            case GameState.Won:
/*            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
*/            default:
                break;
        }

    }

	void ChangeScreen()
	{
		if (currentScreen == 0) {
//			explanationScreen.SetActive(true);
//			explanationScreen.animation.Play("shooterExplanation");
			currentScreen ++;
		}
		else if(currentScreen == 1)
		{
			shooter.SetActive(true);
			currentScreen ++;
			/*if(startGame !=null)
			{
				startGame();
			}*/
		}
	}

    /// Animates the camera to the original location
	/// When it finishes, it checks if we have lost, won or we have other pandas available to throw
    private void AnimateCameraToStartPosition()
    {
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
					camera.GetComponent<CameraMove>().SendMessage("SetZoom", true);
//					GManager.SendMessage("AnswerHit", true);
					nolives = true;
					endGame("Errado");
				}

				if (gameended == true) {
					camera.GetComponent<CameraMove>().SendMessage("SetZoom", true);
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
		Handheld.Vibrate ();
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

/*	
	IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (1.5F);
		if (GameTryAgain != null) {		
			GameTryAgain();
		}	
		transform.SendMessage ("SetQuestionValues");
	}
*/	
	void OnEnable()
	{
		DeactivateOnAnimEnd.animationFinish += ChangeScreen;
		FailureScreen.RestartGame += RestartGame;
		ClickToUnlock.unlockScreen += Init;
		GameController.RestartGame += RestartGame;
		
	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGame;
		ClickToUnlock.unlockScreen -= Init;
		GameController.RestartGame -= RestartGame;
	}
	
}
