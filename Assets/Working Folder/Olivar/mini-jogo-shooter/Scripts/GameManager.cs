using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    int currentPandaIndex;
    public Cannon cannon;
//    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Pandas;
	private List<GameObject> Bamboos;
    private List<GameObject> Targets;

    // Use this for initialization
    void Start()
    {
        CurrentGameState = GameState.Start;
        cannon.enabled = false;
        //find all relevant game objects
        Pandas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Panda"));
        Bamboos = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bamboo"));
        Targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Target"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        cannon.PandaThrown -= Cannon_PandaThrown; cannon.PandaThrown += Cannon_PandaThrown;
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the pandas 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                    AnimatePandaToCannon();
                }
                break;
            case GameState.PandaMovingToCannon:
                //do nothing
                break;
            case GameState.Playing:
                //if we have thrown a panda
                //and either everything has stopped moving
                //or there has been 5 seconds since we threw the panda
                //animate the camera to the start position
              
/*
			if (cannon.CannonState == cannon.CannonState.PandaFlying && (PandasBamboosTargetsStoppedMoving() || Time.time - cannon.TimeSinceThrown > 5f))
			{
			    cannon.enabled = false;
			    AnimateCameraToStartPosition();
			    CurrentGameState = GameState.PandaMovingToCannon;
			}
*/
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
                                CurrentGameState = GameState.Won;
                            }
                            //animate the next panda, if available
                            else if (currentPandaIndex == Pandas.Count - 1)
                            {
                                //no more pandas, go to finished
                                CurrentGameState = GameState.Lost;
                            }
                            else
                            {
                                cannon.CannonState = CannonState.Idle;
                                //panda to throw is the next on the list
                                currentPandaIndex++;
                                AnimatePandaToCannon();
                            }
                        });
    }

    /// <summary>
    /// Animates the panda from the waiting position to the slingshot
    /// </summary>
    void AnimatePandaToCannon()
    {
        CurrentGameState = GameState.PandaMovingToCannon;
     
		Pandas[currentPandaIndex].transform.positionTo
            (Vector2.Distance(Pandas[currentPandaIndex].transform.position / 10,
            cannon.PandaWaitPosition.transform.position) / 10, //duration
			 cannon.PandaWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy(); //destroy the animation
                            CurrentGameState = GameState.Playing;
                            cannon.enabled = true; //enable slingshot
                            //current panda is the current in the list
							cannon.PandaToThrow = Pandas[currentPandaIndex];
                        });


    }

    /// <summary>
    /// Event handler, when the panda is thrown, camera starts following it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cannon_PandaThrown(object sender, System.EventArgs e)
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
            if (item != null && item.rigidbody2D.velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
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

    /// <summary>
    /// Shows relevant GUI depending on the current game state
    /// </summary>
    void OnGUI()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start");
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "You won! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }


}
