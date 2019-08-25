using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sumo : Minigame
{
    public GameObject m_sumoBoardPrefab;

    Vector3[] m_playerStartPos = new[] { new Vector3(-1.3351f, 27.0710f, 35.8596f), new Vector3(1.10489f, 27.0710f, 35.8596f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };
    
    public Sumo() : base("Sumo") { }

    public override void resetGame()
    {
        killAllLive(); //might not be needed so long as each minigame properly deletes all characters before gameEnd

        GameObject parent = GameObject.Find("Players");

        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
        {
            m_players[i] = Instantiate(m_playerPrefab, m_playerStartPos[i], Quaternion.identity, parent.transform);

            m_players[i].GetComponent<PlayerMovement>().x_axis_name = m_gameManager.xAxisNames[i];
            m_players[i].GetComponent<PlayerMovement>().z_axis_name = m_gameManager.zAxisNames[i];
            m_players[i].GetComponent<PlayerMovement>().enabled = false;
            m_players[i].GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            m_numAlivePlayers++;
        }

        parent = GameObject.Find("Environment");

        m_playfield = Instantiate(m_sumoBoardPrefab, m_playfieldStartPos, Quaternion.identity, parent.transform);

        m_playfield.transform.localScale = new Vector3(25,1,25);
    }

    public override void playGame()
    {
        //enable controls for players
        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
            m_players[i].GetComponent<PlayerMovement>().enabled = true;

        this.enabled = true;
    }

    public override int isGameOver()
    {
        if (m_numAlivePlayers <= 1)
        {
            for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
            {
                if (m_players[i].activeSelf)
                    return i + 1;
            }

            //in case of error, return default Player 1 win
            return 1;
        }

        return 0;
    }

    public override void killObject(Collider deadObject)
    {
        if (deadObject.tag == "Item")
        {

        }
        else if (deadObject.tag == "Player")
        {
            --m_numAlivePlayers;
            deadObject.gameObject.SetActive(false);
        }
    }

    //Private Methods
    private void Update()
    {
        int tmp = isGameOver();
        if (tmp != 0)
        {
            this.enabled = false;
            m_gameManager.declareRoundWinner(tmp);
        }
    }

    private void killAllLive()
    {
        for (int i = 0; i < m_numAlivePlayers; i++)
        {
            m_players[i].SetActive(false);
        }

        m_numAlivePlayers = 0;
    }
}
