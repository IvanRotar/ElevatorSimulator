using EX.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Pool;

public class GameScreenUIPanel : UIPanel, EXEventListener<GameScreenUIEvent>
{
    [SerializeField] private Transform FloorRoot;
    [Header("Lift Commander")]
    [SerializeField] private Transform RootLiftComandPanel;
    [SerializeField] private Button StopBtn,ForceDoorClose;
    [SerializeField] private Dictionary<int, Button> ControllBtn = new Dictionary<int, Button>();
    protected override void Open()
    {
        base.Open();
        this.EXEventStartListening<GameScreenUIEvent>();
        StopBtn.onClick.AddListener(() => TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.ForceStop)));
        ForceDoorClose.onClick.AddListener(() => TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.ForceDoorClose)));
    }
    protected override void Close()
    {
        base.Close();
        this.EXEventStopListening<GameScreenUIEvent>();
    }
    public void OnEXEvent(GameScreenUIEvent eventType)
    {
        switch (eventType.gameScreenUIEventType)
        {
            case GameScreenUIEvent.GameScreenUIEventType.CreateFloorItem:
                eventType.FloorItem.transform.SetParent(FloorRoot);
                break;
        }
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
        switch (eventType.gameMainEventType)
        {
            case GameMainEvent.GameMainEventType.FloorInit:
                CreateLiftComandBtn();
                break;
        }
    }
    private void CreateLiftComandBtn()
    {
        for (int i = GameDataHandler.FloorCount - 1; i >= 0; i--)
        {
            SpawnLiftBtn(FloorManager.Instance.GetFloorItem(i));
        }
        StopBtn.transform.SetAsLastSibling();
        ForceDoorClose.transform.SetAsLastSibling();
    }
    private void SpawnLiftBtn(FloorItem floorItem)
    {
        GameObject LiftButtonClone = LeanPool.Spawn(GameDataHandler.ElevatorComandButtonPrefab);
        LiftButtonClone.transform.SetParent(RootLiftComandPanel);
        Button LiftButtoBtn = LiftButtonClone.GetComponent<Button>();
        LiftButtoBtn.onClick.AddListener(() => 
        {        
            TriggerEvent(new ElevatorEvent(ElevatorEvent.ElevatorEventType.LiftCabineMoveComand, floorItem));
        });
        LiftButtoBtn.GetComponentInChildren<Text>().text = floorItem.FloorNumber.ToString();
        ControllBtn.Add(floorItem.FloorNumber, LiftButtoBtn);
    }
}
