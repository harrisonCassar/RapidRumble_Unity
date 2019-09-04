using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo : Minigame
{
    public GameObject m_sumoBoardPrefab;

    Vector3[] m_playerStartPos = new[] { new Vector3(-1.3351f, 27.0710f, 35.8596f), new Vector3(1.10489f, 27.0710f, 35.8596f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };
    
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

    //Private Methods
    private void killAllLive()
    {
        m_gameManager.playerSetActiveAll(false);
        m_gameManager.playerSetScaleAll(new Vector3(1, 1, 1));
        m_numAlivePlayers = 0;

        Destroy(m_playfield);
    }
}
