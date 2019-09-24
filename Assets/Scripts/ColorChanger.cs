using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : Feature
{
    //Constructor
    public ColorChanger() : base("ColorChanger") { }

    //map x and y coordinates in world to pixels in colorchart image
    //getpixel at that coordinate, take value and set color of player to that

    Vector3[] m_playerStartPos = new[] { new Vector3(-1.3351f, 27.0710f, 35.8596f), new Vector3(1.10489f, 27.0710f, 35.8596f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };

    public GameObject m_colorfieldPrefab;
    public Texture2D colorchart;
    private float secondsElapsed = 0;

    public float IMG_X_MAX = 1800;
    public float IMG_Y_MAX = 1800;

    public float boardMinX = -1;
    public float boardMaxX = -1;
    public float boardMinZ = -1;
    public float boardMaxZ = -1;

    public override void runFeature()
    {
        //spawn platform
        GameObject parent = GameObject.Find("Environment");

        m_playfield = Instantiate(m_colorfieldPrefab, m_playfieldStartPos, Quaternion.identity, parent.transform);
        m_playfield.transform.RotateAround(m_playfield.transform.position, m_playfield.transform.up, 180f);

        //update bounds of board
        Renderer board = m_playfield.GetComponent<Renderer>();
        boardMinX = board.bounds.min.x;
        boardMaxX = board.bounds.max.x;
        boardMinZ = board.bounds.min.z;
        boardMaxZ = board.bounds.max.z;

        //add color-changing script to players
        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
        {
            m_gameManager.m_players[i].AddComponent<changeColor>();
            m_gameManager.m_players[i].GetComponent<changeColor>().colorchanger = this;
        }
        
        //activate all players, and tp back to starting positions
        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
            m_gameManager.playerMoveToPos(i, m_playerStartPos[i]);

        m_gameManager.playerSetActiveAll(true);
        m_gameManager.playerAllowMovementAll(true);
        m_numAlivePlayers = m_gameManager.NUM_PLAYERS;
    }

    protected override bool isFeatureOver()
    {
        secondsElapsed += Time.deltaTime;

        print(secondsElapsed);

        /*if (secondsElapsed >= 10)
        {
            cleanUpFeature();
            return true;
        }*/
        
        return false;
    }

    protected override void cleanUpFeature()
    {
        m_gameManager.playerSetActiveAll(false);
        m_gameManager.playerAllowMovementAll(false);
        m_gameManager.playerSetScaleAll(new Vector3(1, 1, 1));
        m_numAlivePlayers = 0;

        if (m_playfield != null)
            Destroy(m_playfield);

        for (int i = 0; i < m_gameManager.NUM_PLAYERS; i++)
        {
            Component tmp = m_gameManager.m_players[i].GetComponent<changeColor>();
            if (tmp != null)
                Destroy(tmp);
        }
    }
}
