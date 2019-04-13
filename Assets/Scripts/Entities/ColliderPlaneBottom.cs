using UnityEngine;
using UnityEngine.Events;

public class ColliderPlaneBottom : MonoBehaviour {
    
    public bool deleteCollider;

    void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "raindrop")
        {
            Core.a.managerC.DropFallen(other.gameObject);
            if (deleteCollider)
                Destroy(other.gameObject);
        } else if (other.tag == "Player")
        {
            Core.a.managerC.Lose();
        }
    }
}