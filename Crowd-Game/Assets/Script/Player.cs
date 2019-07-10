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
    public float speed = 6.0f;
    private Vector3 movementVector = Vector3.zero;
    private bool isLeft = false;
    private bool isRight = false;
    float distBW = 1000;
    int keysPressed = 0;

    public GameObject[] players = new GameObject[2];
    int playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = players[0].gameObject.GetComponentInChildren<Animator>();
        controller = players[0].GetComponent<CharacterController>();
        anim2 = players[1].gameObject.GetComponentInChildren<Animator>();
        controller2 = players[1].GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        distBW = Math.Abs(players[0].transform.position.x - players[1].transform.position.x);
      
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){

            if (Input.GetKey(KeyCode.A) && keysPressed<1){

                if (playerIndex == 0){
                    anim.SetInteger("AnimPar",1);
                } else {
                    anim2.SetInteger("AnimPar",1);
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
                    movementVector = players[playerIndex].transform.forward *speed;
                } else {
                    anim2.SetInteger("AnimPar",0);
                    anim.SetInteger("AnimPar",0);
                }

            } else if (Input.GetKey(KeyCode.D) && keysPressed<1){

                if (playerIndex == 0){
                    anim.SetInteger("AnimPar",1);
                } else {
                    anim2.SetInteger("AnimPar",1);
                }

                keysPressed++;
                if (playerIndex == 1 || (playerIndex == 0 && distBW > 3)){
                    //move 90 degrees in y axis
                    if (!isRight){
                        isRight = true;
                        players[playerIndex].transform.Rotate(0, 90, 0);

                        if (isLeft) {
                            isLeft = false;
                            players[playerIndex].transform.Rotate(0, 90, 0);
                        }
                    }
                    movementVector = players[playerIndex].transform.forward *speed;
                } else {
                    anim.SetInteger("AnimPar",0);
                    anim2.SetInteger("AnimPar",0);
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
                anim.SetInteger("AnimPar",0);
            } else {
                anim2.SetInteger("AnimPar",0);
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

            } 

        }
    }
}
