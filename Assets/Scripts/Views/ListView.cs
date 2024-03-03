using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using Zenject;

namespace Views
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private RectTransform itemsParentTransform;
        [SerializeField] private ListItem itemPrefab;

        [Inject] private DataController _dataStorage;
        private Dictionary<int, ListItem> _cashedItems;

        private void Awake()
        {
            _cashedItems = new Dictionary<int, ListItem>();
            _dataStorage.Data.CollectionModified += OnItemChanged;
        }

        private void OnItemChanged(int id)
        {
            var data = _dataStorage.Data.GetData(id);
            if (data != null)
            {
                RefreshItem(data).Forget();
            }
            else
            {
                if (_cashedItems.TryGetValue(id, out var item))
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

        private async UniTaskVoid RefreshItem(DataModel dataModel)
        {
            _cashedItems.TryGetValue(dataModel.id, out var listItem);
            if (!listItem)
            {
                listItem = Instantiate(itemPrefab, itemsParentTransform);
                _cashedItems.Add(dataModel.id,  listItem);
            }
            listItem.Init(dataModel);
            await UniTask.Yield();
            listItem.gameObject.SetActive(true);
            listItem.PlayAnimation();
        }
    }
}