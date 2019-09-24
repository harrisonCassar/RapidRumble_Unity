using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingPlatform : MonoBehaviour
{
    private float timer = 2f;
    private float secondsElapsed = 0;
    private Vector3 finalScale;
    private Vector3 newScale;
    private Vector3 initialScale;
    public const int DECAY_SPEED = 10;

    public bool startShrink = false;

    void Start()
    {
        initialScale = gameObject.transform.localScale;
        finalScale = gameObject.transform.localScale * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        secondsElapsed += Time.deltaTime;

        if (startShrink && secondsElapsed < timer)
        {
            secondsElapsed = timer;
            startShrink = false;
        }

        if (secondsElapsed >= timer)
            gameObject.transform.localScale = initialScale * (Mathf.Exp(-(secondsElapsed-timer)/DECAY_SPEED));
    }
}
