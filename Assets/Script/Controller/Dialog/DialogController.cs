using System.Collections;
using System.Collections.Generic;
using Controller;
using Entity.Dialog;
using Enumeral;
using Script.Enumeral;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Controller.Dialog
{
    public class DialogController : RootController
    {
        //Public-props
        [Header("Game Objects")] public GameObject printer;
        [Header("Text")] public TextMeshProUGUI printerText;
        [Header("Preference")] public float delay = 0.1f;
        [Header("Selector")] public GameObject selector, selectorItem;
        [Header("Text-Button")] public TextMeshProUGUI selectorItemText;

        [HideInInspector] public State state;

        //Private-props
        private DialogData _currentData;
        private float _currentDelay;
        private float _lastDelay;
        private Coroutine _textingRoutine;
        private Coroutine _printingRoutine;

        //Start
        private new void Start()
        {
            base.Start();
            Hide();
        }

        //Public Method
        public void Show(List<DialogData> data)
        {
            StartCoroutine(Activate_List(data));
        }

        public void Click_Window()
        {
            switch (state)
            {
                case State.Active:
                    StartCoroutine(_skip());
                    break;

                case State.Wait:
                    if (_currentData.SelectList.Count <= 0) Hide();
                    break;
            }
        }

        public void Select(int index)
        {
            Hide();
        }

        #region Speed

        public void Set_Speed(string speed)
        {
            switch (speed)
            {
                case "up":
                    _currentDelay -= 0.25f;
                    if (_currentDelay <= 0) _currentDelay = 0.001f;
                    break;

                case "down":
                    _currentDelay += 0.25f;
                    break;

                case "init":
                    _currentDelay = delay;
                    break;

                default:
                    _currentDelay = float.Parse(speed);
                    break;
            }

            _lastDelay = _currentDelay;
        }

        #endregion

        //Private-methods
        private void Show(DialogData data)
        {
            _currentData = data;
            _textingRoutine = StartCoroutine(Activate());
        }

        private void _initialize()
        {
            _currentDelay = delay;
            _lastDelay = 0.1f;
            printerText.text = string.Empty;
            printer.SetActive(true);
        }

        private void _init_selector()
        {
            _clear_selector();

            if (_currentData.SelectList.Count > 0)
            {
                selector.SetActive(true);

                for (var i = 0; i < _currentData.SelectList.Count; i++)
                {
                    _add_selectorItem(i);
                }
            }

            else selector.SetActive(false);
        }

        private void _clear_selector()
        {
            for (var i = 1; i < selector.transform.childCount; i++)
            {
                Destroy(selector.transform.GetChild(i).gameObject);
            }
        }

        // add-buttons
        private void _add_selectorItem(int index)
        {
            selectorItemText.text = _currentData.SelectList.GetByIndex(index).Value;

            var newItem = Instantiate(selectorItem, selector.transform);
            newItem.GetComponent<Button>().onClick.AddListener(() => Select(index));
            newItem.SetActive(true);
            if(!EventSystem.currentSelectedGameObject) EventSystem.SetSelectedGameObject(newItem);
            
        }

        private void Hide()
        {
            if (_textingRoutine != null)
                StopCoroutine(_textingRoutine);

            if (_printingRoutine != null)
                StopCoroutine(_printingRoutine);

            printer.SetActive(false);
            selector.SetActive(false);

            state = State.Deactivate;

            EventSystem.SetSelectedGameObject(null);
            GameController.SetStatus(GameStatus.Play);

            if (_currentData?.Callback != null)
            {
                _currentData.Callback.Invoke();
                _currentData.Callback = null;
            }
        }

        #region Show Text

        private IEnumerator Activate_List(List<DialogData> dataList)
        {
            state = State.Active;

            foreach (var data in dataList)
            {
                Show(data);

                while (state != State.Deactivate)
                {
                    yield return null;
                }
            }
        }

        private IEnumerator Activate()
        {
            _initialize();

            state = State.Active;

            foreach (var item in _currentData.Commands)
            {
                switch (item.Command)
                {
                    case Command.print:
                        yield return _printingRoutine = StartCoroutine(_print(item.Context));
                        break;

                    case Command.color:
                        _currentData.Format.Color = item.Context;
                        break;

                    case Command.size:
                        _currentData.Format.Resize(item.Context);
                        break;

                    case Command.speed:
                        Set_Speed(item.Context);
                        break;

                    case Command.click:
                        yield return _waitInput();
                        break;

                    case Command.close:
                        Hide();
                        yield break;

                    case Command.wait:
                        yield return new WaitForSeconds(float.Parse(item.Context));
                        break;
                    default:
                        break;
                }
            }
            state = State.Wait;
            _init_selector();
        }

        #region Commands

        //print
        private IEnumerator _print(string text)
        {
            _currentData.PrintText += _currentData.Format.OpenTagger;

            foreach (var t in text)
            {
                _currentData.PrintText += t;
                printerText.text = _currentData.PrintText + _currentData.Format.CloseTagger;

                if (_currentDelay != 0) yield return new WaitForSeconds(_currentDelay);
            }

            _currentData.PrintText += _currentData.Format.CloseTagger;
        }

        //click
        private IEnumerator _waitInput()
        {
            while (!UnityEngine.Input.GetMouseButtonDown(0)) yield return null;
            _currentDelay = _lastDelay;
        }

        #endregion

        private IEnumerator _skip()
        {
            if (_currentData.isSkippable)
            {
                _currentDelay = 0;
                while (state != State.Wait) yield return null;
                _currentDelay = delay;
            }
        }

        #endregion
    }
}