using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Enumeral;

namespace Entity.Dialog
{
    public class DialogFormat
    {
        //================================================
        //Private Variable
        //================================================
        public string DefaultSize = "60";
        private string _defaultColor = "white";

        private string _color;
        private string _size;


        //================================================
        //Public Method
        //================================================
        public DialogFormat(string defaultSize = "", string defaultColor = "")
        {
            _color = string.Empty;
            _size = string.Empty;

            if (defaultSize != string.Empty) DefaultSize = defaultSize;
            if (defaultColor != string.Empty) _defaultColor = defaultColor;
        }

        public string Color
        {
            set
            {
                if (isColorValid(value))
                {
                    _color = value;
                    if (_size == string.Empty) _size = DefaultSize;
                }
            }

            get => _color;
        }

        public string Size
        {
            set
            {
                if (isSizeValid(value))
                {
                    _size = value;
                    if (_color == string.Empty) _color = _defaultColor;
                }
            }

            get => _size;
        }

        public string OpenTagger
        {
            get
            {
                if (isValid) return $"<color={Color}><size={Size}>";
                else return string.Empty;
            }
        }

        public string CloseTagger
        {
            get
            {
                if (isValid) return "</size></color>";
                else return string.Empty;
            }
        }

        public void Resize(string command)
        {
            if (_size == string.Empty) Size = DefaultSize;

            switch (command)
            {
                case "up":
                    _size = (int.Parse(_size) + 10).ToString();
                    break;

                case "down":
                    _size = (int.Parse(_size) - 10).ToString();
                    break;

                case "init":
                    _size = DefaultSize;
                    break;

                default:
                    _size = command;
                    break;
            }
        }

        //================================================
        //Private Method
        //================================================
        private bool isValid
        {
            get => _color != string.Empty && _size != string.Empty;
        }

        private bool isColorValid(string Color)
        {
            TextColor textColor;
            Regex hexColor = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");

            return Enum.TryParse(Color, out textColor) || hexColor.Match(Color).Success;
        }

        private bool isSizeValid(string Size)
        {
            float size;
            return float.TryParse(Size, out size);
        }

    }

    public class DialogCommand
    {
        public Command Command;
        public string Context;

        public DialogCommand(Command command, string context = "")
        {
            Command = command;
            Context = context;
        }
    }

    public class DialogSelect
    {
        private List<DialogSelectItem> ItemList;

        public DialogSelect()
        {
            ItemList = new List<DialogSelectItem>();
        }

        public int Count
        {
            get => ItemList.Count;
        }

        public DialogSelectItem GetByIndex(int index)
        {
            return ItemList[index];
        }

        public List<DialogSelectItem> Get_List()
        {
            return ItemList;
        }

        public string Get_Value(string Key)
        {
            return ItemList.Find((var) => var.isSameKey(Key)).Value;
        }

        public void Clear()
        {
            ItemList.Clear();
        }

        public void Add(string Key, string Value)
        {
            ItemList.Add(new DialogSelectItem(Key, Value));
        }

        public void Remove(string Key)
        {
            var item = ItemList.Find((var) => var.isSameKey(Key));

            if (item != null) ItemList.Remove(item);
        }
    }

    public class DialogSelectItem
    {
        public string Key;
        public string Value;

        public bool isSameKey(string key)
        {
            return Key == key;
        }

        public DialogSelectItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}