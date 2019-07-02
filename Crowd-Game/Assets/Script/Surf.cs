using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surf : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    bool isMoving = false;
    bool isWandering = false;
    bool isRotatingRight = false;
    bool isRotatingLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (isWandering == false) 
       {
           StartCoroutine(SurfMove());
       } 

        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }

        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }

        if (isMoving == true)
        {
            transform.position += transform.forward * -moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator SurfMove()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int moveWait = Random.Range(1, 4);
        int moveTime = Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(moveWait);
        isMoving = true;
        yield return new WaitForSeconds(moveTime);
        isMoving = false;
        yield return new WaitForSeconds(rotateWait);

        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        } else if (rotateLorR == 2) {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;



    }
}
