using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class ButtonCode : MonoBehaviour
{
     SpriteRenderer sprite;
    [SerializeField] Sprite splat;
    [SerializeField] GameObject button;

    Animator animations;

    [SerializeField] float speed;
    [SerializeField] Vector2 minPoint;
    [SerializeField] Vector2 maxPoint;

    public Vector2 randomPoint;
    public Vector2 currentPoint;
    private Vector2 directionToPoint;
    public bool mouseOver;
    public bool reachedLocation;
    private bool loading;
    private int index;
    private bool moving;
    private bool waiting;

    [SerializeField] AudioSource deathsound;
    [SerializeField] AudioSource crawlingsound;
    void Awake()
    {
        sprite = button.GetComponent<SpriteRenderer>();
        animations = button.GetComponent<Animator>();
        currentPoint = button.transform.position;
        randomPoint = currentPoint;
        GetRandomLocationInCamera();
    }
    void GetRandomLocationInCamera()
    {
        Camera mainCamera = Camera.main;

        // Get camera bounds
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        //sets up points within the scene for the button to move around while keeping in the camera bounds
        minPoint = mainCamera.ViewportToWorldPoint(new Vector3(.1f, .1f, 0));
        maxPoint = mainCamera.ViewportToWorldPoint(new Vector3(.9f, .9f, 0));
    }
    void Update()
    {
        //checks if the mouse is on the button and whether the button has reached the point marked
        if (currentPoint == randomPoint)
        {
            reachedLocation = true;
            moving = false;
            StopAnimation();
            crawlingsound.Stop();
            //resets the rotation once it reaches the location
            //button.transform.rotation = Quaternion.identity;
        }
        if (!loading && (mouseOver || !reachedLocation))
        {
            //if the current button location is not at the point marked move towards it
            if (currentPoint != randomPoint)
            {
                reachedLocation = false;
                //moves the button to the marked location
                StartCoroutine(MoveButton());
            }
            else
            {               
                //generates a random point for the button to move towards it
                reachedLocation = false;
                generaterandom();
            }
        }    
    }
    private void generaterandom()
    {
        //toggles reached location
        reachedLocation = true;
        //sets the location of the new button location
        currentPoint = button.transform.position;
        do
        {
            //gets a random point within the camera for the button to move towards
            randomPoint.x = Random.Range(minPoint.x, maxPoint.x);
            randomPoint.y = Random.Range(minPoint.y, maxPoint.y);
        } while (Vector2.Distance(randomPoint, currentPoint) >= 8f && Vector2.Distance(randomPoint,Input.mousePosition) >=8f);
    }
    private IEnumerator MoveButton()
    {
        if (!loading) {
            //creates a small delay so the user can click on the button
            if (!reachedLocation && !moving && !waiting)
            {
                waiting = true;
                yield return new WaitForSeconds(.4f);
                moving = true;
                crawlingsound.Play();
                PlayAnimation();
                waiting = false;
            }
            if (!waiting && moving && !reachedLocation)
            {
                Debug.Log("moving");
                //gets the random location direction from the button
                directionToPoint = randomPoint - currentPoint;
                //makes the button face the target location
                button.transform.rotation = Quaternion.LookRotation(Vector3.forward, -directionToPoint);
                //moves the button towards the target location
                button.transform.position = Vector2.MoveTowards(currentPoint, randomPoint, speed * Time.deltaTime);
                //sets the new location for where the button moved to
                currentPoint = button.transform.position;
            }
        }
        yield return null;
    }

    private void PlayAnimation()
    {
        if (!loading)
        {
            animations.SetBool("IsMoving",true);
        }
    }
    private void StopAnimation()
    {
            animations.SetBool("IsMoving", false);
    }
    public void MousedOverButton()
    {
        if (!loading && !waiting)
        {
            //when moused over toggle boolean and change the colour of the button
            //sprite.color = Color.grey;
            mouseOver = true;
        }
    }

    public void MouseNotOverButton()
    {
        if (!loading && !waiting)
        {
            //when mouse leaves the button toggle boolean and set colour back to base
            //sprite.color = Color.white;
            mouseOver = false;
        }
    }

    private void loadscene()
    {
        if (index == 12)
        {
            Application.Quit();
        }
        SceneManager.LoadScene(index);
    }
    public void StartGame()
    {
        loading = true;
        index = SceneManager.GetActiveScene().buildIndex+1;
        Invoke("loadscene",1f);
    }

    public void helpmenu()
    {
        loading = true;
        index=SceneManager.sceneCountInBuildSettings-1;
        Invoke("loadscene", 1f);
    }
    public void QuitGame()
    {
        loading = true;
        index = 12;
        Invoke("loadscene", 1f);
    }
    public void RestartGame()
    {
        loading = true;
        index = 0;
        Invoke("loadscene", 1f);
    }
    public void buttonclicked()
    {

        animations.SetBool("dead", true);
        deathsound.Play();
        sprite.sprite = splat;
        //sprite.color = Color.black;
    }
    [SerializeField] AudioSource jumpscaresound;
    [SerializeField] Light2D globallight;
    [SerializeField] AudioDistortionFilter distortionFilter=null;
    public void jumpscare()
    {
        animations.SetLayerWeight(0,0);
        animations.SetLayerWeight(1,1);
        distortionFilter.distortionLevel = 0.9f;
        globallight.intensity = 1f;
        globallight.color = Color.red;
        jumpscaresound.Play();
        index = SceneManager.GetActiveScene().buildIndex;
        Invoke("loadscene", 1f);
    }
}
