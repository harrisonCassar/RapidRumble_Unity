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
    public virtual void killObject(Collider deadObject) { }

    protected virtual int isGameOver() { return 0; }

    void Update()
    {
        int pnum = isGameOver();
        if (pnum != 0)
        {
            this.enabled = false;
            m_gameManager.declareRoundWinner(pnum);
        }
    }

    //Data Members
    //public GameObject m_playerPrefab;
    //public GameObject m_itemPrefab;
    public GameManager m_gameManager;

    private string m_name;

    //protected GameObject[] m_players = new GameObject[4];
    protected int m_numAlivePlayers = 0;

    protected GameObject m_playfield;
    protected Vector3 m_playfieldStartPos = new Vector3(0, 26, 37);
}
