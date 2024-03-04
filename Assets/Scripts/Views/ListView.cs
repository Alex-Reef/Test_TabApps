using System.Collections.Generic;
using Controllers;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using Utils;
using Zenject;

namespace Views
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private RectTransform itemsParentTransform;
        [SerializeField] private ListItem itemPrefab;

        [Inject] private DataController _dataStorage;
        private Dictionary<int, ListItem> _cashedItems;

        private Pool<ListItem> _pool;

        private void Awake()
        {
            _pool = new Pool<ListItem>();
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
                if (_cashedItems.Remove(id, out var item))
                {
                    _pool.AddToPool(item);
                    item.gameObject.SetActive(false);
                }
            }
        }

        private async UniTaskVoid RefreshItem(DataModel dataModel)
        {
            _cashedItems.TryGetValue(dataModel.id, out var listItem);
            if (!listItem)
            {
                if (!_pool.TryGet(out listItem))
                {
                    listItem = Instantiate(itemPrefab, itemsParentTransform);
                }
                _cashedItems.Add(dataModel.id,  listItem);
            }
            listItem.Init(dataModel);
            await UniTask.Yield();
            listItem.gameObject.SetActive(true);
            listItem.PlayAnimation();
        }
    }
}