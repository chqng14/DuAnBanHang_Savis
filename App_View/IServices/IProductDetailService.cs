﻿using App_Data.Models;
using App_Data.ViewModels.ProductDetail;
using Microsoft.AspNetCore.Mvc;

namespace App_View.IServices
{
    public interface IProductDetailService
    {
        Task<List<ProductViewModel>?> GetListProductViewModelAsync();
        Task<HttpResponseMessage> CreatProductDetailAsync(ProductDetailDTO productDetailDTO);
        Task UpdateProdutDetailAsync(ProductDetailDTO productDetailDTO);
        Task DeleteListProductDetailAsync(List<Guid> lstIdProductDetailRemove);
        Task DeleteProductDetail(Guid id);
        Task<ProductDetailDTO?> GetProductDTOByIdAsync(Guid id);
        Task<HttpResponseMessage> GetProductDetailForUpdateOrAdd(ProductDetailDTO productDetailDTO);
    }
}