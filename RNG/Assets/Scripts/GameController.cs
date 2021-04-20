using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
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
        else if (Input.GetKeyDown(KeyCode.J))
        {
            if (isMoving == false)
            {
                StartCoroutine(Movement(moveDistance));
            }
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            moveDistance = Random.Range(1, 6);
            Debug.Log("Move distance = " + moveDistance);
        }

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
        if(distance == 0)
        {
            isMoving = false;
        }
    }


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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with something");
    }
}
