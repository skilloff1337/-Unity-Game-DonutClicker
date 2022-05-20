using _0._Localization.Scripts;
using _0._Localization.Scripts.Repository;
using _1._Logs.Scripts;
using _1._Logs.Scripts.Interfaces;
using _11._Shop.Scripts;
using _3._UI.Scripts;
using _3._UI.Scripts.Interfaces;
using _4._Donuts.Scripts;
using _4._Donuts.Scripts.Interfaces;
using _5._DataBase.Data;
using _5._DataBase.Interfaces;
using _5._DataBase.Scripts;
using _7._Level.Scripts;
using _8._Tooltips.Interfaces;
using _8._Tooltips.Scripts;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRepository();
        }

        private void BindRepository()
        {
            Container
                .Bind<IRepository>()
                .To<MongoRepository>()
                .AsSingle();
        }  
    }
}