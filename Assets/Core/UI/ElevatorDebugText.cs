using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;
using UnityEngine.UI;

public class ElevatorDebugText : PersistentSingleton<ElevatorDebugText>
{
    private Text DebugText;
    private void Start()
    {
        DebugText = GetComponent<Text>();
    }
    public void SetText(string owner, string msg)
    {
        DebugText.text = owner.ToUpper() + ": " + msg;
    }
}
