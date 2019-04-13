using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour {

    public float speed;
    public float size;

    private int health;

    private Vector3 moveVector;

	void Start () {
        health = Mathf.CeilToInt(size);
	}

    private void OnEnable()
    {
        
    }

    void Update () {
        moveVector = new Vector3(0, -0.01f * speed, 0);
        transform.Translate(moveVector);
	}

    //hit by missile
    public void GetDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            Core.a.managerC.DropPop();
        }
    }
}
