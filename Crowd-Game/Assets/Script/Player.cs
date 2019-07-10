using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Animator anim2;

    private CharacterController controller;
    private CharacterController controller2;

    private Vector3 movementVector = Vector3.zero;

    private bool isLeft = false;
    private bool isRight = false;

    float distBW = 1000;
    int keysPressed = 0;

    public GameObject[] players = new GameObject[2];
    int playerIndex = 0;

    static int SECURITY_DIST_BW = 3;
    public static float PLAYER_SPEED = 10.0f;
    static float SEC_TO_SURFER_RADIUS = 5;


    // Start is called before the first frame update
    void Start()
    {
        anim = players[0].gameObject.GetComponentInChildren<Animator>();
        controller = players[0].GetComponent<CharacterController>();
        anim2 = players[1].gameObject.GetComponentInChildren<Animator>();
        controller2 = players[1].GetComponent<CharacterController>();
        players[1].transform.Find("Directional Light").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        distBW = Math.Abs(players[0].transform.position.x - players[1].transform.position.x);
    
        movePlayer();
        securityReactionCheck();
    }


    void playerSpotlightUpdate(){

        players[playerIndex].transform.Find("Directional Light").gameObject.SetActive(true);
        if (playerIndex == 0){
            players[1].transform.Find("Directional Light").gameObject.SetActive(false);
        } else if (playerIndex == 1) {
            players[0].transform.Find("Directional Light").gameObject.SetActive(false);
        }
    }

    void movePlayer(){
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            if (Input.GetKey(KeyCode.A) && keysPressed<1){

                if (playerIndex == 0){
                    anim.SetInteger("Run",1);
                } else {
                    anim2.SetInteger("Run",1);
                }

                keysPressed++;
                if (playerIndex == 0 || (playerIndex == 1 && distBW > 3)){
                    //move -90 degrees in y axis                
                    if (!isLeft){
                        isLeft = true;

                        players[playerIndex].transform.Rotate(0, -90, 0);

                        if (isRight) {
                            isRight = false;
                            transform.Rotate(0, -90, 0);
                        }
                    }
                    movementVector = players[playerIndex].transform.forward *PLAYER_SPEED;
                } else {
                    anim2.SetInteger("Run",0);
                    anim.SetInteger("Run",0);
                }

            } else if (Input.GetKey(KeyCode.D) && keysPressed<1){

                if (playerIndex == 0){
                    anim.SetInteger("Run",1);
                } else {
                    anim2.SetInteger("Run",1);
                }

                keysPressed++;
                if (playerIndex == 1 || (playerIndex == 0 && distBW > SECURITY_DIST_BW)){
                    //move 90 degrees in y axis
                    if (!isRight){
                        isRight = true;
                        players[playerIndex].transform.Rotate(0, 90, 0);

                        if (isLeft) {
                            isLeft = false;
                            players[playerIndex].transform.Rotate(0, 90, 0);
                        }
                    }
                    movementVector = players[playerIndex].transform.forward *PLAYER_SPEED;
                } else {
                    anim.SetInteger("Run",0);
                    anim2.SetInteger("Run",0);
                }

            }

            if (playerIndex == 0){
                controller.Move(movementVector * Time.deltaTime);
            } else {
                controller2.Move(movementVector * Time.deltaTime);
            }

        } else {
            keysPressed = 0;

            if (playerIndex == 0){
                anim.SetInteger("Run",0);
            } else {
                anim2.SetInteger("Run",0);
            }

            if (isLeft){
                players[playerIndex].transform.Rotate(0, 90, 0);
            } else if (isRight){
                players[playerIndex].transform.Rotate(0, -90, 0);
            }
            isLeft = false;
            isRight = false;

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                if (playerIndex == 0){
                    playerIndex = 1;
                } else {
                    playerIndex = 0;
                }
                playerSpotlightUpdate();



            } 

        }
    }

    void securityReactionCheck(){

        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
        foreach (object o in obj){
            GameObject surfer = (GameObject) o;
            if (surfer.name == "Surfer(Clone)"){
                for (int i = 0; i < players.Length; i++){
                    if (Math.Sqrt(Math.Pow((players[i].transform.position.x - surfer.transform.position.x),2)+Math.Pow((players[i].transform.position.z - surfer.transform.position.z),2)) <= SEC_TO_SURFER_RADIUS){
                        if (i == 0){
                            anim.SetInteger("Catch",1);
                            return;
                        } else if (i == 1){
                            anim2.SetInteger("Catch",1);
                            return;
                        }
                    }
                }
            }
        }
        anim.SetInteger("Catch",0);
        anim2.SetInteger("Catch",0);
    }
}
