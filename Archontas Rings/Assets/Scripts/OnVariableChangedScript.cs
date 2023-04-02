using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnVariableChangedScript : MonoBehaviour
{

    public delegate void OnVariableChangedDelegate<T>(T value);
    public static event OnVariableChangedDelegate<bool> OnVariableChangedEvent;

    public bool myBool
    {
        get { return myBool; }
        set
        {
            if(myBool == value) { return; }
            myBool = value;
            OnVariableChangedEvent(value);
        }
    }

    public void setChangedEventToBool(ref bool refBool)
    {
        
    }
  
}
