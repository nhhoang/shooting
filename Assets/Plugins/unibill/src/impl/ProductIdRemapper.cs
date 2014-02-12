//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections.Generic;
using Uniject;

namespace Unibill.Impl {

    /// <summary>
    /// Product identifier remapper.
    /// </summary>
    public class ProductIdRemapper {

        private Dictionary<string, string> genericToPlatformSpecificIds;
        private Dictionary<string, string> platformSpecificToGenericIds;
        public InventoryDatabase db { get; private set; }
        private UnibillXmlParser parser;

        public ProductIdRemapper (InventoryDatabase db, UnibillXmlParser parser, UnibillConfiguration config) {
            this.db = db;
            this.parser = parser;
            initialiseForPlatform(config.CurrentPlatform);
        }

        public void initialiseForPlatform (BillingPlatform platform) {
            genericToPlatformSpecificIds = new Dictionary<string, string>();
            platformSpecificToGenericIds = new Dictionary<string, string>();
            string lookingFor = string.Format("{0}.Id", platform);
            foreach (var item in parser.Parse("unibillInventory", "item")) {
                string id = item.attributes["id"];
                string mapsTo = id;
                if (item.kvps.ContainsKey(lookingFor)) {
                    mapsTo = item.kvps[lookingFor];
                }
                genericToPlatformSpecificIds[id] = mapsTo;
                platformSpecificToGenericIds[mapsTo] = id;

            }
        }

        public string[] getAllPlatformSpecificProductIds () {
            var ids = new List<string> ();
            foreach (PurchasableItem item in db.AllPurchasableItems) {
                ids.Add(mapItemIdToPlatformSpecificId(item));
            }

            return ids.ToArray();
        }

        public string mapItemIdToPlatformSpecificId(PurchasableItem item) {
            return genericToPlatformSpecificIds[item.Id];
        }

        public PurchasableItem getPurchasableItemFromPlatformSpecificId(string platformSpecificId) {
            string genericId = platformSpecificToGenericIds[platformSpecificId];
            return db.getItemById(genericId);
        }

        public bool canMapProductSpecificId (string id) {
            if (platformSpecificToGenericIds.ContainsKey (id)) {
                return true;
            }
            return false;
        }
    }
}
