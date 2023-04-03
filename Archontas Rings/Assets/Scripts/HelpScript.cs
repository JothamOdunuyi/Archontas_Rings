using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour
{

    public Transform start;
    private void Awake()
    {

        augh(start);
        

    }

    private void augh(Transform obj)
    {
        print(obj.name);
        if(obj.childCount > 0)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                if (obj.GetChild(i).GetComponent<Rigidbody>())
                {
                    //Destroy(transform.GetChild(i).GetComponent<CharacterJoint>());
                    // Destroy(transform.GetChild(i).GetComponent<Rigidbody>());

                    obj.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    //transform.GetChild(i).GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
                }

                augh(obj.GetChild(i).transform);
                /// All your stuff with transform.GetChild(i) here...
            }
        }
       
    }
}
