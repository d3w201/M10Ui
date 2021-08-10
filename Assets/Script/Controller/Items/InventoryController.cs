using Controller;
using Script.Enumeral;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Controller.Items
{
    public class InventoryController : RootController
    {
        private GameObject _selectedItem;
        private new void Start()
        {
            base.Start();
            Resume();
        }
        
        private void Resume()
        {
            InventoryUI.SetActive (false);
            GameController.SetStatus(GameStatus.Play);
        }
        
        //Public-methods
        public void HandleInventory(InputValue value)
        {
            if (GameStatus.Inventory.Equals(GameController.GetStatus()))
            {
                Resume();
            }
            else if(GameStatus.Play.Equals(GameController.GetStatus()))
            {
                Inventory();
            }
        }

        public GameObject GetSelectedItem()
        {
            return this._selectedItem;
        }
        
        public void SetSelectedItem(GameObject item)
        {
            this._selectedItem = item;
        }

        private void Inventory()
        {
            Time.timeScale = 1f;
            InventoryUI.SetActive(true);
            SelectItemController.LoadItems();
            GameController.SetStatus(GameStatus.Inventory);
            
        }
    }
}