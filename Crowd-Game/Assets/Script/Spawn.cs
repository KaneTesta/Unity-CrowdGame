using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject surfer1;
    public GameObject surfer2;
    int xPos;
    int surfCount = 0;
    int surfType;
    static float currentScale = 0.4f;
    Animation anim;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SurfSpawn());
    }


    IEnumerator SurfSpawn()
    {
        while (surfCount < 10)
        {
            //Instantiate Random Variables
            xPos = Random.Range(-14,9);
            surfType = Random.Range(1,3);
            
            Debug.Log(surfType);
            //Create Character in Game
            if (surfType == 1){
                Instantiate(surfer1, new Vector3(xPos,6,6.75f), Quaternion.identity);
                surfer1.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                anim = surfer1.GetComponent<Animation>();
                anim.wrapMode = WrapMode.Loop;
            } else if (surfType == 2){
                Instantiate(surfer2, new Vector3(xPos,6,6.75f), Quaternion.identity);
                surfer2.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
                anim = surfer2.GetComponent<Animation>();
                anim.wrapMode = WrapMode.Loop;
            }
            yield return new WaitForSeconds(3);
            surfCount+=1;
        }
    }


}
