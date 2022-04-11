using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The GameManager keeps track of global information for the game, such as the players and the location of the gaol.
// The GameManager is a Singleton (see Nystrom, chapter II.6), there is only single instance of it.

public class GameManager : MonoBehaviour
{
  // This is the single instance of the class
  private static GameManager instance = null;

  // Keep track of all the players
  private const int numGreenPlayers = 3;
  private List<GreenPlayer> greenPlayers = new List<GreenPlayer>(numGreenPlayers);

  private const int numPurplePlayers = 1;
  private List<PurplePlayer> purplePlayers = new List<PurplePlayer>(numPurplePlayers);

  public static int caught = 0; // number of caught green players

    [SerializeField]  private Text caughtText; // text the number of caught green players
    [SerializeField]  private Text endText; // text the end of the Game
    [SerializeField]  private Text timerText; // text for Timer

    float timer = 0.0f; 




  [SerializeField]
  GameObject greenPlayerPrefab;
  [SerializeField]
  GameObject purplePlayerPrefab;
  [SerializeField]
  GameObject gaol;
  
  // Start is called before the first frame update
  void Start()
  {
        
    // If there already is an official instance, this instance deletes itself.
    if (instance == null)
    { instance = this; }
    else
    {
      Destroy(this);
      return;
    }

    // Create all the players.
    for (int i = 0; i < numGreenPlayers; i++)
    {
      greenPlayers.Add(Instantiate(greenPlayerPrefab).GetComponent<GreenPlayer>());
    }

    for (int i = 0; i < numPurplePlayers; i++)
    {
      purplePlayers.Add(Instantiate(purplePlayerPrefab).GetComponent<PurplePlayer>());
         

    }

        caughtText.text = "Caught: " + caught;     // 0 players 
        timerText.text = "";                      // 10 seconds

  }

  // Update is called once per frame
  void Update()
  {
        caughtText.text = "Caught: " + caught; // Updating caught players count
        

       

        timer += Time.deltaTime;
        float seconds = timer % 60;

        timerText.text = "Timer: " + (10 - Mathf.Round(seconds)); // Updating the timer

        if (seconds > 10) // times up!
        {
            GreenWins();
        }

        if (caught == numGreenPlayers) // all green player have been caught!
        {
            PurpleWins();
        }

    }

    // Find the nearest green player to a given purple player
    public GreenPlayer FindClosestTarget(PurplePlayer player)
  {
    GreenPlayer target = null;
    float closestDistance = float.MaxValue;

    foreach (GreenPlayer greenPlayer in greenPlayers)
    {
      float distance = Vector2.Distance(greenPlayer.Position(), player.Position());
      if (distance < closestDistance)
      {
        closestDistance = distance;
        target = greenPlayer;
      }
    }
    return target;
  }

  // Find the nearest purple player to a given green player

  public PurplePlayer FindClosestTarget(GreenPlayer player)
  {
    PurplePlayer target = null;
    float closestDistance = float.MaxValue;

    foreach (PurplePlayer purplePlayer in purplePlayers)
    {
      float distance = Vector2.Distance(purplePlayer.Position(), player.Position());
      if (distance < closestDistance)
      {
        closestDistance = distance;
        target = purplePlayer;
      }
    }
    return target;
  }

  // Return the gaol object
  public GameObject GetGaol()
  {
    return gaol;
  }

  // Return the single instance of the class
  public static GameManager Instance()
  {
    return instance;
  }

  private void PurpleWins()
    {
        endText.text = "Purple Team Wins!";
        Time.timeScale = 0;

    }
  private void GreenWins()
    {
        endText.text = "Green Team Wins!";
        Time.timeScale = 0;

    }

}
