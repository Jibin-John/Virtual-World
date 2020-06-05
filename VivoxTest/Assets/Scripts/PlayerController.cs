using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    public float speed;
    

    
    private void FixedUpdate()
    {

		if (Input.GetKey(KeyCode.RightArrow))
		{

			Vector3 newPosition = character.transform.position;
			newPosition.x++;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{

			Vector3 newPosition = character.transform.position;
			newPosition.x--;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.UpArrow))
		{

			Vector3 newPosition = character.transform.position;
			newPosition.z++;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.DownArrow))
		{

			Vector3 newPosition = character.transform.position;
			newPosition.z--;
			character.transform.position -= newPosition;

		}
	}

}
