using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * Make platfrom gradually decrease
 * Add button to start decrease of platform
 * Have pickups fall to gravity
 * Add reset mechanic/detect when ALL players are dead
 * Add punch attack (WITH ANIMATION/PARTICLES)
 */

public class GameManager : MonoBehaviour
{
    //Methods
    public void declareRoundWinner(int winner)
    {
        Main();
    }
    public void matchEnd() { } //show game winnder, ask for reset with num players?
    public void matchRestart(int numOfPlayers) { }

    // Start is called before the first frame update
    private void Start()
    {
        //Sumo sumo = gameObject.AddComponent<Sumo>() as Sumo;
        //m_minigames[0] = sumo;

        //disable all minigame's scripts, causing them to STOP running Update()
        for (int i = 0; i < NUM_MINIGAMES; i++)
        {
            m_minigames[i].enabled = false;
        }

        Main();
    }

    private void Main()
    {
        //randomly select games to play
        minigameInPlay = 0;

        m_minigames[minigameInPlay].resetGame();
        m_minigames[minigameInPlay].playGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            matchRestart(NUM_PLAYERS);
    }

    private void enableMinigame(int index)
    {
        m_minigames[index].enabled = true;
    }

    public void setToDestroy(Collider deadObject)
    {
        m_minigames[minigameInPlay].killObject(deadObject);
    }

    //Public Data Members
    public string[] xAxisNames = new[] { "x_player1", "x_player2", "x_player3", "x_player4" };
    public string[] zAxisNames = new[] { "z_player1", "z_player2", "z_player3", "z_player4" };
    public int NUM_ROUNDS = 10;
    public int NUM_PLAYERS = 2;
    public Minigame[] m_minigames;

    //Private Data Members
    private int minigameInPlay = 0;
    private const int NUM_MINIGAMES = 1;
    
}
