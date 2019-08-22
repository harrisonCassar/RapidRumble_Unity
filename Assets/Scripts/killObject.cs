using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killObject : MonoBehaviour
{
    public GameManager m_GameManager;

    private void OnTriggerEnter(Collider other)
    {
        m_GameManager.setToDestroy(other);
    }
}
