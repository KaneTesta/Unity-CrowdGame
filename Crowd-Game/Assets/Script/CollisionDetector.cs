using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter(Collider other){
        if (other.name.Contains("Surfer")){
            Destroy(other.gameObject);
            score++;
        }
    }
}
