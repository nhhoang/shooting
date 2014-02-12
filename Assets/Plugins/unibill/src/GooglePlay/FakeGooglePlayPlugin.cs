//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.IO;
using Unibill.Impl;

namespace Unibill.Impl {
    public class FakeGooglePlayPlugin : Unibill.Impl.IRawGooglePlayInterface {

        private GooglePlayBillingService callback;
        public bool available = true;

        private InventoryDatabase db;
        private ProductIdRemapper remapper;

        public FakeGooglePlayPlugin (InventoryDatabase db, ProductIdRemapper remapper) {
            this.db = db;
            this.remapper = remapper;
        }

        #region IRawGooglePlayInterface implementation
        public void initialise (Unibill.Impl.GooglePlayBillingService callback, string publicKey) {
            this.callback = callback;
            if (available) {
                callback.onProductListReceived (File.ReadAllText("../../../data/requestProductsResponse.json"));
            }
        }
        #endregion

        #region IRawGooglePlayInterface implementation

        public void purchase (string product) {
            Hashtable response = new Hashtable ();
            response.Add ("productId", product);
            response.Add ("signature", "signature");
            callback.onPurchaseSucceeded(response.toJson());
        }

        public void restoreTransactions () {
            callback.onPurchaseSucceeded(remapper.mapItemIdToPlatformSpecificId(db.AllNonConsumablePurchasableItems[0]));
            callback.onTransactionsRestored("true");
        }

        #endregion
    }
}

