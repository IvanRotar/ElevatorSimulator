using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public struct GameMainEvent
    {
        public enum GameMainEventType
        {
            GameStart, GamePause, GameEnd,FloorInit
        }
        public GameMainEventType gameMainEventType;
        public GameMainEvent(GameMainEventType gameMainEventType)
        {
            this.gameMainEventType = gameMainEventType;
        }
    }
}