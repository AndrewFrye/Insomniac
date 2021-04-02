using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.SceneManagement;

public class BasicMovement : MonoBehaviour
{
    public ParticleSystem RocketBootsParticles;
    public float floatHeight;
    public PlayerMovement controls;
    private Rigidbody2D rb2d;
    private Transform player;
    public float speed;
    public float jumpSpeed;
    public int power;
    Vector2 move;
    Vector2 m;
    bool jumping = false;
    public bool grounded;
    public bool groundTest;
    public GameObject bulletPrefab;
    //bool firing = false;
    Vector2 mousePos;
    public Camera cam;
    //float fireTimer = 0;
    public bool debug;
    public bool ZeroG;
    float timer = 0;
    string currentTime;

    void Awake()
    {
        controls = new PlayerMovement();
        player = GetComponent<Transform>();
        rb2d = GetComponent<Rigidbody2D>();

        if (!CameraMovement.zoom)
        {
            controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => move = Vector2.zero;

            controls.Player.Jump.performed += ctx => jump();

            controls.Player.Interact.performed += ctx => interact();

            controls.Player.Fire.performed += ctx => shoot();
            controls.Player.DebugTeleport.performed += ctx => teleport();
            controls.Player.OpenMenuExitUI.performed += ctx => escapePressed();
            controls.Player.Zoom.performed += ctx => zoom(); 
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("BossRoom")) ZeroG = true;

        else ZeroG = false;
        if (ZeroG) rb2d.gravityScale = 0f;
        else rb2d.gravityScale = 1f;
    }

    private void jump()
    {
        if (!ZeroG)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.position.x, player.position.y - 1.6f), -Vector2.up);
            if (hit.collider != null)
            {
                float distance = transform.position.y - hit.point.y;
                Debug.Log(distance);
                if (distance < 1.7f) rb2d.AddForce(new Vector2(move.x * speed, jumpSpeed)); ;
            }
        }
    }

    private void Update()
    {
        //Depracated system to be removed
        /*Vector3 pos = new Vector3(Pointer.current.position.x.ReadValue(), Pointer.current.position.y.ReadValue(), 0);
        mousePos = cam.ScreenToWorldPoint(pos);
        if (!ZeroG)
        {
            m = new Vector2(move.x * speed, 0) * Time.deltaTime;
            if (jumping)
            {
                if (grounded)
                {

                    grounded = false;
                }
                power--;
            }
        }
        else if(ZeroG)
        {
            m = new Vector2(move.x * speed, move.y * speed) * Time.deltaTime;
        }
        transform.Translate(m, Space.World);*/

        rb2d.AddForce(new Vector3(move.x * speed * time.DeltaTime - rb2d.velocity.x, 0, 0));

        if (rb2d.velocity.y > 0) RocketBootsParticles.Play();
        else RocketBootsParticles.Stop();

        //Debug Timer
        timer += Time.deltaTime;
        currentTime = (int)timer / 60 + ":" + (int)timer % 60;
        Debug.Log(currentTime);
    }

    public static GameObject currentInteractable;
    private void interact()
    {
        switch (currentInteractable.tag)
        {
            case "Finish":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
                break;
        }
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void shoot()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y);
        Vector2 dir = mousePos - playerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        if(mousePos.x > playerPos.x) Instantiate(bulletPrefab, new Vector3(player.position.x+0.7f, player.position.y), rotation);
        else if (mousePos.x < playerPos.x) Instantiate(bulletPrefab, new Vector3(player.position.x - 0.7f, player.position.y), rotation);
        else if (mousePos.x == playerPos.x) Instantiate(bulletPrefab, new Vector3(player.position.x, player.position.y + 1f), rotation);
    }

    public void teleport()
    {
        if (debug) player.SetPositionAndRotation(new Vector3(mousePos.x, mousePos.y, player.position.x), player.rotation);
    }

    private void escapePressed()
    {
        Application.Quit();
    }

    private void zoom()
    {
        CameraMovement.zoom = !CameraMovement.zoom;

        //Lock player
        if (CameraMovement.zoom)
        {
            rb2d.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rb2d.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
