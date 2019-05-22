using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;

public class UIPanel : EXBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform RootTransform;
    public bool IsOpen
    {
        get
        {
            return RootTransform.gameObject.activeSelf;
        }
        set
        {
            if (value) Open();
            else Close();
        }
    }
    protected virtual void Open()
    {
        RootTransform.gameObject.SetActive(true);
    }
    protected virtual void Close()
    {
        RootTransform.gameObject.SetActive(false);
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
    }
}