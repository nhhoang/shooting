//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using Unibill;
using Unibill.Impl;

public class AmazonJSONGenerator {

    private ProductIdRemapper remapper;
    public AmazonJSONGenerator (ProductIdRemapper remapper) {
        this.remapper = remapper;
        remapper.initialiseForPlatform(BillingPlatform.AmazonAppstore);
    }

    public string encodeAll () {
        Hashtable result = new Hashtable();
        foreach (PurchasableItem item in remapper.db.AllPurchasableItems) {
            result[remapper.mapItemIdToPlatformSpecificId (item)] = purchasableDetailsToHashtable (item);
        }

        return Unibill.Impl.MiniJSON.jsonEncode(result);
    }

    public Hashtable purchasableDetailsToHashtable (PurchasableItem item) {
        var dic = new Hashtable();
        dic ["itemType"] = item.PurchaseType == PurchaseType.Consumable ? "CONSUMABLE" : item.PurchaseType == PurchaseType.NonConsumable ? "ENTITLED" : "SUBSCRIPTION";
        dic ["title"] = item.name == null ? string.Empty : item.name;
        dic ["description"] = item.description == null ? string.Empty : item.description;
        dic["price"] = 0.99;
        dic ["smallIconUrl"] = "http://example.com";
        if (PurchaseType.Subscription == item.PurchaseType) {
            dic["subscriptionParent"] = "does.not.exist";
        }

        return dic;
    }

}
