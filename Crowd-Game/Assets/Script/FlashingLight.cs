using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    Light testLight;
	
	void Start () {
		testLight = GetComponent<Light>();
		StartCoroutine(Flashing());
	}
	
	IEnumerator Flashing ()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.25f);
			testLight.enabled = ! testLight.enabled;

		}
	}
}
