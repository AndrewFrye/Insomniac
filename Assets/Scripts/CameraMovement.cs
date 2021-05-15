using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform Camera;
    public Transform Player;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public static bool zoom;
    public Vector3 prePos;

    // Update is called once per frame
    void Update()
    {
        if (!zoom)
        {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    if (Camera.gameObject.GetComponent<Camera>().orthographicSize != 5) Camera.gameObject.GetComponent<Camera>().orthographicSize = 5;
                    break;
                case 1:
                    if (Camera.gameObject.GetComponent<Camera>().orthographicSize != 10) Camera.gameObject.GetComponent<Camera>().orthographicSize = 10;
                    break;
            }
            
            if (prePos != new Vector3(0, 0, 0))
            {
                Camera.position = prePos;
            }

            if (Player.position.x > Camera.position.x + 5 && Camera.position.x < maxX) Camera.Translate(new Vector3(Player.position.x - (Camera.position.x + 5), 0, 0));
            else if (Player.position.x < Camera.position.x - 5 && Camera.position.x > minX) Camera.Translate(new Vector3(-((Camera.position.x - 5) - Player.position.x), 0, 0));

            if (Player.position.y > Camera.position.y + 1 && Camera.position.y < maxY) Camera.Translate(new Vector3(0, Player.position.y - (Camera.position.y + 1), 0));
            else if (Player.position.y < Camera.position.y - 1 && Camera.position.y > minY) Camera.Translate(new Vector3(0, Player.position.y - (Camera.position.y - 1), 0));

            prePos = Camera.position;


            //Set camera position if out of bounds
            if(Camera.position.y < minY) Camera.position = new Vector3(Camera.position.x, minY, Camera.position.z);
            if(Camera.position.y > maxY) Camera.position = new Vector3(Camera.position.x, maxY, Camera.position.z);
            if(Camera.position.x < minX) Camera.position = new Vector3(maxY, Camera.position.y, Camera.position.z);
            if(Camera.position.x > maxX) Camera.position = new Vector3(maxX, Camera.position.y, Camera.position.z);

        }
        else if(zoom)
        {
            
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    Camera.position = new Vector3(-9.3f, 23, -10);
                    Camera.gameObject.GetComponent<Camera>().orthographicSize = 28.8f;
                    break;
                default:
                    break;
            }
        }
    }
}
