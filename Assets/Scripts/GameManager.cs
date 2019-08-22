using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * Make platfrom gradually decrease
 * Add button to start decrease of platform
 * Have pickups fall to gravity
 * Add reset mechanic/detect when ALL players are dead
 * Add punch attack (WITH ANIMATION/PARTICLES)
 */

public class GameManager : MonoBehaviour
{
    private Vector3[] playerStartPos = new[] { new Vector3(-1.96f, 46.639f, -86.47f), new Vector3(0.48f, 46.639f, -86.47f), new Vector3(-25.45f, 35.35f, -71.81f), new Vector3(-1.96f, 46.639f, -86.47f) };

    public GameObject playerPrefab;
    public GameObject itemPrefab;
    public int numberOfItems = 10;
    public int numberOfDesiredPlayers = 2;

    private GameObject[] players = new GameObject[4];
    private int numberOfAlivePlayers = 0;

    private GameObject[] items = new GameObject[10];
    private int numberOfAliveItems = 0;

    // Start is called before the first frame update
    void Start()
    {
        resetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfAlivePlayers == 0)
        {
            resetGame();
        }
       //if all players dead, resetGame();
       //if r is pressed, resetGame();
    }
    
    public void setToDestroy(Collider deadObject)
    {
        if (deadObject.tag == "Item")
        {
            
        }
        else if (deadObject.tag == "Player")
        {
            Destroy(players[--numberOfAlivePlayers]);
        }
    }

    private void resetGame()
    {
        destroyAllLive();

        for (int i = 0; i < numberOfDesiredPlayers; i++)
        {
            players[i] = Instantiate(playerPrefab, playerStartPos[i], Quaternion.identity);
            numberOfAlivePlayers++;
        }
    }

    private void destroyAllLive()
    {
        for (int i = 0; i < numberOfAlivePlayers; i++)
            Destroy(players[i]);

        numberOfAlivePlayers = 0;

        for (int i = 0; i < numberOfAliveItems; i++)
            Destroy(items[i]);

        numberOfAlivePlayers = 0;
    }
}
