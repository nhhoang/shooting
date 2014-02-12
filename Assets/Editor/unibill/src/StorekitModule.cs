//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Ninject.Modules;

namespace Unibill.Impl {
    public class StorekitModule : Ninject.Modules.NinjectModule {

        private bool editor;
        public StorekitModule (bool editor) {
            this.editor = editor;
        }
        
        public override void Load () {
            Rebind<IBillingService>().To<AppleAppStoreBillingService>().InSingletonScope();
            
            if (editor) {
                Bind<IStoreKitPlugin>().To<FakeStorekitPlugin>().InSingletonScope();
            } else {
                Bind<IStoreKitPlugin>().To<StoreKitPluginImpl>().InSingletonScope();
            }
        }
    }
}
