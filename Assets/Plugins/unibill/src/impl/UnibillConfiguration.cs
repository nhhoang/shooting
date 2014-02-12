//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using Uniject;
using Mono.Xml;
using UnityEngine;

namespace Unibill.Impl {

    /// <summary>
    /// The underlying platform be it from Google, Amazon, Apple etc.
    /// </summary>
    public enum BillingPlatform {
        GooglePlay,
        AmazonAppstore,
        AppleAppStore,
		MacAppStore,
    }

    public class UnibillConfiguration {

        public string iOSSKU { get; private set; }
        public BillingPlatform CurrentPlatform { get; private set; }
        public string GooglePlayPublicKey { get; private set; }
        public bool AmazonSandboxEnabled { get; private set; }

        public UnibillConfiguration (IResourceLoader loader, UnibillXmlParser parser, IUtil util, ILogger logger) {
            var element = parser.Parse ("unibillInventory", "inventory") [0];
            string o = null;
            element.kvps.TryGetValue("iOSSKU", out o);
            iOSSKU = o;
            element.kvps.TryGetValue("GooglePlayPublicKey", out o);
            GooglePlayPublicKey = o;
            this.AmazonSandboxEnabled = bool.Parse (element.kvps ["useAmazonSandbox"]);
			
			CurrentPlatform = BillingPlatform.MacAppStore;
#if UNITY_ANDROID
            CurrentPlatform = (BillingPlatform) Enum.Parse(typeof(BillingPlatform), element.kvps["androidBillingPlatform"]);
#endif
#if UNITY_IPHONE
            CurrentPlatform = BillingPlatform.AppleAppStore;
#endif
        }
    }
}
