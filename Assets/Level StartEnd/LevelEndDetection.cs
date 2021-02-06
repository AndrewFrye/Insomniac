using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndDetection : MonoBehaviour
{
    public Transform door;
    public Transform player;
    private float doorx;
    private float playerx;
    private float playerAbsolute;
    private float doorAbsolute;
    private float playerDistance;
    public static bool levelComplete = false;
    public static bool playerInRangeOfExit = false;
    private Scene currentScene;
    void Start()
    {
        door = GetComponent<Transform>();
        doorx = door.position.x;
        if (doorx < 0) doorAbsolute = -doorx;
        else doorAbsolute = doorx;
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        //Check if in range of door
        playerx = player.position.x;
        if (playerx < 0) playerAbsolute = -playerx;
        else playerAbsolute = playerx;
        playerDistance = doorAbsolute - playerAbsolute;
        if (playerDistance < 0) playerDistance = -playerDistance;
        if (playerDistance <= 2) playerInRangeOfExit = true;
        else playerInRangeOfExit = false;

        //Reset level on player death
        string sceneName = currentScene.name;
        if (PlayerCollision.dead) SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        //Open loading scene to wait for next level to load
        if (levelComplete) SceneManager.LoadScene("Loading", LoadSceneMode.Single);
    }
}
