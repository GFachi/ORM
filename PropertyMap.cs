using System;

namespace ORM.UI
{
    public class PropertyMap
    {
        private bool _Key;
        public bool Key
        {
            get { return _Key; }
            set
            {
                _Key = value;
            }
        }
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
            }
        }

        private String _ControlName;
        public String ControlName
        {
            get { return _ControlName; }
            set
            {
                _ControlName = value;
            }
        }

        private String _AspControlName;
        public String AspControlName
        {
            get { return _AspControlName; }
            set
            {
                _AspControlName = value;
            }
        }

        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set
            {
                _Caption = value;
            }
        }

        private string _ControlPrefix;
        public string ControlPrefix
        {
            get { return _ControlPrefix; }
            set
            {
                _ControlPrefix = value;
            }
        }

        private string _AspControlPrefix;
        public string AspControlPrefix
        {
            get { return _AspControlPrefix; }
            set
            {
                _AspControlPrefix = value;
            }
        }

        private object _ControlType;
        public object ControlType
        {
            get { return _ControlType; }
            set
            {
                _ControlType = value;
            }
        }

        private string _AspControlType;
        public string AspControlType
        {
            get { return _AspControlType; }
            set
            {
                _AspControlType = value;
            }
        }

        private string _EditField;
        public string EditField
        {
            get { return _EditField; }
            set
            {
                _EditField = value;
            }
        }

        private string _AspEditField;
        public string AspEditField
        {
            get { return _AspEditField; }
            set
            {
                _AspEditField = value;
            }
        }

        private bool _Required;
        public bool Required
        {
            get { return _Required; }
            set
            {
                _Required = value;
            }
        }
        
    }
}
