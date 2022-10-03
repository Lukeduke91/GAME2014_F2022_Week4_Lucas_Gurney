using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed = 2.0f;
    public Boundary boundary;
    public float verticalPosition;
    public bool usingMobileInput = false;
    public ScoreManager scoreManager;

    private Camera camera;
    // Start is called before the first frame update

    // Update is called once per frame
     void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        transform.position = new Vector2(0.0f, verticalPosition);
        camera = Camera.main;
        usingMobileInput = Application.platform == RuntimePlatform.Android || 
                           Application.platform == RuntimePlatform.IPhonePlayer;
    }

    void Update()
    {
        if(usingMobileInput)
        {
            GetMobileInput();
        }
        else 
        { 
            GetConventionalInput();
        }
        Move();
        if(Input.GetKeyUp(KeyCode.K))
        {
            scoreManager.AddPoints(10);
        }
        //GetMobileInput();
        //GetConventionalInput();
        //float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        //float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        //transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
    }

    void GetConventionalInput()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        transform.position += new Vector3(x, 0, 0);
    }

    public void Move()
    {
        //float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        //transform.position += new Vector3(x, 0, 0);

        float clampedPosition = Mathf.Clamp(transform.position.x, boundary.min, boundary.max);
        transform.position = new Vector2(clampedPosition, verticalPosition);
    }

    void GetMobileInput()
    {
        //Touch currentTouch = new Touch();

        foreach (Touch touch in Input.touches) 
        { 
            var destination = camera.ScreenToWorldPoint(touch.position);
            //currentTouch = touch;
            transform.position = Vector2.Lerp(transform.position, destination, Time.deltaTime * speed);
        }
        //var touch = Input.GetTouch(0);
    }


    //void CheckBounds()
    //{
    //    if (transform.position.x > boundary.max)
    //    {
    //        transform.position = new Vector2(boundary.max, verticalPosition);
    //    }
    //    if (transform.position.x < boundary.max)
    //    {
    //        transform.position = new Vector2(boundary.max, verticalPosition);
    //    }
    //    //Mathf.Clamp();
    //}
}
