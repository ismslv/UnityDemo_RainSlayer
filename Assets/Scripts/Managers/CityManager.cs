using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : MonoBehaviour {

    //entities
    public Transform waterbody;
    public Image[] rulerSteps;
    public AudioSource audioplayer;
    public Transform weaponRoot;
    public Animator weaponAnimator;
    public Transform weaponTrunk;
    public Transform weaponShootPoint;
    public GameObject prefabMissile;

    //settings
    public AudioClip soundSplash;
    public AudioClip soundPop;
    public Color rulerStepColorOff;
    public Color rulerStepColorOn;
    public float levelRiseCoeff;
    public float shootRate;
    public float missileSpeed;

    //states
    public float waterLevel;
    public bool toShoot = false;

    //inner
    private RulerStep[] steps;

    void Start () {
        GenerateSteps();
        StartCoroutine(ShootNext());
    }
	
	void Update () {

	}

    //generating an array of ruler steps and respective levels
    void GenerateSteps()
    {
        steps = new RulerStep[rulerSteps.Length];
        float lvl = 0;
        for (int i = 0; i < steps.Length; i++)
        {
            lvl += 2f;
            steps[i] = new RulerStep()
            {
                level = lvl,
                image = rulerSteps[i],
                isOn = false
            };
        }
    }

    //check each step if underwater
    void CheckSteps()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            if (waterLevel >= steps[i].level)
            {
                //is underwater
                if (!steps[i].isOn)
                {
                    //not to recolor twice
                    steps[i].isOn = true;
                    steps[i].image.color = rulerStepColorOn;
                }
            } else
            {
                //is over water
                if (steps[i].isOn)
                {
                    //not to recolor twice
                    steps[i].isOn = false;
                    steps[i].image.color = rulerStepColorOff;
                }
            }
        }
    }

    //if drop collides with the water, delete the drop and raise the water level depending on drop size
    public void DropFallen(GameObject dropObj)
    {
        WaterRise(dropObj.GetComponent<Raindrop>().size);
        Destroy(dropObj);
    }

    //to be called when raindrop has fallen down
    void WaterRise(float size)
    {
        audioplayer.PlayOneShot(soundSplash);
        waterLevel += size * levelRiseCoeff;
        Tween.a.MoveTo(waterbody, new Vector3(0, waterLevel / 10, 0), 0.5f, Easing.Ease.Spring);
        CheckSteps();
    }

    //waterdrop is popped
    public void DropPop()
    {
        audioplayer.PlayOneShot(soundPop);
    }

    public void Shoot()
    {
        GameObject missile = Instantiate(prefabMissile);
        prefabMissile.transform.position = weaponShootPoint.position;
        missile.GetComponent<Missile>().speed = missileSpeed;
    }

    //check for shooting at a given rate
    IEnumerator ShootNext()
    {
        yield return new WaitForSeconds(shootRate);
        if (toShoot)
        {
            //add random
            if (Random.Range(0, 2) == 0)
                Shoot();
        }
        StartCoroutine(ShootNext());
    }

    public void Lose()
    {
        Core.a.managerR.isRaining = false;
        Core.a.managerR.reelAnimator.SetTrigger("Lose");
        Core.a.managerR.audioMusic.pitch = 0.4f;
    }

    struct RulerStep
    {
        public float level;
        public Image image;
        public bool isOn;
    }
}
