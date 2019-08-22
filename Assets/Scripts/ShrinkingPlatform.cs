using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    public float timer = 10f;
    private float secondsElapsed = 0;
    private int counter = 0;
    private Vector3 finalScale;
    private Vector3 tmpScale;
    private void Start()
    {
        finalScale = gameObject.transform.localScale * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed > timer)
        {
            tmpScale = gameObject.transform.localScale;
            gameObject.transform.localScale = gameObject.transform.localScale * 0.95f;
            secondsElapsed = 0;
            timer = 0.5f;
            if (tmpScale.x < finalScale.x && tmpScale.y < finalScale.y && tmpScale.z < finalScale.z)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
