using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakeLightws : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetChildrensGravity(transform);
    }


    private void SetChildrensGravity(Transform obj)
    {
        if (obj.childCount > 0)
        {
            for (int i = 0; i < obj.childCount; i++)
            {
                if (obj.GetChild(i).GetComponent<Light>())
                {
                    //obj.GetChild(i).GetComponent<Light>().lightmapBakeType = LightmapBakeType.Baked;
                }

                SetChildrensGravity(obj.GetChild(i).transform);
            }
        }

    }
}
