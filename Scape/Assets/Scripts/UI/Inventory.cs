using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{

    public class Inventory : MonoBehaviour
    {

        [SerializeField] GameObject _mainObject;
        [SerializeField] GameObject _itemBar;
        [SerializeField] List<ItemView> _itemsViews;

        private Queue<ItemView> _itemsViewsQueue;
        private Dictionary<CollectableItem, ItemView> _collectedItems = new();
        private ItemView _selectedItem;

        private void Awake()
        {
            InventoryManager.onAddItem.Insert(AddItem, this, 0);
            InventoryManager.onRemoveItem.Insert(RemoveItem, this, 0);
            InventoryManager.onSelectItem.Insert(SelectItem, this, 0);

            _itemsViewsQueue = new Queue<ItemView>(_itemsViews);

            _itemBar.SetActive(false);
            _itemsViews.ForEach(item =>
            {
                item.Clear();
                item.gameObject.SetActive(false);
            });
        }

        private void AddItem(CollectableItem item)
        {

            var itemView = _itemsViewsQueue.Dequeue();

            var itemIndex = _collectedItems.Count + 1;

            //Get Item Data
            //itemView.icon = data.icon;
            itemView.indicator.text = itemIndex.ToString();
            if(itemIndex == 1)
            {
                _selectedItem = itemView;
                itemView.SetSelected(true);
                _itemBar.SetActive(true);
            }
            _itemsViewsQueue.Enqueue(itemView);
            itemView.transform.SetAsLastSibling();
            itemView.gameObject.SetActive(true);

            _collectedItems.Add(item, itemView);
        }

        private void RemoveItem(CollectableItem item)
        {
            var itemView = _collectedItems[item];
            itemView.Clear();
            itemView.gameObject.SetActive(false);
            itemView.transform.SetAsFirstSibling();

            _collectedItems.Remove(item);
        }

        private void SelectItem(CollectableItem item)
        {
            _selectedItem.SetSelected(false);
            _selectedItem = _collectedItems[item];
            _selectedItem.SetSelected(true);
        }

    }

}