using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    //Constructor
    public Minigame(string name)
    {
        m_name = name;
    }

    //Methods
    public virtual void resetGame() { }
    public virtual void playGame() { }
    public virtual int isGameOver() { return 0; }
    public virtual void killObject(Collider deadObject) { }
    
    void Update()
    {
        int pnum = isGameOver();
        if (pnum != 0)
            m_gameManager.declareRoundWinner(pnum);
    }

    //Data Members
    public GameObject m_playerPrefab;
    //public GameObject m_itemPrefab;
    public GameManager m_gameManager;

    //private Vector3[] m_playerStartPos = new[] { new Vector3(-1.96f, 46.639f, -86.47f), new Vector3(0.48f, 46.639f, -86.47f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };
    private string m_name;

    protected GameObject[] m_players = new GameObject[4];
    protected int m_numAlivePlayers = 0;

    protected GameObject m_playfield;
    protected Vector3 m_playfieldStartPos = new Vector3(0, 26, 37);

    //private GameObject[] items = new GameObject[10];
    //private int numberOfAliveItems = 0;

    //Data Structures
    /*protected struct Player
    {
        public bool isAlive;
        public GameObject player;
    }*/

    //Accessor Methods
    /*protected int getNumAlivePlayers()
    {
        return m_numAlivePlayers;
    }

    protected int incNumAlivePlayers()
    {
        return ++m_numAlivePlayers;
    }

    protected int decNumAlivePlayers()
    {
        return --m_numAlivePlayers;
    }*/
}
