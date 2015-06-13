using System;
using UnityEngine;

namespace FF.BT
{
    public class GUIConnectLine : IGUIElement
    {
        private GUIConnectableElement _start;
        public GUIConnectableElement StateElement;
        private GUIConnectableElement _end;
        public GUIConnectableElement EndElement;

        public Action<GUIElement> onClick { get; set; }
        public Action<GUIElement, bool> onPress { get; set; }
        public Action onDraw { get; set; }

        public GUIConnectLine(GUIConnectableElement start, GUIConnectableElement end)
        {
            _start = start;
            _end = end;
        }

        public void SetParentWindow(GUIWindow w)
        {
        }

        public Vector2 GetSize()
        {
            return Vector2.zero;
        }

        public Vector2 GetCenter()
        {
            return Vector2.zero;
        }

        public void SetSize(Vector2 size)
        {
            return;
        }

        public void SetCenter(Vector2 center)
        {
            return;
        }

        public void Draw()
        {
            _DrawConnectLine();
        }

        public void Click()
        {
        }

        public void Press(bool pressed)
        {
        }

        public bool InRegion(Vector2 point)
        {
            return false;
        }

        public void Delete()
        {
            GUIConnectLineManager.Instance.DeleteLine(this);
        }

        private void _DrawConnectLine()
        {
            
        }
    }
}