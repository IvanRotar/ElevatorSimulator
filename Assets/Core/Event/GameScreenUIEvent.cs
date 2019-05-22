using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public struct GameScreenUIEvent
    {
        public enum GameScreenUIEventType
        {
            CreateFloorItem
        }
        public GameScreenUIEventType gameScreenUIEventType;
        public GameObject FloorItem;
        public GameScreenUIEvent(GameScreenUIEventType gameScreenUIEventType,GameObject FloorItem)
        {
            this.gameScreenUIEventType = gameScreenUIEventType;
            this.FloorItem = FloorItem;
        }
    }
}