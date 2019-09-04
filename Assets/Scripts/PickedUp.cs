using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.localScale = other.transform.localScale * 1.1f;
            other.attachedRigidbody.mass = other.attachedRigidbody.mass * 1.1f;
            Destroy(gameObject);
        }
    }
}
