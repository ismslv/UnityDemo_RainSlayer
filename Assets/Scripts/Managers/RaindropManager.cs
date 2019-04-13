using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropManager : MonoBehaviour {

    //settings
    public Vector2 dropsXLimits;
    public Vector2 dropsZLimits;
    public float dropsY;
    public Vector2 dropsSizeLimits;
    public Vector2 dropsSpeedLimits;

    public float dropRate;

    //entities
    public GameObject prefabDrop;
    public Animator reelAnimator;
    public AudioSource audioMusic;

    //states
    public bool isRaining;

	void Start () {
        StartCoroutine(NewDrop(dropRate));
    }
	
	void Update () {
        
    }

    void GenerateRandomRaindrop()
    {
        float size = GetRandomSize();
        CreateRaindrop(new Vector3(GetRandomX(), dropsY, GetRandomZ()), GetSpeed(size), size);
    }

    float GetRandomX()
    {
        return Random.Range(dropsXLimits.x, dropsXLimits.y);
    }

    float GetRandomZ()
    {
        return Random.Range(dropsZLimits.x, dropsZLimits.y);
    }

    float GetRandomSize()
    {
        return Random.Range(dropsSizeLimits.x, dropsSizeLimits.y);
    }

    //the bigger the slower
    float GetSpeed(float size)
    {
        return size.Remap(dropsSizeLimits.x, dropsSizeLimits.y, dropsSpeedLimits.y, dropsSpeedLimits.x) * Random.Range(0.9f, 1.1f);
    }

    //instantiating a new raindrop instance
    void CreateRaindrop(Vector3 pos, float speed, float size)
    {
        GameObject drop = Instantiate(prefabDrop);
        Raindrop dropR = drop.GetComponent<Raindrop>();
        dropR.speed = speed;
        dropR.size = size;
        drop.transform.position = pos;
        drop.transform.localScale = new Vector3(size, size, size);
    }

    //timer to a new drop
    IEnumerator NewDrop(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (isRaining)
        {
            GenerateRandomRaindrop();
        }
        StartCoroutine(NewDrop(dropRate));
    }

    public void ButtonStart()
    {
        reelAnimator.SetTrigger("Close");
        isRaining = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        audioMusic.pitch = 1f;
        Core.a.CounterStart();
    }
}
