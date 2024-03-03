using System;
using System.Collections.Generic;

namespace Models
{
    public class DataStorage
    {
        private Dictionary<int, DataModel> _storageData = new Dictionary<int, DataModel>();

        public event Action<int> CollectionModified;

        public void AddData(DataModel dataModel)
        {
            _storageData.TryAdd(dataModel.id, dataModel);
            CollectionModified?.Invoke(dataModel.id);
        }

        public void AddData(DataModel[] dataModels)
        {
            foreach (var dataModel in dataModels)
            {
                AddData(dataModel);
            }
        }

        public void RemoveData(int id)
        {
            _storageData.Remove(id);
            CollectionModified?.Invoke(id);
        }

        public DataModel GetData(int id)
        {
            _storageData.TryGetValue(id, out var data);
            return data;
        }

        public void UpdateData(DataModel dataModel)
        {
            if (_storageData.ContainsKey(dataModel.id))
            {
                _storageData[dataModel.id] = dataModel;
                CollectionModified?.Invoke(dataModel.id);
            }
        }
    }
}