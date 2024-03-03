using System;
using UnityEngine;
using Views;

namespace Controllers
{
    public class PopUpController : MonoBehaviour
    {
        [SerializeField] private RectTransform targetTransform;
        [SerializeField] private PopUp popUpPrefab;
        
        private PopUp _popUp;

        public void Show(string message, Action<string> onSubmit)
        {
            if (_popUp == null) _popUp = Instantiate(popUpPrefab, targetTransform);
            _popUp.Init(message, onSubmit);
            _popUp.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if(_popUp != null) _popUp.gameObject.SetActive(false);
        }
    }
}