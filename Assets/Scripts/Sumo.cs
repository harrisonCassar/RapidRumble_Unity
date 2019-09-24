using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo : Minigame
{
    public GameObject m_sumoBoardPrefab;
    public GameObject m_sumoItemPrefab;
    public GameObject m_sumoShrinkButtonPrefab;
    public const int NUM_ITEMS = 25;

    Vector3[] m_playerStartPos = new[] { new Vector3(-1.3351f, 27.0710f, 35.8596f), new Vector3(1.10489f, 27.0710f, 35.8596f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };
    GameObject[] m_sumoItems = new GameObject[NUM_ITEMS];
    GameObject m_sumoShrinkButton;

    //Constructor
    public Sumo() : base("Sumo") { }

    //Public Methods
    public override void resetGame()
    {
        killAllLive();
        
        //activate all players, and tp back to starting positions
        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
            m_gameManager.playerMoveToPos(i, m_playerStartPos[i]);

        m_gameManager.playerSetActiveAll(true);
        m_gameManager.playerAllowMovementAll(false);
        m_numAlivePlayers = m_gameManager.NUM_PLAYERS;

        //create sumo ring
        GameObject parent = GameObject.Find("Environment");

        m_playfield = Instantiate(m_sumoBoardPrefab, m_playfieldStartPos, Quaternion.identity, parent.transform);
        m_playfield.transform.localScale = new Vector3(25,1,25);

        //create sumo's Shrink Button
        m_sumoShrinkButton = Instantiate(m_sumoShrinkButtonPrefab, new Vector3(-0.119f, 26.509f, 35.873f), Quaternion.identity, parent.transform);

        //find boundaries of sumo playfield
        Renderer board = m_playfield.GetComponent<Renderer>();
        float minX = board.bounds.min.x;
        float maxX = board.bounds.max.x;
        float minZ = board.bounds.min.z;
        float maxZ = board.bounds.max.z;

        //spawn items within boundaries of playfield
        parent = GameObject.Find("Items");

        for (int i = 0; i < NUM_ITEMS; i++)
            m_sumoItems[i] = Instantiate(m_sumoItemPrefab, new Vector3(Random.Range(minX, maxX), 26.77879f, Random.Range(minZ, maxZ)), Quaternion.identity, parent.transform);
    }

    public override void playGame()
    {
        //enable controls for players
        m_gameManager.playerAllowMovementAll(true);

        //enable game
        base.enabled = true;
    }

    protected override int isGameOver()
    {
        //return player number if game is over; otherwise, 0
        if (m_numAlivePlayers <= 1)
        {
            //find last player alive
            int tmp = m_gameManager.playerFindFirstActive();

            cleanUpGame();

            if (tmp > 0)
                return tmp + 1;
            
            //in case of error, default Player 1 win
            return 1;
        }

        return 0;
    }

    public override void killObject(Collider deadObject)
    {
        if (deadObject.tag == "Player")
        {
            --m_numAlivePlayers;
            deadObject.gameObject.SetActive(false);
            print("'Killed' player");
        }
    }

    protected override void cleanUpGame()
    {
        killAllLive(); //POTENTIALLY REMOVE THIS LAYER OF ABSTRACTION ALL TOGETHER
    }

    //Private Methods
    private void killAllLive()
    {
        m_gameManager.playerSetActiveAll(false);
        m_gameManager.playerSetScaleAll(new Vector3(1, 1, 1));
        m_numAlivePlayers = 0;

        for (int i = 0; i < NUM_ITEMS; i++)
        {
            if (m_sumoItems[i] != null)
                Destroy(m_sumoItems[i]);
        }

        if (m_sumoShrinkButton != null)
            Destroy(m_sumoShrinkButton);

        if (m_playfield != null)
            Destroy(m_playfield);
    }
}
