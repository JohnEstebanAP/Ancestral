using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour
{
    public FixedTouchField _FixedTouchField;
    public PlayerCamera _PlayerCamera;
    void Start()
    {
        
    }

    
    void Update()
    {
        _PlayerCamera.LookAxis = _FixedTouchField.TouchDist;
    }
}
