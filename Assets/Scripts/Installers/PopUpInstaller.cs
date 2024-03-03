using Controllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PopUpInstaller : MonoInstaller
    {
        [SerializeField] private PopUpController target;
    
        public override void InstallBindings()
        {
            Container.Bind<PopUpController>().FromInstance(target).AsSingle().NonLazy();
        }
    }
}