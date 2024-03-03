using Controllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ToastInstaller : MonoInstaller
    {
        [SerializeField] private ToastController target;
    
        public override void InstallBindings()
        {
            Container.Bind<ToastController>().FromInstance(target).AsSingle().NonLazy();
        }
    }
}