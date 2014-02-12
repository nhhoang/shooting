//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Ninject.Modules;

namespace Unibill.Impl {
    public class GooglePlayModule : Ninject.Modules.NinjectModule {
        
        private bool editor;
        public GooglePlayModule (bool editor) {
            this.editor = editor;
        }
        
        public override void Load () {        
            Bind<IBillingService>().To<GooglePlayBillingService>().InSingletonScope();
            if (editor) {
                Bind<IRawGooglePlayInterface>().To<FakeGooglePlayPlugin>().InSingletonScope();
            } else {
                Bind<IRawGooglePlayInterface>().To<RawGooglePlayInterface>().InSingletonScope();
            }
        }
    }
}
