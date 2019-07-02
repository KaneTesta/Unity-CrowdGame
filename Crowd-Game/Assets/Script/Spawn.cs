using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject crowdMember;
    public int xPos;
    public int zPos;
    public int crowdCount = 0;
    public int danceMove;
    public static float currentScale = 0.4f;
    public Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CrowdSpawn());
    }


    IEnumerator CrowdSpawn()
    {
        while (crowdCount < 50)
        {
            //Instantiate Random Variables
            xPos = Random.Range(-16,19);
            zPos = Random.Range(-3,17);
            danceMove = Random.Range(1,4);
            

            //Create Character in Game
            Instantiate(crowdMember, new Vector3(xPos,0,zPos), Quaternion.Euler(0,180,0));
            crowdMember.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            anim = crowdMember.GetComponent<Animation>();

            if (danceMove == 1){
                Debug.Log("Option 1");
                anim.Play("Rock_Out1");
            } else if(danceMove == 2){
                Debug.Log("Option 2");
                anim.Play("Rock_Out2");
            } else {
                Debug.Log("Option 3");
                anim.Play("Rock_Out3");
            }
            

            yield return new WaitForSeconds(0);
            crowdCount+=1;
        }
    }


}
