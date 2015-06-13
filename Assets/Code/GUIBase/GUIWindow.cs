using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace FF.BT
{
    public class GUIWindow
    {
        private List<IGUIElement> _elements = new List<IGUIElement>();
        private List<IGUIElement> _needAdd = new List<IGUIElement>();
        private List<IGUIElement> _needDelete = new List<IGUIElement>();
        private bool _ticking = false;
        private Vector2 _currentMousePos;
        private Vector2 _prevMouseDownPos;
        private IGUIElement _selectedElement;
        private bool _mouseStayStill = false;

        public IGUIElement MouseDownElement;

        public void Tick()
        {
            _currentMousePos = Input.mousePosition;
            bool mouseDown = Input.GetMouseButtonDown(0);
            if (mouseDown)
            {
                _mouseStayStill = true;
                _prevMouseDownPos = _currentMousePos;
                _selectedElement = null;
            }
            bool mouseUp = Input.GetMouseButtonUp(0);
            if (Input.GetMouseButton(0))
            {
                if((_currentMousePos - _prevMouseDownPos).sqrMagnitude > 10)
                    _mouseStayStill = false;
            }
            bool deleteKey = Input.GetKeyUp("Delete");

            _ticking = true;
            if (mouseDown || mouseUp)
            {
                foreach (var element in _elements)
                {
                    if (element.InRegion(_currentMousePos))
                    {
                        if (mouseDown) element.Press(true);
                        if (mouseUp)
                        {
                            element.Press(false);
                            if (_mouseStayStill)
                            {
                                element.Click();
                                _selectedElement = element;
                            }
                        }
                    }
                }
            }
            else if(deleteKey && _selectedElement != null)
            {
                if (EditorUtility.DisplayDialog("Warning!", "Are you SURE for this?", "yes,Let me zuo die", "no"))
                {
                    _selectedElement.Delete();
                    RemoveElement(_selectedElement);
                    _selectedElement = null;
                }
            }
            _ticking = false;

            foreach (var element in _needDelete)
            {
                _elements.Remove(element);
            }
            foreach (var element in _needAdd)
            {
                _elements.Add(element);
            }

            foreach (var element in _elements)
            {
                element.Draw();
            }
            
        }

        public Vector2 GetMousePostion()
        {
            return _currentMousePos;
        }

        public void AddElement(IGUIElement e)
        {
            if (_ticking)
            {
                _needAdd.Add(e);
            }
            else
                _elements.Add(e);
        }

        public void RemoveElement(IGUIElement e)
        {
            if (_ticking)
            {
                if (_needAdd.Contains(e))
                {
                    _needAdd.Remove(e);
                }
                else
                {
                    _needDelete.Add(e);
                }
            }
            else
                _elements.Remove(e);
        }
    }
}