﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IShopCartBO")]
public interface IShopCartBO
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/AddProduct", ReplyAction = "http://tempuri.org/IShopCartBO/AddProductResponse")]
    void AddProduct(SwinSchool.CommonShared.Dto.ProductDto product);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/ChangeQuantity", ReplyAction = "http://tempuri.org/IShopCartBO/ChangeQuantityResponse")]
    void ChangeQuantity(SwinSchool.CommonShared.Dto.ProductDto[] updatedCart);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/RemoveProduct", ReplyAction = "http://tempuri.org/IShopCartBO/RemoveProductResponse")]
    void RemoveProduct(SwinSchool.CommonShared.Dto.ProductDto product);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/GetCart", ReplyAction = "http://tempuri.org/IShopCartBO/GetCartResponse")]
    SwinSchool.CommonShared.Dto.ProductDto[] GetCart();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/GetAvailableProducts", ReplyAction = "http://tempuri.org/IShopCartBO/GetAvailableProductsResponse")]
    SwinSchool.CommonShared.Dto.ProductDto[] GetAvailableProducts();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/GetTotalCart", ReplyAction = "http://tempuri.org/IShopCartBO/GetTotalCartResponse")]
    decimal GetTotalCart();

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IShopCartBO/CheckOut", ReplyAction = "http://tempuri.org/IShopCartBO/CheckOutResponse")]
    void CheckOut();
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IShopCartBOChannel : IShopCartBO, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class ShopCartBOClient : System.ServiceModel.ClientBase<IShopCartBO>, IShopCartBO
{

    public ShopCartBOClient()
    {
    }

    public ShopCartBOClient(string endpointConfigurationName) :
        base(endpointConfigurationName)
    {
    }

    public ShopCartBOClient(string endpointConfigurationName, string remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public ShopCartBOClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
        base(endpointConfigurationName, remoteAddress)
    {
    }

    public ShopCartBOClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
        base(binding, remoteAddress)
    {
    }

    public void AddProduct(SwinSchool.CommonShared.Dto.ProductDto product)
    {
        base.Channel.AddProduct(product);
    }

    public void ChangeQuantity(SwinSchool.CommonShared.Dto.ProductDto[] updatedCart)
    {
        base.Channel.ChangeQuantity(updatedCart);
    }

    public void RemoveProduct(SwinSchool.CommonShared.Dto.ProductDto product)
    {
        base.Channel.RemoveProduct(product);
    }

    public SwinSchool.CommonShared.Dto.ProductDto[] GetCart()
    {
        return base.Channel.GetCart();
    }

    public SwinSchool.CommonShared.Dto.ProductDto[] GetAvailableProducts()
    {
        return base.Channel.GetAvailableProducts();
    }

    public decimal GetTotalCart()
    {
        return base.Channel.GetTotalCart();
    }

    public void CheckOut()
    {
        base.Channel.CheckOut();
    }
}
