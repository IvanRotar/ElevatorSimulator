using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EX.Tools
{
    public struct UIEvent
    {
        public enum UIEventType
        {
            OpenPanel, ClosePanel, CloseAllPanel
        }
        public UIEventType uIEventType;
        public string PanelName;
        public bool CloseAll;
        public UIEvent(UIEventType uIEventType,string PanelName, bool CloseAll = true)
        {
            this.uIEventType = uIEventType;
            this.PanelName = PanelName;
            this.CloseAll = CloseAll;
        }
        public UIEvent(UIEventType uIEventType, string PanelName)
        {
            this.uIEventType = uIEventType;
            this.PanelName = PanelName;
            this.CloseAll = true;
        }
        public UIEvent(UIEventType uIEventType)
        {
            this.uIEventType = uIEventType;
            this.PanelName = string.Empty;
            this.CloseAll = true;
        }
    }
}