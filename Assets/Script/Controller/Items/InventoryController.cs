using System.Collections;
using Controller;
using Script.Entity.Item;
using Script.Enumeral;
using Script.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Controller.Items
{
    public class InventoryController : RootController
    {
        
        //Public-props
        [Header("Selector")] public GameObject selector;
        [Header("Selectable")] public GameObject selectable;
        [Header("Default Item Distance")] public int adjacentDistance = 500;
        [Header("Default Item Scale")] public Vector3 defaultScale = new Vector3(100, 100, 100);
        [Header("Default Highlight Multiplier")] public float highlightedScaleFactor = 2.5f;
        [Header("Default Highlight Rotation")] public float rotationSpeed = 2.5f;
        [Header("Default Swap Duration")] public float duration = 0.5f;

        //Private-props
        private float _y;
        private float _z;
        private Vector3 _highlightedScale;
        private CircularList<GenericItem> _selectableItemList; 
        public GameObject currentActiveItem;

        //Start
        private void OnEnable()
        {
            var localPosition = selector.transform.localPosition;
            _y = localPosition.y;
            _z = localPosition.z;
            selector.transform.localPosition = new Vector3(0, _y, _z);
            _highlightedScale = defaultScale * highlightedScaleFactor;

            Resume();
        }
        
        //Update
        private void Update()
        {
            if (_selectableItemList != null && _selectableItemList.Count > 0)
                _selectableItemList.Current.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }

        //Public-methods
        public void HandleInventory(InputValue value)
        {
            if (GameStatus.Inventory.Equals(GameController.GetStatus()))
            {
                Resume();
            }
            else if (GameStatus.Play.Equals(GameController.GetStatus()))
            {
                Inventory();
            }
        }
        
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

        //Private-methods
        private void Resume()
        {
            InventoryUI.SetActive(false);
            GameController.SetStatus(GameStatus.Play);
        }

        private void Inventory()
        {
            GameController.SetStatus(GameStatus.Inventory);
            InventoryUI.SetActive(true);
            LoadItems();
        }
        
        private void LoadItems()
        {
            _selectableItemList ??= new CircularList<GenericItem>();
            if (ChiuskyController.Inventory == null || ChiuskyController.Inventory.Count <= 0) return;
            SetActiveItem(ChiuskyController.Inventory.Current.gameObject);
            SpawnAllItems();
        }
        
        private void SetActiveItem(GameObject item)
        {
            if (currentActiveItem != null)
                Destroy(currentActiveItem.gameObject);

            currentActiveItem = Instantiate(item, selectable.transform, true);
            currentActiveItem.gameObject.SetActive(true);
            currentActiveItem.gameObject.layer = 5;
            currentActiveItem.transform.localScale = defaultScale;
            currentActiveItem.transform.localPosition = new Vector3(0, 0, -250);
            currentActiveItem.transform.localRotation = Quaternion.identity;
            currentActiveItem.name = item.name;
        }

        private void SpawnAllItems()
        {
            for (var i = 0; i < ChiuskyController.Inventory.Count; i++)
            {
                var obj = Instantiate(ChiuskyController.Inventory[i].gameObject);
                if (obj is null) continue;
                obj.SetActive(true);
                obj.layer = 5;
                var item = obj.transform;
                item.SetParent(selector.transform);
                item.localScale = defaultScale;
                item.localPosition = new Vector3(i * adjacentDistance, 0, -250);
                item.localRotation = Quaternion.identity;
                item.name = obj.name;
                _selectableItemList.Add(item.gameObject.GetComponent<GenericItem>());
            }
        }
        
        // L E R P
        private IEnumerator Lerp(int direction)
        {
            Debug.Log("lerping ..." + direction);
            float timeElapsed = 0;
            var currentItemPosition = -_selectableItemList.Current.transform.localPosition.x;
            var nextItemPosition = direction > 0 ? -_selectableItemList.Next.transform.localPosition.x : -_selectableItemList.Previous.transform.localPosition.x;

            SetActiveItem(
                direction > 0 ? _selectableItemList.MoveNext.gameObject : _selectableItemList.MovePrevious.gameObject);

            var currentItem = _selectableItemList.Current;
            var previousItem = direction > 0 ? _selectableItemList.Previous : _selectableItemList.Next;

            //il lerping finisce prima dello scadere della durata
            while (timeElapsed < duration)
            {
                Debug.Log(timeElapsed / duration);
                previousItem.transform.localScale = Vector3.Lerp(
                    _highlightedScale,
                    defaultScale,
                    timeElapsed / duration);

                currentItem.transform.localScale = Vector3.Lerp(
                    defaultScale,
                    _highlightedScale,
                    timeElapsed / duration);

                selector.transform.localPosition = Vector3.Lerp(
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