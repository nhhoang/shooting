//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Ninject;
using Ninject.Modules;
using Ninject.Injection;
using UnityEngine;
using Uniject.Impl;
using Unibill.Impl;

public class UnityInjector
{
    private static IKernel kernel;
    
    public static IKernel get() {
        if (null == kernel) {
            kernel = _get ();            
        }
        return kernel;
    }

    public static IKernel _get () {
        BillingPlatform platform = new UnibillConfiguration(new UnityResourceLoader(), new UnibillXmlParser(new Mono.Xml.SmallXmlParser(), new UnityResourceLoader()), new UnityUtil(), new UnityLogger()).CurrentPlatform;
        NinjectModule module = null;
        switch (platform) {
        case BillingPlatform.AppleAppStore:
		case BillingPlatform.MacAppStore:
            module = new StorekitModule(Application.isEditor);
        break;
        case BillingPlatform.AmazonAppstore:
            module = new AmazonModule(Application.isEditor);
            break;
        case BillingPlatform.GooglePlay:
            module = new GooglePlayModule(Application.isEditor);
            break;
        }
        return new StandardKernel (new UnityNinjectSettings (), new Ninject.Modules.INinjectModule[] {
            new UnityModule (),
            module,
        } );
    }
    
    public static object levelScope = new object();
    private static object scoper(Ninject.Activation.IContext context) {
        return levelScope;
    }
}

