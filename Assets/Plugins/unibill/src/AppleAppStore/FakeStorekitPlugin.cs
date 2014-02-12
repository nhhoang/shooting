//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.IO;
using Unibill;
using Unibill.Impl;

namespace Unibill.Impl {
    public class FakeStorekitPlugin : Unibill.Impl.IStoreKitPlugin {
        private InventoryDatabase db;
        private ProductIdRemapper remapper;
        private AppleAppStoreBillingService callback;
        private string requestProductsResponse = File.ReadAllText("../../../data/requestProductsResponse.json");

        public FakeStorekitPlugin (InventoryDatabase db, ProductIdRemapper mapper) {
            this.db = db;
            this.remapper = mapper;
        }

        public bool available = true;
        public bool storeKitPaymentsAvailable () {
            return available;
        }

        public bool functional = true;
        public void storeKitRequestProductData (string productIdentifiers) {
            if (functional) {
                callback.onProductListReceived (requestProductsResponse);
            }
        }

        public void storeKitPurchaseProduct (string productId) {
            if (functional) {
                callback.onPurchaseSucceeded (formatPurchaseResponse(productId));
            }
        }

        public void storeKitRestoreTransactions () {
            foreach (PurchasableItem item in db.AllNonConsumablePurchasableItems) {
                callback.onPurchaseSucceeded(formatPurchaseResponse(remapper.mapItemIdToPlatformSpecificId(item)));
            }
            callback.onTransactionsRestoredSuccess();
        }

        public void initialise (Unibill.Impl.AppleAppStoreBillingService callback) {
            this.callback = callback;
        }

        public static string formatPurchaseResponse(string productId) {
            return "{ \"productId\" : \"" + productId + "\", \"receipt\" : \"THIS IS A RECEIPT!\" }";
        }
    }
}
