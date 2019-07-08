using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;
    private Animator anim2;
    private CharacterController controller;
    private CharacterController controller2;
    public float speed = 6.0f;
    int moveDirection = -1;
    private Vector3 movementVector = Vector3.zero;
    private bool isLeft = false;
    private bool isRight = false;

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
        Debug.Log(playerIndex);

        if (Input.GetKeyDown(KeyCode.LeftShift)){
            isLeft = false;
            isRight = false;

            if (playerIndex == 0){
                playerIndex = 1;
                anim.SetInteger("AnimPar",0);
            } else {
                playerIndex = 0;
                anim2.SetInteger("AnimPar",0);
            }

        } 
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){


            if (playerIndex == 0){
                anim.SetInteger("AnimPar",1);
            } else {
                anim2.SetInteger("AnimPar",1);
            }

            if (Input.GetKey(KeyCode.A)){

                moveDirection = -1;
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

                moveDirection = 1;
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

            }
            if (playerIndex == 0){
                controller.Move(movementVector * Time.deltaTime);
            } else {
                controller2.Move(movementVector * Time.deltaTime);
            }

        } else {
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

        }



    }
}
