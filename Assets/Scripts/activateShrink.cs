using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateShrink : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject platform = GameObject.FindWithTag("SumoBoard");
            if (platform == null)
                Debug.Log("Cannot find sumo board");
            else
                platform.GetComponent<ShrinkingPlatform>().startShrink = true;

            Destroy(gameObject);
        }
    }
}
