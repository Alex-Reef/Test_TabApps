using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] private Button submitButton, cancelButton;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text messageText;
        
        public void Init(string message, Action<string> onSubmit)
        {
            messageText.text = message;
            inputField.text = "";
            
            submitButton.onClick.RemoveAllListeners();
            submitButton.onClick.AddListener(() =>
            {
                onSubmit?.Invoke(inputField.text);
                gameObject.SetActive(false);
            });
            
            cancelButton.onClick.AddListener(() => gameObject.SetActive(false));
        }
    }
}