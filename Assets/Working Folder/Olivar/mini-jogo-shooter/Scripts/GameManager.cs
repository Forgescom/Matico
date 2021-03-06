﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public GameObject introScreen;
	public GameObject explanationScreen;
	public GameObject shooter;
	public GameObject target;

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

    // Use this for initialization
    void Start()
    {
		introScreen.SetActive (true);
//		explanationScreen.SetActive(false);
		shooter.SetActive(false);
    }

	void Init() 
	{
		CurrentGameState = GameState.Start;
		slingshot.enabled = false;
		//find all relevant game objects
		Pandas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Panda"));
		Bamboos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bamboo"));
		Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
		print (Pandas.Count);
		print (Bamboos.Count);
		print (Targets.Count);
		//unsubscribe and resubscribe from the event
		//this ensures that we subscribe only once
		slingshot.PandaThrown -= Slingshot_PandaThrown; 
		slingshot.PandaThrown += Slingshot_PandaThrown;
		AnimatePandaToSlingshot();
	}
	
	
	// Update is called once per frame
	void Update()
    {
		if (start == false) {
			startText.text = "Toca no ecra para o jogo iniciar";
			if(Input.touchCount > 0)
			{
				Init();
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
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
            default:
                break;
        }
    }

	void ChangeScreen()
	{
		if (currentScreen == 0) {
//			explanationScreen.SetActive(true);
//			explanationScreen.animation.Play("boiaExplanation");
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


    /// <summary>
    /// A check whether all Pigs are null
    /// i.e. they have been destroyed
    /// </summary>
    /// <returns></returns>
    private bool AllTargetsDestroyed()
    {
        return Targets.All(x => x == null);
    }

    /// <summary>
    /// Animates the camera to the original location
    /// When it finishes, it checks if we have lost, won or we have other pandas
    /// available to throw
    /// </summary>
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
                            if (AllTargetsDestroyed())
                            {
								print("Continua1");
								slingshot.slingshotState = SlingshotState.Idle;
								//bird to throw is the next on the list
								currentPandaIndex++;
								AnimatePandaToSlingshot();

//                          	CurrentGameState = GameState.Won;
//								print("Ganhou");
                            }
                            //animate the next bird, if available
                            else if (currentPandaIndex == Pandas.Count - 1)
                            {
                                //no more birds, go to finished
                                CurrentGameState = GameState.Lost;
                            }
                            else
                            {
								print("Continua2");
                                slingshot.slingshotState = SlingshotState.Idle;
                                //bird to throw is the next on the list
                                currentPandaIndex++;
                                AnimatePandaToSlingshot();
                            }
                        });
    }

    /// <summary>
    /// Animates the panda from the waiting position to the slingshot
    /// </summary>
    void AnimatePandaToSlingshot()
    {
        CurrentGameState = GameState.PandaMovingToSlingshot;
		Pandas[currentPandaIndex].transform.positionTo
            (Vector2.Distance(Pandas[currentPandaIndex].transform.position / 10,
            slingshot.PandaWaitPosition.transform.position) / 10, //duration
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

    /// <summary>
    /// Event handler, when the panda is thrown, camera starts following it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Slingshot_PandaThrown(object sender, System.EventArgs e)
    {
        cameraFollow.PandaToFollow = Pandas[currentPandaIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    /// <summary>
    /// Check if all pandas, pigs and bricks have stopped moving
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Found here
    /// http://www.bensilvis.com/?p=500
    /// </summary>
    /// <param name="screenWidth"></param>
    /// <param name="screenHeight"></param>
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

	void AnswerHit(bool correct)
	{
		Handheld.Vibrate ();
		
		if(correct == true)
		{
			if(endGame != null)
			{
				endGame("Certo");
				print("Acertou!!!");
				
			}
		}
		else
		{
			if(endGame != null)
			{
				endGame("Errado");
				
			}
		}
	}

    /// <summary>
    /// Shows relevant GUI depending on the current game state
    /// </summary>
/*    void OnGUI()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Toca no ecra para iniciar");
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "Ganhaste! Toca no ecra para reiniciar");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "Perdeste! Toca no ecra para reiniciar");
                break;
            default:
                break;
        }
    }
*/
	void RestartGame()
	{
		//		accelerometer.animation.Play("");
		StartCoroutine ("ChangeQuestion");
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
		//		AccelerometerBox.finishEvent += AccelerometerFinish;
	}
	
	void OnDisable()
	{
		DeactivateOnAnimEnd.animationFinish -= ChangeScreen;
		FailureScreen.RestartGame -= RestartGame;
		//		AccelerometerBox.finishEvent -= AccelerometerFinish;
	}


}
