using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    //settings
    public float moveXCoeff;
    public float smoothX;
    public float moveYCoeff;
    public float smoothY;
    public Vector2 trunkMoveLimits;

    void Start () {

    }
	
	void Update () {
		if (Core.a.managerR.isRaining)
        {
            //get mouse axis
            float mouseX = Input.GetAxisRaw("Mouse X");
            Vector3 pos = Core.a.managerC.weaponRoot.position;
            //get movement and clamp it to the borders
            pos.x = Mathf.Clamp(pos.x + mouseX * moveXCoeff, Core.a.managerR.dropsXLimits.x - 0.1f, Core.a.managerR.dropsXLimits.y + 0.1f);
            //smoothly move to
            float t = Time.deltaTime * smoothX;
            Core.a.managerC.weaponRoot.position = Vector3.Lerp(Core.a.managerC.weaponRoot.position, pos, t);

            //set wheels animation
            if (mouseX == 0.0f)
            {
                Core.a.managerC.weaponAnimator.SetFloat("Speed", 0.5f);
            } else
            {
                Core.a.managerC.weaponAnimator.SetFloat("Speed", mouseX > 0.0f ? 0f : 1f);
            }

            //get scrollwheel
            float wheelY = Input.mouseScrollDelta.y;
            if (wheelY != 0.0f)
            {
                //move trunk vertically
                Vector3 posTrunk = Core.a.managerC.weaponTrunk.localPosition;
                //get movement and clamp it to the limits
                posTrunk.y = Mathf.Clamp(posTrunk.y + wheelY * moveYCoeff, trunkMoveLimits.x, trunkMoveLimits.y);
                //smoothly move to
                t = Time.deltaTime * smoothY;
                Core.a.managerC.weaponTrunk.localPosition = Vector3.Lerp(Core.a.managerC.weaponTrunk.localPosition, posTrunk, t);
            }

            //shoot if left mousebutton
            Core.a.managerC.toShoot = Input.GetMouseButton(0);
        }
	}
}
