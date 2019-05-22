using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using EX.Tools;

[Serializable]
public class FloorPosition
{
    public int YPosition;
    public Transform LiftPoint; 
}
[Serializable]
public class FloorUIControl
{
    public Text FloorLabel;
    public Button UPComand, DownComand;
    public void ActivateButton(params bool[] act)
    {
        UPComand.gameObject.SetActive(act[0]);
        DownComand.gameObject.SetActive(act[1]);
    }
}
[Serializable]
public class FloorCell
{
    [SerializeField] private FloorPosition floorPosition;
    [SerializeField] private FloorUIControl floorUIControl;
    public void Init(int PosY)
    {
        FloorPosition = PosY;
        SetFloorLabel();
        InitFloorButton();
    }
    public bool FirstFloor
    {
        get
        {
            return FloorPosition == GameDataHandler.FirstFloorNumber;
        }
    }
    public bool LastFloor
    {
        get
        {
            return FloorPosition == GameDataHandler.FloorCount;
        }
    }
    public int FloorPosition
    {
        get
        {
            return floorPosition.YPosition;
        }
        set
        {
            floorPosition.YPosition = value;
        }
    }
    public void SetFloorLabel()
    {
        floorUIControl.FloorLabel.text = "Floor " + FloorPosition.ToString();
    }
    public void InitFloorButton()
    {
        floorUIControl.ActivateButton(!LastFloor, !FirstFloor);
    }
    public Vector3 LiftPointPosition()
    {
        return floorPosition.LiftPoint.position;
    }
    public Button GetFloorControllButton(ElevatorMoveDirection elevatorMoveDirection)
    {
        return elevatorMoveDirection == ElevatorMoveDirection.Up ? floorUIControl.UPComand : floorUIControl.DownComand;
    }
}

