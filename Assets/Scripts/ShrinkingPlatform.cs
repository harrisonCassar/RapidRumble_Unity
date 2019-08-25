using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    public float timer = 10f;
    private float secondsElapsed = 0;
    private Vector3 finalScale;
    private Vector3 newScale;

    void Start()
    {
        finalScale = gameObject.transform.localScale * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;
        if (secondsElapsed > timer)
        {
            newScale = new Vector3(gameObject.transform.localScale.x * 0.95f, gameObject.transform.localScale.y, gameObject.transform.localScale.z * 0.95f);
            gameObject.transform.localScale = newScale;
            secondsElapsed = 0;
            timer = 0.5f;
            if (newScale.x < finalScale.x && newScale.z < finalScale.z)
            {
                Destroy(gameObject);
                return;
            }
        }
    }
}
