using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feature : MonoBehaviour
{
    //Constructor
    public Feature(string name)
    {
        m_name = name;
    }

    //Methods
    public virtual void runFeature() { }

    protected virtual bool isFeatureOver() { return false; }
    protected virtual void cleanUpFeature () { }

    void Update()
    {
        if (isFeatureOver())
        {
            this.enabled = false;
            m_gameManager.playerSetActiveAll(false);
            m_gameManager.playerAllowMovementAll(false);
            m_gameManager.declareFeatureFinish();
        }
    }

    //Data Members
    public GameManager m_gameManager;

    private string m_name;

    protected int m_numAlivePlayers = 0;

    protected GameObject m_playfield;
    protected Vector3 m_playfieldStartPos = new Vector3(0, 26, 37);
}
