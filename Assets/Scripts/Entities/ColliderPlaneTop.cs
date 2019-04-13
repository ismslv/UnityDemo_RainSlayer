using UnityEngine;
using UnityEngine.Events;

public class ColliderPlaneTop : MonoBehaviour {

    public string tagToCatch;
    public bool deleteCollider;

    void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagToCatch)
        {
            if (deleteCollider)
                Destroy(other.gameObject);
        }
    }
}