using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string x_axis_name = "x_player#";
    public string z_axis_name = "z_player#";
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        float z_translation = Input.GetAxisRaw(z_axis_name) * speed;
        float x_translation = Input.GetAxisRaw(x_axis_name) * speed;

        x_translation *= Time.deltaTime;
        z_translation *= Time.deltaTime;

        transform.Translate(x_translation, 0 , z_translation);
    }
}
