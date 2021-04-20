using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //private variables
    Rigidbody2D rB;
    WaitForSeconds delay = new WaitForSeconds(1);
    int moveDistance;
    bool isMoving;

    //public variables
    public float currentXPos;
    public float currentYPos;
    public float nextXPos;
    public float nextYPos;
    public int status;

    //dice graphics
    public GameObject die1;
    public GameObject die2;
    public GameObject die3;
    public GameObject die4;
    public GameObject die5;
    public GameObject die6;

    //oneway colliders
    public GameObject oneway1;
    public GameObject oneway2;
    public GameObject oneway3;
    public GameObject oneway4;
    public GameObject oneway5;
    public GameObject oneway6;
    public GameObject oneway7;


    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        status = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Move(nextXPos,nextYPos);
            nextXPos = 0;
            nextYPos = 0;
        }
        //start moving
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (isMoving == false)
            {
                StartCoroutine(Movement(moveDistance));
            }
        }
        //choose random distance
        else if (Input.GetKeyDown(KeyCode.K))
        {
            moveDistance = Random.Range(1, 6);
            Debug.Log("Move distance = " + moveDistance);
        }

        //choose movement direction
        if (isMoving == false)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextXPos++;
                rB.rotation = 0;
                Debug.Log("Next X Position = " + nextXPos);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextXPos--;
                rB.rotation = 180;
                Debug.Log("Next X Position = " + nextXPos);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                nextYPos++;
                rB.rotation = 90;
                Debug.Log("Next Y Position = " + nextYPos);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                nextYPos--;
                rB.rotation = 270;
                Debug.Log("Next Y Position = " + nextYPos);
            }
        } 
    }

    


        //move set distance, 1 unit per second
        IEnumerator Movement(int distance)
    {
        while (distance > 0)
        {
            isMoving = true;
            rB.transform.Translate(1, 0, 0);
            distance--;
            Debug.Log("Distance left = " + distance);
            yield return delay;
        }
        //conveyor move
        if (status == 1)
        {
            isMoving = true;
            rB.transform.Translate(1, 0, 0); //could be 2?
            status = 0;
            Debug.Log("Distance left = " + distance);
            yield return delay;
        }
        //ice move
        if (status == -1)
        {
            isMoving = true;
            rB.transform.Translate(-1, 0, 0);
            status = 0;
            Debug.Log("Distance left = " + distance);
            yield return delay;
        }

        if (distance == 0)
        {
            isMoving = false;
        }
    }

    //move for the legacy function of choosing
    public void Move(float xPos, float yPos)
    {
        rB.transform.Translate(xPos, yPos, 0, Space.World);
        currentXPos = rB.transform.position.x;
        currentYPos = rB.transform.position.y;
        Debug.Log("Current X and Y = " + currentXPos + "," + currentYPos);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered a trigger");

        if (collision.CompareTag("Goal"))
        {
            Debug.Log("Entered the goal");
        }
        //collide to respawn
        if (collision.CompareTag("respawn"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //collide with conveyor
        if (collision.CompareTag ("conveyor"))
        {
            status = 1;
            //StartCoroutine(Movement(moveDistance));
        }
        //collide with ice
        if (collision.CompareTag("ice"))
        {
            status = -1;
            //StartCoroutine(Movement(moveDistance));
        }
        //sets oneway to deactive
        if (collision.CompareTag("onewaytrigger"))
        {
            oneway1.SetActive(false);
            oneway2.SetActive(false);
            oneway3.SetActive(false);
            oneway4.SetActive(false);
            oneway5.SetActive(false);
            oneway6.SetActive(false);
            oneway7.SetActive(false);


        }
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with something");
        
        
    }

    //reactivates oneway
    private void OnCollisionExit2D(Collision2D collision)
    {
        status = 0;
        //if (collision.gameObject.tag == "onewaytrigger")
       // {
            oneway1.SetActive(true);
            oneway2.SetActive(true);
            oneway3.SetActive(true);
            oneway4.SetActive(true);
            oneway5.SetActive(true);
            oneway6.SetActive(true);
            oneway7.SetActive(true);
      // }
    }


    //THESE ARE THE BUTTONY BOYS!
    public void LeftButton()
    {
        nextXPos--;
        rB.rotation = 180;
        Debug.Log("Next X Position = " + nextXPos);
    }

    public void RightButton()
    {
        nextXPos++;
        rB.rotation = 0;
        Debug.Log("Next X Position = " + nextXPos);
    }

    public void UpButton()
    {
        nextYPos++;
        rB.rotation = 90;
        Debug.Log("Next Y Position = " + nextYPos);
    }

    public void DownButton()
    {
        nextYPos--;
        rB.rotation = 270;
        Debug.Log("Next Y Position = " + nextYPos);
    }

    public void GoButton()
    {
        moveDistance = Random.Range(1, 6) + status;
        Debug.Log("Move distance = " + moveDistance);
        if (isMoving == false)
        {
            StartCoroutine(Movement(moveDistance));
        }
        //these are for the rolls
        if (moveDistance == 1)
        {
            die1.SetActive(true);
            die2.SetActive(false);
            die3.SetActive(false);
            die4.SetActive(false);
            die5.SetActive(false);
            die6.SetActive(false);
        }
        if (moveDistance == 2)
        {
            die2.SetActive(true);
            die1.SetActive(false);
            die3.SetActive(false);
            die4.SetActive(false);
            die5.SetActive(false);
            die6.SetActive(false);
        }
        if (moveDistance == 3)
        {
            die3.SetActive(true);
            die2.SetActive(false);
            die1.SetActive(false);
            die4.SetActive(false);
            die5.SetActive(false);
            die6.SetActive(false);
        }
        if (moveDistance == 4)
        {
            die4.SetActive(true);
            die2.SetActive(false);
            die3.SetActive(false);
            die1.SetActive(false);
            die5.SetActive(false);
            die6.SetActive(false);
        }
        if (moveDistance == 5)
        {
            die5.SetActive(true);
            die2.SetActive(false);
            die3.SetActive(false);
            die4.SetActive(false);
            die1.SetActive(false);
            die6.SetActive(false);
        }
        if (moveDistance == 6)
        {
            die6.SetActive(true);
            die2.SetActive(false);
            die3.SetActive(false);
            die4.SetActive(false);
            die5.SetActive(false);
            die1.SetActive(false);

        }
    }
}
