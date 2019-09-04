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

    //Data Members
    public string[] xAxisNames = new[] { "x_player1", "x_player2", "x_player3", "x_player4" };
    public string[] zAxisNames = new[] { "z_player1", "z_player2", "z_player3", "z_player4" };
    public int NUM_ROUNDS = 10;
    public int NUM_PLAYERS = 2;
    public Minigame[] m_minigames;

    public GameObject m_playerPrefab;
    private GameObject[] m_players = new GameObject[4];

    private int minigameInPlay = 0;
    private const int NUM_MINIGAMES = 1;

    //Methods
    public void declareRoundWinner(int winner)
    {
        print("ROUND WINNER: PLAYER ");
        print(winner);

        //before calling to main, make sure no players have won the match already

        Main();
    }
    public void setToDestroy(Collider deadObject)
    {
        m_minigames[minigameInPlay].killObject(deadObject);
    }

    //Player Methods
    public void playerMoveToPos(int pindex, Vector3 pos)
    {
        m_players[pindex].transform.position = pos;
    }
    public void playerSetActive(int pindex, bool isActive)
    {
        m_players[pindex].SetActive(isActive);
    }
    public void playerSetActiveAll(bool isActive)
    {
        for (int i = 0; i < NUM_PLAYERS; i++)
            m_players[i].SetActive(isActive);
    }
    public void playerAllowMovement(int pindex, bool allow)
    {
        m_players[pindex].GetComponent<PlayerMovement>().enabled = allow;
    }
    public void playerAllowMovementAll(bool allow)
    {
        for (int i = 0; i < NUM_PLAYERS; i++)
            m_players[i].GetComponent<PlayerMovement>().enabled = allow;
    }
    public int playerFindFirstActive()
    {
        for (int i = 0; i < NUM_PLAYERS; i++)
        {
            if (m_players[i].activeSelf == true)
                return i;
        }

        return 0;
    }
    public void playerSetScale(int pindex, Vector3 newScale)
    {
        m_players[pindex].transform.localScale = newScale;
    }

    public void playerSetScaleAll(Vector3 newScale)
    {
        for (int i = 0; i < NUM_PLAYERS; i++)
            m_players[i].transform.localScale = newScale;
    }
    
    //Game State Methods
    private void Start()
    {
        //disable all minigame's scripts, causing them to STOP running Update()
        for (int i = 0; i < NUM_MINIGAMES; i++)
        {
            m_minigames[i].enabled = false;
        }

        //create all players
        GameObject parent = GameObject.Find("Players");

        print("Creating Players");
        for (int i = 0; i < NUM_PLAYERS; i++)
        {
            m_players[i] = Instantiate(m_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);

            m_players[i].GetComponent<PlayerMovement>().x_axis_name = xAxisNames[i];
            m_players[i].GetComponent<PlayerMovement>().z_axis_name = zAxisNames[i];
            m_players[i].GetComponent<PlayerMovement>().enabled = false;
            m_players[i].GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
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
        if (Input.GetKeyDown(KeyCode.R)) //this should restart the match at the final implementation
        {
            print("Restarting game...");
            matchRestart(NUM_PLAYERS);
            m_minigames[minigameInPlay].resetGame();
        }
            
    }

    private void matchEnd() { } //show game winner, ask for reset with num players?
    private void matchRestart(int numOfPlayers) { }
}
