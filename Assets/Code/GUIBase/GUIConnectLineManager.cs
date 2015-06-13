using System.Collections.Generic;

namespace FF.BT
{
    public class GUIConnectLineManager
    {
        private static GUIConnectLineManager _instance = new GUIConnectLineManager();
        public static GUIConnectLineManager Instance {
            get { return _instance; }
        }

        private List<GUIConnectLine> _lines = new List<GUIConnectLine>();
        private GUIWindow _guiWindow;

        private GUIConnectLineManager()
        {

        }

        public void SetWindow(GUIWindow window)
        {
            _guiWindow = window;
        }

        public void AddLine(GUIConnectableElement start, GUIConnectableElement end)
        {
            if (start == null || end == null)
                return;
            for(int i = 0; i < _lines.Count; ++i)
            {
                if (_lines[i].StateElement == start)
                {
                    if (_lines[i].EndElement == end)
                        return;
                    _DeleteLine(i);
                    var newLine = new GUIConnectLine(start, end);
                    _lines.Add(newLine);
                }
            }
        }

        public void DeleteLine(GUIConnectLine line)
        {
            if(line == null)
                return;
            var index = _lines.IndexOf(line);
            _DeleteLine(index);
        }

        private void _DeleteLine(int index)
        {
            if (index < 0 || _lines.Count <= index)
                return;
            _guiWindow.RemoveElement(_lines[index]);
            _lines.RemoveAt(index);
        }

        public void DeleteRelatedLine(GUIConnectableElement e)
        {
            for (int i = _lines.Count; i >= 0; --i)
            {
                if (_lines[i].StateElement == e || _lines[i].EndElement == e)
                {
                    _DeleteLine(i);
                }
            }
        }
    }
}