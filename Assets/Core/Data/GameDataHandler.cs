using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EX.Tools;

namespace EX.Tools
{
    public class GameDataHandler
    {
        public static int FloorCount;
        public static int FirstFloorNumber = 1;
        public static readonly float ElevatorMoveTime = 5;
        public static readonly float ElevatorDoorIdle = 3f;
        public static GameObject FloorPregab
        {
            get
            {
                return GameData.FloorItemPrefab;
            }
        }
        public static GameObject ElevatorComandButtonPrefab
        {
            get
            {
                return GameData.ElevatorComandButton;
            }
        }
        private static GameData gameData;
        public static GameData GameData
        {
            get
            {
                if (gameData == null)
                    LoadData();
                return gameData;
            }
        }
        private static void LoadData()
        {
            gameData = Resources.Load<GameData>("GameData");
        }
    }
}