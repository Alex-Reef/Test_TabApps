using Controllers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DataControllerInstaller : MonoInstaller
    {
        [SerializeField] private DataController target;
    
        public override void InstallBindings()
        {
            Container.Bind<DataController>().FromInstance(target).AsSingle().NonLazy();
        }
    }
}