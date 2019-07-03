using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Surf : MonoBehaviour
{

    public float moveSpeed = 2f;

    bool isMoving = true;
    bool isWandering = false;

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
        
        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));

        //Surfer Stuff
        if (isMoving == true)
        {
            transform.position += transform.forward * -moveSpeed * Time.deltaTime;

            foreach (object o in obj)
            {
                GameObject g = (GameObject) o;
                if (g.name == "Crowd Member(Clone)"){
                    if (g.transform.position.z < -4.8) {
                        g.transform.position += Vector3.down * 2*moveSpeed * Time.deltaTime;
                    }
                    if (g.transform.position.y < -2.5){
                        Destroy(g);
                    }
                }
            }
        }

        //Crowd Stuff

        // FIGURE OUT A MORE EFFICIENT WAY TO DO THIS SO THAT THE SAME CHARACTERS ARENT BEING CHECKED OVER AND OVER
        // MAYBE COMBINE THE BELOW WITH THE ABOVE


        bool now = true;
        if (now == true)
        {
            foreach (object o in obj)
            {
                GameObject crowd = (GameObject) o;
                if ((crowd.name).Contains("CrowdMember"))
                {
                    foreach (object o2 in obj)
                    {
                        GameObject surfy = (GameObject) o2;
                        if (surfy.name == "Crowd Member(Clone)")
                        {
                            if (Math.Sqrt(Math.Pow((surfy.transform.position.x - crowd.transform.position.x),2)+Math.Pow((surfy.transform.position.z - crowd.transform.position.z),2)) <=2)
                            {
                                foreach(AnimationState state in crowd.GetComponent<Animation>())
                                {
                                    if (state.name == "Crowd_Crowdsurf1"){
                                        crowd.GetComponent<Animation>().Play(state.name);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            now = false;
        }
    }

    IEnumerator SurfMove()
    {
        int moveWait = UnityEngine.Random.Range(1, 4);
        int moveTime = UnityEngine.Random.Range(1, 5);

        isWandering = true;

        yield return new WaitForSeconds(moveWait);
        isMoving = true;
        yield return new WaitForSeconds(moveTime);
        isMoving = false;

        isWandering = false;



    }
}
