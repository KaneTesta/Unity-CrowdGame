using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject SurferScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Get Lives from SurferSpawn GameObject
        int l = SurferScript.GetComponent<Spawn>().lives;
        Debug.Log(l);
    }
}
