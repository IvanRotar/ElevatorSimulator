using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using EX.Tools;

public class UIManager : EXBehaviour, EXEventListener<UIEvent>
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) { _instance = FindObjectOfType<UIManager>(); }
            return _instance;
        }
    }
    public List<UIPanel> AllUIPanel;
    private void Start()
    {
        TriggerEvent(new UIEvent(UIEvent.UIEventType.OpenPanel, "StartScreenUIPanel"));
    }
    protected void CloseAllPanel()
    {
        foreach (var panel in AllUIPanel) panel.IsOpen = false;
    }
    public void OpenPanel(string PanelType, bool OnCloseAllPanel = true)
    {
        if (OnCloseAllPanel)
            TriggerEvent(new UIEvent(UIEvent.UIEventType.CloseAllPanel));
        AllUIPanel.FirstOrDefault(panel => panel.GetComponent(PanelType) != null).IsOpen = true;
    }
    public void ClosePanel(string PanelType)
    {
        AllUIPanel.FirstOrDefault(panel => panel.GetComponent(PanelType) != null).IsOpen = false;
    }
    public override void OnEXEvent(GameMainEvent eventType)
    {
    }
    public void OnEXEvent(UIEvent eventType)
    {
        switch (eventType.uIEventType)
        {
            case UIEvent.UIEventType.OpenPanel:
                OpenPanel(eventType.PanelName, eventType.CloseAll);
                break;
            case UIEvent.UIEventType.CloseAllPanel:
                CloseAllPanel();
                break;
            case UIEvent.UIEventType.ClosePanel:
                ClosePanel(eventType.PanelName);
                break;
        }
    }
    protected override void Show()
    {
        this.EXEventStartListening<UIEvent>();
    }
    protected override void Hide()
    {
        this.EXEventStopListening<GameMainEvent>();
    }
}
