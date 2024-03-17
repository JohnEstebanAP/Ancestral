using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptController : MonoBehaviour
{
    public FixedTouchField _FixedTouchField;
    public PlayerCamera _PlayerCamera;
    public Player1 _Player1;
    public FixedButton _FixedButton;
    void Start()
    {
        
    }

    
    void Update()
    {
        _PlayerCamera.LookAxis = _FixedTouchField.TouchDist;
        _Player1.Pressed = _FixedButton.Pressed;
    }
}
