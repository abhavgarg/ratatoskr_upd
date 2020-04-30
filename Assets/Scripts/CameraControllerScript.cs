using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

    public GameObject player;
    private float furthestXPos = 0f;
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 newCameraPosition = player.transform.position;

        if(newCameraPosition.y < -1.69f)
        {
            newCameraPosition.y = -1.69f;
        }

        if (newCameraPosition.y > 5.0f)
        {
            newCameraPosition.y = 5.0f;
        }

        if (newCameraPosition.x <= furthestXPos) //camera movements not restricted
        {
            newCameraPosition.x = furthestXPos;
        }
        /*else
        {
            furthestXPos = newCameraPosition.x;
        }*/

        newCameraPosition.z = this.transform.position.z;

        this.transform.position = newCameraPosition;

 

		/*if(Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}*/
	}
}
