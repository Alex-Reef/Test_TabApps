using UnityEngine;
using Zenject;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private ButtonBehavior[] menuButtons;

        [Inject] private DataController _dataController;
        
        private void Awake()
        {
            for (int i = 0; i < menuButtons.Length; i++)
            {
                int id = i;
                var button = menuButtons[id];
                button.ButtonClicked += () => OnMenuItemSelected(id);
            }
        }

        private void OnMenuItemSelected(int id)
        {
            _dataController.RunCommand(id);
        }
    }
}