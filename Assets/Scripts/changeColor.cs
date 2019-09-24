using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    private float playerXPos;
    private float playerZPos;

    private int pixelX;
    private int pixelY;

    public ColorChanger colorchanger;

    // Update is called once per frame
    void Update()
    {
        playerXPos = gameObject.transform.position.x;
        playerZPos = gameObject.transform.position.z;

        pixelX = map(colorchanger.boardMinX, colorchanger.boardMaxX, 0, colorchanger.IMG_X_MAX, playerXPos);
        pixelY = map(colorchanger.boardMinZ, colorchanger.boardMaxZ, 0, colorchanger.IMG_Y_MAX, playerZPos);
        print("Pixels: X: " + pixelX + " Y: " + pixelY);

        Color px = colorchanger.colorchart.GetPixel(pixelX, pixelY);

        gameObject.GetComponent<Renderer>().material.color = new Color(px.r, px.g, px.b);
    }

    private int map(float firstMin, float firstMax, float secondMin, float secondMax, float value)
    {
        return (int) (secondMin + (value - firstMin) * (secondMax - secondMin) / (firstMax - firstMin));
    }
}
