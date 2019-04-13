using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour {

    public static Core a;

    //managers
    public CityManager managerC;
    public RaindropManager managerR;

    //screen size and aspect ratio
    int lastWidth = Screen.width;
    bool isReseting = false;

    //entities
    public Text counterText;

    //inner
    private int counter;

    private void Awake()
    {
        a = this;
    }

    void Start () {
       
	}
	
	void Update () {
		
	}

    void LateUpdate()
    {
        if (Camera.main.aspect != 0.6f && !isReseting)
        {
            if (Screen.width != lastWidth)
            {
                // user is resizing width
                StartCoroutine(SetResolution());
                lastWidth = Screen.width;
            }
            else
            {
                // user is resizing height
                StartCoroutine(SetResolution());
            }
        }
    }

    //try to keep persistent aspect ratio
    IEnumerator SetResolution()
    {
        isReseting = true;
        Screen.fullScreen = !Screen.fullScreen;
        Screen.SetResolution(Mathf.RoundToInt(Screen.height * 0.6f), Screen.height, false);
        yield return new WaitForSeconds(0.5f);
        isReseting = false;
    }

    public void CounterStart()
    {
        StartCoroutine(CounterTick());
    }

    //Update counter
    IEnumerator CounterTick()
    {
        yield return new WaitForSeconds(1);
        counter++;
        counterText.text = counter.ToString();
        StartCoroutine(CounterTick());
    }
}
