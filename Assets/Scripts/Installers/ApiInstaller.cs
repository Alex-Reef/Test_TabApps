using UnityEngine;
using Utils;
using Zenject;

namespace Installers
{
    public class ApiInstaller : MonoInstaller
    {
        [SerializeField] private API target;
    
        public override void InstallBindings()
        {
            Container.Bind<API>().FromInstance(target).AsSingle().NonLazy();
        }
    }
}