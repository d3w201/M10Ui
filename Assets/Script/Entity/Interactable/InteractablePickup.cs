using Script.Entity.Item;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractablePickup :InteractableGeneric
    {
        public override void DoInteract()
        {
            ChiuskyCtrl.Inventory.Add(this.gameObject.GetComponent<GenericItem>());
            this.gameObject.SetActive(false);
        }
    }
}