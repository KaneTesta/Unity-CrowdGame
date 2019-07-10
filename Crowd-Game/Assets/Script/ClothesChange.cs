using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesChange : MonoBehaviour
{
    // Start is called before the first frame update

    //Change the colour of some of the crowd members flannies so that they aren't all just blue
    void Start()
    {
        int randomInt;
        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
        foreach (object o in obj)
        {
            GameObject crowd = (GameObject) o;
            if ((crowd.name).Contains("CrowdMember"))
            {
                randomInt = Random.Range(1,5);

                if (randomInt >= 2){
                    Renderer[] rend = crowd.GetComponentsInChildren<Renderer>();
                    Material[] mats = rend[0].materials;

                    if (randomInt == 2){
                        mats[3].color = new Color(.08f,0,0);
                    } else if (randomInt == 3){
                        mats[3].color = new Color(0,0,0);
                    } else if (randomInt == 4){
                        mats[3].color = new Color(0,.04f,0);
                    }
                }
            }
        }
    }
}