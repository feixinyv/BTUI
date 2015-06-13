using System;
using UnityEngine;
using System.Collections;

namespace FF.BT
{
    public class GUIConnectableElement : IGUIElement
    {
        private GUIElement _element;
        private GUIWindow _parentWindow;
        private static float _heightPercent = 0.1f;
        private static GUIConnectableElement _mouseDownConnectableElement;

        public Action<GUIElement> onClick 
        {
            get { return _element.onClick; }
            set { _element.onClick = value; }
        }
        public Action<GUIElement, bool> onPress
        {
            get { return _element.onPress; }
            set { _element.onPress = value; }
        }
        public Action onDraw
        {
            get { return _element.onDraw; }
            set { _element.onDraw = value; }
        }

        public GUIConnectableElement(GUIElement e)
        {
            _element = e;
        }

        public void SetParentWindow(GUIWindow w)
        {
            _parentWindow = w;
        }

        public Vector2 GetSize()
        {
            var baseSize = _element.GetSize();
            baseSize.y += baseSize.y*_heightPercent;
            return baseSize;
        }

        public Vector2 GetCenter()
        {
            var center = _element.GetCenter();
            var baseSize = _element.GetSize();
            center.y += baseSize.y*_heightPercent*0.5f;
            return center;
        }

        public void SetSize(Vector2 size)
        {
            size.y /= (1 + _heightPercent);
            _element.SetSize(size);
        }

        public void SetCenter(Vector2 center)
        {
            var baseSize = _element.GetSize();
            center.y -= baseSize.y * _heightPercent * 0.5f;
            _element.SetCenter(center);
        }

        public void Draw()
        {
            _element.Draw();
            _DrawConnectableRegion();
        }

        public void Click()
        {
            var pos = _parentWindow.GetMousePostion();
            if (_element.InRegion(pos))
            {
                _element.Click();
            }
        }

        public void Press(bool pressed)
        {
            var pos = _parentWindow.GetMousePostion();
            if (_element.InRegion(pos))
            {
                _element.Press(pressed);
            }
            else
            {
                if (!pressed && _parentWindow.MouseDownElement is GUIConnectableElement)
                {
                    GUIConnectLineManager.Instance.AddLine(_parentWindow.MouseDownElement as GUIConnectableElement, this);
                }
            }
        }

        public bool InRegion(Vector2 point)
        {
            var dif = point - GetCenter();
            var halfSize = GetSize()*0.5f;
            if (dif.x > halfSize.x || dif.x < -halfSize.x || dif.y > halfSize.y || dif.y < -halfSize.y)
                return false;
            return true;
        }

        public virtual void Delete()
        {
            GUIConnectLineManager.Instance.DeleteRelatedLine(this);
        }

        private void _DrawConnectableRegion()
        {

        }
    }
}

