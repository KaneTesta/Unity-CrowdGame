using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject surfer;
    int xPos;
    public int surfCount = 0;
    int surfType;
    static float currentScale = 0.4f;
    Animation anim;

    bool isMoving = true;
    bool isWandering = false;
    string prevState;
    public int lives = 3;

    static double BARRIER_Z_COORD = -4.8;
    static double FLOOR_Y_COORD_FOR_DEATH = -2.5;
    static double CROWD_TO_SURFER_RADIUS = 5;
    static float SURF_SPEED = 5f;
    float SPAWN_DELAY = 2f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SurfSpawn());
    }

    void Update() {

        //Controls the reactions of crowd to surfers
        if (isWandering == false) 
       {
           StartCoroutine(SurfMove());
           InvokeRepeating("SurferReact", 0f, 2.0f);
       } 
        
        //Make the surfers move
        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
        if (isMoving == true)
        {
            //Look for surfers in all gameobjects and move them accordingly
            foreach (object o in obj)
            {
                GameObject surfy = (GameObject) o;
                if (surfy.name == "Surfer(Clone)"){
                    surfy.transform.position += surfy.transform.forward * -SURF_SPEED * Time.deltaTime;
                    if (surfy.transform.position.z < BARRIER_Z_COORD) {
                        surfy.transform.position += Vector3.down * 2*SURF_SPEED * Time.deltaTime;
                        changeAllMaterials(surfy, Color.red);
                    }
                    if (surfy.transform.position.y < FLOOR_Y_COORD_FOR_DEATH){
                        Destroy(surfy);
                        lives--;
                    }
                }
            }
        }
    }

    void changeAllMaterials(GameObject surfy, Color c)
    {
        Component[] surferChildren = surfy.GetComponentsInChildren(typeof (Renderer));
        foreach(Renderer child in surferChildren) {
            Material[] mats = child.materials;
            if (mats[0].color != c && mats[0].color != Color.green){
                foreach(Material mat in mats){
                    mat.color = c;
                }
            }
        }
    }


    IEnumerator SurfSpawn()
    {
        while (surfCount < 15)
        {
            //Instantiate Random Variables
            xPos = UnityEngine.Random.Range(-9,9);
            surfType = UnityEngine.Random.Range(1,3);
            
            //Create Character in Game
            if (surfType == 1){
                Instantiate(surfer, new Vector3(xPos,5.5f,6.75f), Quaternion.identity).GetComponent<Animation>().Play("Surfer1");;
            } else if (surfType == 2){
                Instantiate(surfer, new Vector3(xPos,5.5f,6.75f), Quaternion.identity).GetComponent<Animation>().Play("Surfer2");;
            }
            surfer.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return new WaitForSeconds(SPAWN_DELAY);
            surfCount+=1;
        }
    }

    // Make the crowd react to the surfers
    void SurferReact()
    {
        bool surferExists = false;
        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
        //Surfer Stuff
        if (isMoving == true)
        {
            foreach (object o in obj)
            {
                GameObject surfy = (GameObject) o;
                if (surfy.name == "Crowd Member(Clone)"){
                    surferExists = true;
                    if (surfy.transform.position.z >= BARRIER_Z_COORD) {
                        //CROWD REACTION TO SURFERS
                        foreach (object o2 in obj)
                        {
                            GameObject crowd = (GameObject) o2;
                            if ((crowd.name).Contains("CrowdMember"))
                            {

                                //Make a movement to catch the surfers if they are close enough
                                if (Math.Sqrt(Math.Pow((surfy.transform.position.x - crowd.transform.position.x),2)+Math.Pow((surfy.transform.position.z - crowd.transform.position.z),2)) <= CROWD_TO_SURFER_RADIUS)
                                {
                                    int crowdReaction = UnityEngine.Random.Range(1, 7);
                                    foreach(AnimationState state in crowd.GetComponent<Animation>())
                                    {
                                        if (crowdReaction == 5)
                                        {
                                            if (state.name == "Crowd_Crowdsurf2")
                                            {
                                                crowd.GetComponent<Animation>().Play(state.name);
                                                break;
                                            }
                                        } else if (crowdReaction == 6)
                                        {
                                            if (state.name == "Crowd_Crowdsurf3")
                                            {
                                                crowd.GetComponent<Animation>().Play(state.name);
                                                break;
                                            }
                                        } else 
                                        {
                                            if (state.name == "Crowd_Crowdsurf1")
                                            {
                                                crowd.GetComponent<Animation>().Play(state.name);
                                                break;
                                            }
                                        }
                                    }
                                } else 
                                {
                                    // If the person is now far from a crowd surfer, continue rocking!
                                    foreach(AnimationState state in crowd.GetComponent<Animation>())
                                    {
                                        if (state.name.Contains("Rock"))
                                        {
                                            crowd.GetComponent<Animation>().Play(state.name);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //If no surfer exists, continue rocking out!
            if (surferExists == false)
            {
                foreach (object o in obj)
                {
                    GameObject crowd = (GameObject) o;
                    if ((crowd.name).Contains("CrowdMember")){
                        foreach(AnimationState state in crowd.GetComponent<Animation>())
                        {
                            if (state.name.Contains("Rock"))
                            {
                                crowd.GetComponent<Animation>().Play(state.name);
                                break;
                            }
                        }
                    }
                }
            }
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
