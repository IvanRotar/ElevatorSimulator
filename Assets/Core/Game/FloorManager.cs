using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;
using Lean.Pool;
using System.Linq;

public class FloorManager : EXBehaviour, EXEventListener<FloorEvent>
{
    private static FloorManager _instance;
    public static FloorManager Instance
    {
        get
        {
            if (_instance == null) { _instance = FindObjectOfType<FloorManager>(); }
            return _instance;
        }
    }
    private List<FloorItem> floorItems = new List<FloorItem>();
    #region APi
    public int FloorCount
    {
        get
        {
            return floorItems.Count;
        }
    }
    public void RegisterNewFloor(FloorItem floorItem)
    {
        floorItems.Add(floorItem);
    }
    public void UnRegisterFloor(FloorItem floorItem)
    {
        floorItems.Remove(floorItem);
    }
    public FloorItem GetFloorItem(int index)
    {
        return floorItems[index];
    }
    public FloorItem GetFloorByNumber(int index)
    {
        return floorItems.FirstOrDefault(floor => floor.FloorNumber == index);
    }
    public FloorItem GetRandomFloorItem()
    {
        int rand = Random.Range(0, floorItems.Count);
        return floorItems[rand];
    }
    #endregion
    public override void OnEXEvent(GameMainEvent eventType)
    {
        switch (eventType.gameMainEventType)
        {
            case GameMainEvent.GameMainEventType.GameStart:
                TriggerEvent(new UIEvent(UIEvent.UIEventType.OpenPanel, "GameScreenUIPanel"));
                CreateFloorItem();
                break;
        }
    }
    private void CreateFloorItem()
    {
        for (int i = GameDataHandler.FloorCount; i >= 1; i--)
        {
            CreateFloorObject(i);
        }
        TriggerEvent(new GameMainEvent(GameMainEvent.GameMainEventType.FloorInit));
    }
    private void CreateFloorObject(int posy)
    {
        GameObject floorClone = LeanPool.Spawn(GameDataHandler.FloorPregab);
        FloorItem floorItem = floorClone.GetComponent<FloorItem>();
        floorItem.OnCreate(posy);
        TriggerEvent(new GameScreenUIEvent(GameScreenUIEvent.GameScreenUIEventType.CreateFloorItem, floorClone));
    }
    public void OnEXEvent(FloorEvent eventType)
    {
        switch (eventType.floorEventtype)
        {
            case FloorEvent.FloorEventtype.Create:
                RegisterNewFloor(eventType.floorItem);
                break;
            case FloorEvent.FloorEventtype.Delete:
                UnRegisterFloor(eventType.floorItem);
                break;
        }
    }
    protected override void Show()
    {
        this.EXEventStartListening<FloorEvent>();
    }
    protected override void Hide()
    {
        this.EXEventStopListening<FloorEvent>();
    }
}
