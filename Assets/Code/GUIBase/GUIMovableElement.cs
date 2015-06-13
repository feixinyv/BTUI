using UnityEngine;

namespace FF.BT
{
    public class GUIMovableElement : GUIElement
    {
        private Vector2 _prevMousePoint;
        private bool _isPressed = false;

        public override void Press(bool pressed)
        {
            _isPressed = pressed;
            if (pressed)
            {
                _prevMousePoint = _parentWindow.GetMousePostion();
            }
            base.Press(pressed);
        }

        public override void Draw()
        {
            if (_isPressed)
            {
                var newCenter = _parentWindow.GetMousePostion() - _prevMousePoint + GetCenter();
                SetCenter(newCenter);
            }
            base.Draw();
        }
    }
}
