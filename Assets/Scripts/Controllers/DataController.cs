using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using UnityEngine;
using Utils;
using Zenject;

namespace Controllers
{
    public class DataController : MonoBehaviour
    {
        [Inject] private PopUpController _popUpController;
        [Inject] private ToastController _toastController;
        [Inject] private API _api;
        
        private List<Action> _commandActions;

        public DataStorage Data { get; private set; }

        private void Awake()
        {
            Data = new DataStorage();
            _commandActions = new List<Action>() { Create, Delete, UpdateData, Refresh };
        }

        public void RunCommand(int commandId)
        {
            _commandActions[commandId]?.Invoke();
        }

        private void Create()
        {
            var data = new DataModel().Generate();
            CreateAsync(data).Forget();
        }

        private async UniTaskVoid CreateAsync(DataModel dataModel)
        {
            var responce = await _api.PostAsync(DataFormatter.Serialize(dataModel));
            try
            {
                dataModel = DataFormatter.Deserialize<DataModel>(responce);
                if (dataModel != null)
                {
                    if(Data == null) Debug.Log("Data");
                    Data.AddData(dataModel);
                    _toastController.Show("Button added.", MessageType.Success).Forget();
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private void Delete()
        {
            _popUpController.Show("Enter id for delete", result =>
            {
                TryParseId(result, out var id);
                DeleteAsync(id).Forget();
            });
        }

        private async UniTaskVoid DeleteAsync(int id)
        {
            if (id <= -1)
            {
                _toastController.Show("Empty or invalid id.", MessageType.Error).Forget();
                return;
            }
            var data = await _api.GetAsync(id);
            if (DataFormatter.Deserialize<DataModel>(data) != null)
            {
                await _api.DeleteAsync(id);
                Data.RemoveData(id);
                _toastController.Show("Button deleted.", MessageType.Success).Forget();
            }
            else
            {
                _toastController.Show("Button not found.", MessageType.Success).Forget();
            }
        }

        private void Refresh()
        {
            _popUpController.Show("Enter id for refresh", result =>
            {
                TryParseId(result, out var id);
                RefreshAsync(id).Forget();
            });
        }

        private async UniTaskVoid RefreshAsync(int id)
        {
            var responce = await _api.GetAsync(id);
            var itemsData = new List<DataModel>();
            if (id > -1)
            {
                var data = DataFormatter.Deserialize<DataModel>(responce);
                if (data != null) itemsData.Add(data);
                else _toastController.Show("Invalid id", MessageType.Error).Forget();
            }
            else
            {
                itemsData.AddRange(DataFormatter.Deserialize<DataModel[]>(responce));
            }
            
            Data.AddData(itemsData.ToArray());
            _toastController.Show("Button refreshed.", MessageType.Info).Forget();
        }

        private void UpdateData()
        {
            _popUpController.Show("Enter id for update", result =>
            {
                TryParseId(result, out var id);
                if(id > -1) UpdateDataAsync(id).Forget();
            });
        }

        private async UniTaskVoid UpdateDataAsync(int id)
        {
            var responce = await _api.GetAsync(id);
            var data = DataFormatter.Deserialize<DataModel>(responce);
            if (data != null)
            {
                data.Generate();
                responce = await _api.PutAsync(data.id, DataFormatter.Serialize(data));
                data = DataFormatter.Deserialize<DataModel>(responce);
                Data.UpdateData(data);
                _toastController.Show("Button updated.", MessageType.Success).Forget();
            }
        }
        
        private void TryParseId(string text, out int result)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Int32.TryParse(text, out result);
            }
            else
            {
                result = -1;
            }
        }
    }
}