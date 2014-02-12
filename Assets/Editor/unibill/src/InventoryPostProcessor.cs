//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using UnityEngine;
using UnityEditor;
using Unibill;
using Unibill.Impl;
using Ninject;
using System.IO;

public class InventoryPostProcessor : AssetPostprocessor {
	
	private const string UNIBILL_INVENTORY_PATH = "Assets/Plugins/unibill/resources/unibillInventory.xml";
	
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath) {
		
		if (!File.Exists(UNIBILL_INVENTORY_PATH)) {
			AssetDatabase.CopyAsset("Assets/Plugins/unibill/static/InventoryTemplate.xml", UNIBILL_INVENTORY_PATH);
		}
		
        foreach (var s in importedAssets) {
            if (s.Contains("unibillInventory.xml")) {
                UnityInjector._get().Get<StorekitMassImportTemplateGenerator>().writeFile();
                UnityInjector._get().Get<GooglePlayCSVGenerator>().writeCSV();
            }
        }
    }
}
