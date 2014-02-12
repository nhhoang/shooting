//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using UnityEditor;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using UnityEngine;
using Unibill.Impl;
using Uniject;
using Uniject.Impl;


public class AndroidManifestGenerator {

    private const string AndroidManifestPath = "Assets/Plugins/Android/AndroidManifest.xml";

    public static void mergeManifest() {

        if (!Directory.Exists("Assets/Plugins/Android")) {
            AssetDatabase.CreateFolder("Assets/Plugins", "Android");
        }

        if (!File.Exists (AndroidManifestPath)) {
            AssetDatabase.CopyAsset("Assets/Plugins/unibill/static/Manifest.xml", AndroidManifestPath);
            AssetDatabase.ImportAsset(AndroidManifestPath);
        }
        
        UnibillConfiguration config = new UnibillConfiguration(new UnityResourceLoader(), new UnibillXmlParser(new Mono.Xml.SmallXmlParser(), new UnityResourceLoader()), new UnityUtil(), new UnityLogger());
        XDocument doc = XDocument.Load(AndroidManifestPath);
        doc = new AndroidManifestMerger().merge(doc, config.CurrentPlatform, config.AmazonSandboxEnabled);
        doc.Save(AndroidManifestPath);
        AssetDatabase.ImportAsset(AndroidManifestPath);
    }
}
