using Script.Entity.Item;
using UnityEngine;

namespace Script.Entity.Interactable
{
    public class InteractablePickup :InteractableGeneric
    {
        public override void DoInteract()
        {
            Debug.Log(this.gameObject.name);
            ChiuskyCtrl.Inventory.Add(this.gameObject.GetComponent<GenericItem>());
            base.DoInteract();
            this.gameObject.SetActive(false);
        }
    }
}