using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class Toast : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private Image backgroundImage;
        
        public void Init(string message, MessageType messageType)
        {
            messageText.text = message;
            Color color = new Color32(0, 0, 0, 0);
            switch (messageType)
            {
                case MessageType.Info:
                    color = Color.white;
                    break;
                case MessageType.Error:
                    color = new Color32(255, 102, 102, 255);
                    break;
                case MessageType.Success:
                    color = new Color32(153, 255, 153, 255);
                    break;
            }

            backgroundImage.color = color;
        }
    }
}