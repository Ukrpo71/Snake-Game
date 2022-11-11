using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class InApps : MonoBehaviour, IStoreListener
{
    [SerializeField] private Button _purchaseButton;

    [SerializeField] private CharacterSelectionUISetup _characterSelection;

    private static IStoreController m_StoreController;
    IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

    public static string Paramedic = "Paramedic";
    public static string Scarecrow = "Scarecrow";
    public static string SummerGirl = "SummerGirl";
    public static string NoAds = "NoAds";

    [SerializeField] private IAPButton button = new IAPButton();


    public void ChangePurchaseButtonID(string id)
    {
        _purchaseButton.onClick.AddListener(delegate { BuyProductID(id); });
        _purchaseButton.GetComponentInChildren<TextMeshProUGUI>().text = "$" + m_StoreController.products.all.FirstOrDefault(p => 
                                                                                                  p.definition.id == id).metadata.localizedPrice;
    }

    public void ChangeIAPButtonID(string id)
    {
        button.productId = id;
        button.GetComponentInChildren<TextMeshProUGUI>().text = "$" + m_StoreController.products.all.FirstOrDefault(p =>
                                                                                                  p.definition.id == id).metadata.localizedPrice;
    }

    public void UnlockSkin(Product product)
    {
        DataPersist dataPersist = FindObjectOfType<DataPersist>();
        dataPersist.PlayerData.Skins.First(s => s.Name.ToLower() == product.definition.id.ToLower()).IsUnlocked = true;
        dataPersist.Save();
        _characterSelection.SetupSkinBackground();
    }

    public void DisableAds()
    {
        FindObjectOfType<DataPersist>().PlayerData.NoAds = true;
        InterstitialAd.Instance.StopAllAds();
        FindObjectOfType<DataPersist>().Save();
    }

    private void Start()
    {
        InitializePurchasing();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(SummerGirl, ProductType.NonConsumable);
        builder.AddProduct(Paramedic, ProductType.NonConsumable);
        builder.AddProduct(Scarecrow, ProductType.NonConsumable);
        builder.AddProduct(NoAds, ProductType.NonConsumable);


        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProductID(string productID)
    {
        m_StoreController.InitiatePurchase(productID);
    }
    
    

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        if(m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(product))
        {
            //The purchase is Deferred.
            //Therefore, we do not unlock the content or complete the transaction.
            //ProcessPurchase will be called again once the purchase is Purchased.
            return PurchaseProcessingResult.Pending;
        }

        if (product.definition.id != NoAds)
        {
            UnlockSkin(product);
        }

        if (product.definition.id == NoAds)
        {
            DisableAds();
        }

        Debug.Log($"Purchase Complete - Product: {product.definition.id}");

        return PurchaseProcessingResult.Complete;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
    }

}
