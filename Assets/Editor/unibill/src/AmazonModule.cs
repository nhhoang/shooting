//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Ninject.Modules;

namespace Unibill.Impl {
    public class AmazonModule : Ninject.Modules.NinjectModule {
        
        private bool editor;
        public AmazonModule (bool editor) {
            this.editor = editor;
        }
        
        public override void Load () {
            Rebind<IBillingService>().To<AmazonAppStoreBillingService>().InSingletonScope();
            if (editor) {
                Bind<IRawAmazonAppStoreBillingInterface>().To<FakeRawAmazonAppStoreBillingInterface>().InSingletonScope();
            } else {
                Bind<IRawAmazonAppStoreBillingInterface>().To<RawAmazonAppStoreBillingInterface>().InSingletonScope();
            }
        }
    }
}
