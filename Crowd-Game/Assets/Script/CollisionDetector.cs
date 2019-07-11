using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter(Collider other){
        if (other.name.Contains("Surfer")){
            StartCoroutine(DestroyPlayer(other.gameObject));
            score++;
        }
    }

    void changeAllMaterials(GameObject surfy, Color c)
    {
        Component[] surferChildren = surfy.GetComponentsInChildren(typeof (Renderer));
        foreach(Renderer child in surferChildren) {
            Material[] mats = child.materials;
            if (mats[0].color == Color.red){
                foreach(Material mat in mats){
                    mat.color = c;
                }
            }
        }
    }

    IEnumerator DestroyPlayer(GameObject o){
        changeAllMaterials(o, Color.green);
        yield return new WaitForSeconds(0.2f);
        Destroy(o);
    }
}
