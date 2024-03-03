using Cysharp.Threading.Tasks;
using DG.Tweening;
using Models;
using UnityEngine;
using Views;

namespace Controllers
{
    public class ToastController : MonoBehaviour
    {
        [SerializeField] private RectTransform targetTransform;
        [SerializeField] private Toast toastPrefab;
        
        private Toast _toast;

        public async UniTaskVoid Show(string message, MessageType messageType)
        {
            if (_toast == null) _toast = Instantiate(toastPrefab, targetTransform);
            
            _toast.Init(message, messageType);
            _toast.gameObject.SetActive(true);
            
            var rect = _toast.gameObject.GetComponent<RectTransform>();
            rect.DOAnchorPosY(-150,  1f).SetEase(Ease.OutBack);
            
            await UniTask.Delay(2000);
            
            rect.DOAnchorPosY(150, 1).SetEase(Ease.OutBack).
                OnComplete(() => _toast.gameObject.SetActive(false));
        }
    }
}