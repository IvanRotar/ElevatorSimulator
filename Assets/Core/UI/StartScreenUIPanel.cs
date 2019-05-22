using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EX.Tools;

public class StartScreenUIPanel : UIPanel
{
    [SerializeField] private InputField FloorCountInputField;
    [SerializeField] private Button StartGameBtn;
    private void Start()
    {
        FloorCountInputField.onValueChanged.AddListener(ChangeFloorCount);
        StartGameBtn.onClick.AddListener(() => StartGame());
    }
    private void ChangeFloorCount(string val)
    {
        bool NullVall = string.IsNullOrEmpty(val);
        GameDataHandler.FloorCount = NullVall ? 0 : int.Parse(val);
        StartGameBtn.gameObject.SetActive(GameDataHandler.FloorCount > 0);
    }
    private void StartGame()
    {
        TriggerEvent(new GameMainEvent(GameMainEvent.GameMainEventType.GameStart));
    }
}
