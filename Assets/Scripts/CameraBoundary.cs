using UnityEngine;
using System.Collections;


//Made by Braedon (Shadow Fang Realm)
//This code can only be used for private use
public class CameraBoundary : MonoBehaviour
{
	//The first transform is where you insert your objects and the floats are for tweaking.
	//Objects should be placed in the bottom left corner and top right corner
    public Transform bottomLeftObject, topRightObject;
    Vector3 bottomLeft, topRight;
    public float tweakAmountBotX, tweakAmountBotZ, tweakAmountTopX, tweakAmountTopZ;

    void Start()
    {
		//Just setting a few variables that we only have control of
        bottomLeft = bottomLeftObject.position;
        topRight = topRightObject.position;
    }
    void Update()
    {
		//Just a simple little piece that checks if your out on the left or top or right or bottom and puts you back in the boundaries
        if (this.transform.position.x < bottomLeft.x + tweakAmountBotX)
        {
            this.transform.position = new Vector3(bottomLeft.x + tweakAmountBotX, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.z < bottomLeft.z + tweakAmountBotZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, bottomLeft.z + tweakAmountBotZ);
        }

        if (this.transform.position.x > topRight.x + tweakAmountTopX)
        {
            this.transform.position = new Vector3(topRight.x + tweakAmountTopX, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.z > topRight.z + tweakAmountTopZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, topRight.x + tweakAmountTopZ);
        }
    }
    void OnDrawGizmos()
    {
		//This gizmo code is too see what the boundaries are just check yes in the gizmo thing under play and TADA!!!
        Vector3 left = new Vector3(bottomLeft.x, topRight.y, topRight.z);
        Vector3 right = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
        Gizmos.DrawLine(bottomLeft, left); //Left Line
        Gizmos.DrawLine(left, topRight); //Top Line
        Gizmos.DrawLine(topRight, right); //Right Line
        Gizmos.DrawLine(right, bottomLeft); //Bottom Line
    }
}