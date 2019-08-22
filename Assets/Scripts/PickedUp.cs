using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUp : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Vector3 tmp = other.transform.localScale;
        other.transform.localScale = other.transform.localScale * 1.1f;
        other.attachedRigidbody.mass = other.attachedRigidbody.mass * 1.1f;
        Destroy(gameObject);
    }
}
