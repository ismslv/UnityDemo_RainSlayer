using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed;

	void Start () {
		
	}
	
	void Update () {
        transform.Translate(new Vector3(0, speed, 0));
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "raindrop")
        {
            other.GetComponent<Raindrop>().GetDamage();
            Destroy(gameObject);
        }
    }
}
