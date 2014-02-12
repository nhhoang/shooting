//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using Unibill;
using Unibill.Impl;
using Uniject;
using Uniject.Impl;
using UnityEngine;

public class UnityModule : NinjectModule {
    
    public override void Load() {
        Bind<ILogger>().To<UnityLogger>();
        Bind<IResourceLoader>().To<UnityResourceLoader>().InSingletonScope();
        Bind<IStorage>().To<UnityPlayerPrefsStorage>().InSingletonScope();
        
        Bind<InventoryDatabase>().ToSelf().InSingletonScope();
        Bind<Unibill.Impl.ProductIdRemapper>().ToSelf().InSingletonScope();
        Bind<Uniject.Editor.IEditorUtil>().To<UnityEditorUtil>().InSingletonScope();

        Bind<Biller> ().ToSelf ().InSingletonScope ();
        Bind<HelpCentre> ().ToSelf ().InSingletonScope ();

        // Resource bindings.
        Bind<IUtil>().To<UnityUtil>().InSingletonScope();
    }
}
