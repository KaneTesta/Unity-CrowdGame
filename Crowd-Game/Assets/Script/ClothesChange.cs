using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int randomInt;
        object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));
        foreach (object o in obj)
        {
            GameObject crowd = (GameObject) o;
            if ((crowd.name).Contains("CrowdMember"))
            {
                randomInt = Random.Range(0,7);

                if (randomInt >= 4){
                    Renderer[] rend = crowd.GetComponentsInChildren<Renderer>();
                    Material[] mats = rend[0].materials;

                    if (randomInt == 4){
                        mats[3].color = new Color(.08f,0,0);
                    } else if (randomInt == 5){
                        mats[3].color = new Color(0,0,0);
                    } else if (randomInt == 6){
                        mats[3].color = new Color(0,.04f,0);
                    }
                }
            }
        }
    }
}