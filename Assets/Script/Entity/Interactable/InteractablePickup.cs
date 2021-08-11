using Script.Entity.Item;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractablePickup :InteractableGeneric
    {
        public override void DoInteract()
        {
            ChiuskyCtrl.Inventory.Add(this.gameObject.GetComponent<GenericItem>());
            ChiuskyCtrl.SetFocusItem(null);
            this.gameObject.SetActive(false);
            
        }
    }
}