using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public struct FloorEvent
    {
        public enum FloorEventtype
        {
            Create,Delete
        }
        public FloorEventtype floorEventtype;
        public FloorItem floorItem;
        public FloorEvent(FloorEventtype floorEventtype, FloorItem floorItem)
        {
            this.floorEventtype = floorEventtype;
            this.floorItem = floorItem;
        }
    }
}