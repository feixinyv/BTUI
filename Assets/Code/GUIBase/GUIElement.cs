using System;
using UnityEngine;
using System.Collections;

namespace FF.BT
{
    public interface IGUIElement
    {
        Vector2 GetSize();
        Vector2 GetCenter();
        void SetSize(Vector2 size);
        void SetCenter(Vector2 center);
        void Draw();
        void Click();
        void Press(bool pressed);
        void Delete();
        bool InRegion(Vector2 point);
        void SetParentWindow(GUIWindow w);

        Action<GUIElement> onClick { get; set; }
        Action<GUIElement, bool> onPress { get; set; }
        Action onDraw { get; set; }
    }

    public class GUIElement : IGUIElement
    {
        public Action<GUIElement> onClick { get; set; }
        public Action<GUIElement, bool> onPress { get; set; }
        public Action onDraw { get; set; }
        
        protected Vector2 _halfSize;
        protected Vector2 _center;
        protected GUIWindow _parentWindow;

        public void SetParentWindow(GUIWindow w)
        {
            _parentWindow = w;
        }

        public virtual Vector2 GetSize()
        {
            return _halfSize * 2;
        }

        public virtual Vector2 GetCenter()
        {
            return _center;
        }

        public virtual void SetSize(Vector2 size)
        {
            _halfSize = size * 0.5f;
            _halfSize.x = Mathf.Max(_halfSize.x, 0);
            _halfSize.y = Mathf.Max(_halfSize.y, 0);
        }

        public virtual void SetCenter(Vector2 center)
        {
            _center = center;
        }

        public virtual void Draw()
        {
            if (onDraw != null)
                onDraw();
        }

        public virtual void Click()
        {
            if (onClick != null)
                onClick(this);
        }

        public virtual void Press(bool pressed)
        {
            if (onPress != null)
                onPress(this, pressed);
        }

        public virtual void Delete()
        {

        }

        public bool InRegion(Vector2 point)
        {
            var dif = point - _center;
            if(dif.x > _halfSize.x || dif.x < -_halfSize.x || dif.y > _halfSize.y || dif.y < -_halfSize.y)
                return false;
            return true;
        }
    }
}