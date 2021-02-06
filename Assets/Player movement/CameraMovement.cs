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
            //Debug.Log(Camera.gameObject.GetComponent<Camera>().orthographicSize);
            if (Camera.gameObject.GetComponent<Camera>().orthographicSize != 5) Camera.gameObject.GetComponent<Camera>().orthographicSize = 5;
            if (prePos != new Vector3(0, 0, 0))
            {
                Camera.position = prePos;
            }

            if (Player.position.x > Camera.position.x + 5 && Camera.position.x < maxX) Camera.Translate(new Vector3(Player.position.x - (Camera.position.x + 5), 0, 0));
            else if (Player.position.x < Camera.position.x - 5 && Camera.position.x > minX) Camera.Translate(new Vector3(-((Camera.position.x - 5) - Player.position.x), 0, 0));

            if (Player.position.y > Camera.position.y + 1 && Camera.position.y < maxY) Camera.Translate(new Vector3(0, Player.position.y - (Camera.position.y + 1), 0));
            else if (Player.position.y < Camera.position.y - 1 && Camera.position.y > minY) Camera.Translate(new Vector3(0, Player.position.y - (Camera.position.y - 1), 0));

            prePos = Camera.position;
        }
        else if(zoom)
        {
            
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 0:
                    Camera.position = new Vector3(24, 11, -10);
                    Camera.gameObject.GetComponent<Camera>().orthographicSize = 17;
                    break;
                default:
                    break;
            }
        }
    }
}
