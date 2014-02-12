//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.IO;

namespace Unibill.Impl {
    public class FakeRawAmazonAppStoreBillingInterface : IRawAmazonAppStoreBillingInterface {

        private AmazonAppStoreBillingService amazon;
        public void initialise (AmazonAppStoreBillingService amazon) {
            this.amazon = amazon;
        }

        public void initiateItemDataRequest (string[] productIds) {
            amazon.onProductListReceived(File.ReadAllText("../../../data/requestProductsResponseAmazon.json"));
        }

        public void initiatePurchaseRequest (string productId) {
            amazon.onPurchaseSucceeded(getPurchaseResponse(productId));
        }

        public void restoreTransactions() {
            amazon.onTransactionsRestored("true");
        }

        public static string getPurchaseResponse(string productId) {
            var h = new Hashtable ();
            h.Add ("productId", productId);
            h.Add ("purchaseToken", "TOKEN");
            return h.toJson ();
        }
    }
}
