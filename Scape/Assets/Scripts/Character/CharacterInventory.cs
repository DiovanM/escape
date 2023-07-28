using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterInventory : MonoBehaviour
{

    [SerializeField] private Transform itemHolder;
    [SerializeField] private Transform hiddenPosition;
    [SerializeField] private Transform holdPosition;
    [SerializeField] private float moveDuration;
    
    private CollectableItem _selectedItem;

    private void Awake()
    {
        InventoryManager.onSelectItem.Insert(OnSelectItem, this);
    }

    private void OnSelectItem(CollectableItem item)
    {
        if (_selectedItem != item && _selectedItem != null)
        {
            _selectedItem.transform.DOLocalMove(hiddenPosition.localPosition, moveDuration).OnComplete(() =>
            {
                _selectedItem.gameObject.SetActive(false);
                _selectedItem = item;
            });

            var position = holdPosition.localPosition + item.itemSO.holdPositionOffset;
            var rotation = holdPosition.localRotation * item.itemSO.holdRotation;

            item.transform.DOLocalMove(position, moveDuration);
            item.transform.DOLocalRotate(rotation.eulerAngles, moveDuration);

            item.gameObject.SetActive(true);
        }
        else
        {
            _selectedItem = item;
        }
    }

    public void OnInteract(InteractableBase interactable)
    {
        if (interactable.TryGetComponent(out CollectableItem item))
        {
            item.Collect();
            item.transform.SetParent(itemHolder);

            if(_selectedItem == null)
            {
                var position = holdPosition.localPosition + item.itemSO.holdPositionOffset;
                var rotation = holdPosition.localRotation * item.itemSO.holdRotation;

                item.transform.DOLocalMove(position, moveDuration);
                item.transform.DOLocalRotate(rotation.eulerAngles, moveDuration);
            }
            else
            {
                item.transform.DOLocalMove(hiddenPosition.localPosition, moveDuration).OnComplete(() =>
                {
                    item.gameObject.SetActive(false);
                });
            }

            InventoryManager.AddItem(item);
        }
        else if (_selectedItem != null)
        {
            if (interactable.TryGetComponent(out KeyItemReceptacle receptacle))
            {
                if (receptacle.TryUseItem(_selectedItem))
                {
                    InventoryManager.RemoveItem(_selectedItem);
                }
            }
        }
    }

}
