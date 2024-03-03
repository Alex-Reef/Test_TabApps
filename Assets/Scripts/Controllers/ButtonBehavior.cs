using System;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    [RequireComponent(typeof(Button))]
    public class ButtonBehavior : MonoBehaviour
    {
        public event Action ButtonClicked;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => ButtonClicked?.Invoke());
        }
    }
}