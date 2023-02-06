using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Agava.YandexGames
{
    public static class Billing
    {
        private static Action s_onPurchaseProductSuccessCallback;
        private static Action<string> s_onPurchaseProductErrorCallback;

        private static Action s_onConsumeProductSuccessCallback;
        private static Action<string> s_onConsumeProductErrorCallback;
        
        private static Action<GetProductCatalogResponse> s_onGetProductCatalogSuccessCallback;
        private static Action<string> s_onGetProductCatalogErrorCallback;

        private static Action s_onGetPurchasedProductsSuccessCallback;
        private static Action<string> s_onGetPurchasedProductsErrorCallback;

        #region PurchaseProduct
        public static void PurchaseProduct(string productId, Action onSuccessCallback = null, Action<string> onErrorCallback = null, string developerPayload = "")
        {
            s_onPurchaseProductSuccessCallback = onSuccessCallback;
            s_onPurchaseProductErrorCallback = onErrorCallback;

            BillingPurchaseProduct(productId, OnPurchaseProductSuccessCallback, OnPurchaseProductErrorCallback, developerPayload);
        }

        [DllImport("__Internal")]
        private static extern void BillingPurchaseProduct(string productId, Action successCallback, Action<string> errorCallback, string developerPayload);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnPurchaseProductSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnPurchaseProductSuccessCallback)} invoked");

            s_onPurchaseProductSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnPurchaseProductErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnPurchaseProductErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onPurchaseProductErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region ConsumeProduct
        public static void ConsumeProduct(string purchasedProductToken, Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onConsumeProductSuccessCallback = onSuccessCallback;
            s_onConsumeProductErrorCallback = onErrorCallback;

            BillingConsumeProduct(purchasedProductToken, OnConsumeProductSuccessCallback, OnConsumeProductErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void BillingConsumeProduct(string purchasedProductToken, Action successCallback, Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnConsumeProductSuccessCallback()
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnConsumeProductSuccessCallback)} invoked");

            s_onConsumeProductSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnConsumeProductErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnConsumeProductErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onConsumeProductErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region GetProductCatalog
        public static void GetProductCatalog(Action<GetProductCatalogResponse> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onGetProductCatalogSuccessCallback = onSuccessCallback;
            s_onGetProductCatalogErrorCallback = onErrorCallback;

            BillingGetProductCatalog(OnGetProductCatalogSuccessCallback, OnGetProductCatalogErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void BillingGetProductCatalog(Action<string> successCallback, Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetProductCatalogSuccessCallback(string productCatalogResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnGetProductCatalogSuccessCallback)} invoked, {nameof(productCatalogResponseJson)} = {productCatalogResponseJson}");

            //GetProductCatalogResponse productCatalogResponse = JsonUtility.FromJson<GetProductCatalogResponse>(productCatalogResponseJson);

            //Debug.Log(JsonUtility.ToJson(productCatalogResponse));

            var temporaryMock = new GetProductCatalogResponse();

            s_onGetProductCatalogSuccessCallback?.Invoke(temporaryMock);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetProductCatalogErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnGetProductCatalogErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetProductCatalogErrorCallback?.Invoke(errorMessage);
        }
        #endregion

        #region GetPurchasedProducts
        public static void GetPurchasedProducts(Action onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            s_onGetPurchasedProductsSuccessCallback = onSuccessCallback;
            s_onGetPurchasedProductsErrorCallback = onErrorCallback;

            BillingGetPurchasedProducts(OnGetPurchasedProductsSuccessCallback, OnGetPurchasedProductsErrorCallback);
        }

        [DllImport("__Internal")]
        private static extern void BillingGetPurchasedProducts(Action<string> successCallback, Action<string> errorCallback);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPurchasedProductsSuccessCallback(string purchasedProductsResponseJson)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnGetPurchasedProductsSuccessCallback)} invoked, {nameof(purchasedProductsResponseJson)} = {purchasedProductsResponseJson}");

            s_onGetPurchasedProductsSuccessCallback?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPurchasedProductsErrorCallback(string errorMessage)
        {
            if (YandexGamesSdk.CallbackLogging)
                Debug.Log($"{nameof(Billing)}.{nameof(OnGetPurchasedProductsErrorCallback)} invoked, {nameof(errorMessage)} = {errorMessage}");

            s_onGetPurchasedProductsErrorCallback?.Invoke(errorMessage);
        }
        #endregion
    }
}
