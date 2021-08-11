using System.Collections;
using Controller;
using Script.Utils;
using UnityEngine;

namespace Script.Controller.Items
{
    public class SelectItemController : RootController
    {
        #region Props

        private float _y;
        private float _z;
        private Transform _parent;
        private Transform _activeCharacterParent;
        public GameObject currentActiveItem;
        public int adjacentdistance = 1000;
        public float duration = 0.5f;
        public int rotationspeed = 50;
        public float highlightedscalefactor = 3;
        public Vector3 defaultscale = new Vector3(100, 100, 100);
        Vector3 _highlightedscale;
        private CircularList<Transform> _selectableItemList; 

        #endregion

        #region Init

        //Init
        private void Start()
        {
            SetupReferences();
            
            _highlightedscale = defaultscale * highlightedscalefactor;
            var localPosition = _parent.localPosition;
            _y = localPosition.y;
            _z = localPosition.z;
            _parent.localPosition = new Vector3(0, _y, _z);
        }

        private void SetupReferences()
        {
            var gameObjectArray = GetComponentsInChildren<Transform>(true);
            foreach (var go in gameObjectArray)
                switch (go.name)
                {
                    case "Parent":
                        _parent = go;
                        break;
                    case "CurrentActivePlayerParent":
                        _activeCharacterParent = go;
                        break;
                }
        }

        public void LoadItems()
        {
            if (_selectableItemList == null)
            {
                _selectableItemList = new CircularList<Transform>();
            }
            if (ChiuskyController.Inventory != null && ChiuskyController.Inventory.Count > 0)
            {
                SetActiveItem(ChiuskyController.Inventory.Current.gameObject);
                SpawnAllItems();
            }
        }

        private void SpawnAllItems()
        {
            for (var i = 0; i < ChiuskyController.Inventory.Count; i++)
            {
                var obj = Instantiate(ChiuskyController.Inventory[i].gameObject);
                if (obj is null) continue;
                obj.SetActive(true);
                obj.layer = 5;
                var character = obj.transform;
                character.SetParent(_parent);
                character.localScale = defaultscale;
                character.localPosition = new Vector3(i * adjacentdistance, 0, -250);
                character.localRotation = Quaternion.identity;
                character.name = "Character " + (i + 1);
                _selectableItemList.Add(character);
            }
        }

        private void SetActiveItem(GameObject item)
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem.gameObject);

            currentActiveItem = Instantiate(item, _activeCharacterParent, true);
            currentActiveItem.gameObject.SetActive(true);
            currentActiveItem.gameObject.layer = 5;
            currentActiveItem.transform.localScale = defaultscale;
            currentActiveItem.transform.localPosition = new Vector3(0, 0, -250);
            currentActiveItem.transform.localRotation = Quaternion.identity;
            currentActiveItem.name = item.name;
        }

        private void Update()
        {
            if (_selectableItemList != null && _selectableItemList.Count > 0)
                _selectableItemList.Current.Rotate(Vector3.up, Time.deltaTime * rotationspeed);
        }

        #endregion
        
        public void HandleMove()
        {
            if (InputController.move.Equals(new Vector2(1, 0)))
            {
                //right.
                StartCoroutine("Lerp", 1);
            }
            else if (InputController.move.Equals(new Vector2(-1, 0)))
            {
                //left.
                StartCoroutine("Lerp", -1);
            }
        }

        // L E R P

        private IEnumerator Lerp(int direction)
        {
            Debug.Log("lerping ..." + direction);
            float timeElapsed = 0;
            var currentItemPosition = -_selectableItemList.Current.localPosition.x;
            var nextItemPosition = direction > 0 ? -_selectableItemList.Next.localPosition.x : -_selectableItemList.Previous.localPosition.x;

            SetActiveItem(
                direction > 0 ? _selectableItemList.MoveNext.gameObject : _selectableItemList.MovePrevious.gameObject);

            var currentItem = _selectableItemList.Current;
            var previousItem = direction > 0 ? _selectableItemList.Previous : _selectableItemList.Next;

            //il lerping finisce prima dello scadere della durata
            while (timeElapsed < duration)
            {
                Debug.Log(timeElapsed / duration);
                previousItem.localScale = Vector3.Lerp(
                    _highlightedscale,
                    defaultscale,
                    timeElapsed / duration);

                currentItem.localScale = Vector3.Lerp(
                    defaultscale,
                    _highlightedscale,
                    timeElapsed / duration);

                _parent.localPosition = Vector3.Lerp(
                    new Vector3(currentItemPosition, _y,_z),
                    new Vector3(nextItemPosition, _y, _z),
                    timeElapsed / duration);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            Debug.Log("end lerping ..." + direction);
        }
    }
}