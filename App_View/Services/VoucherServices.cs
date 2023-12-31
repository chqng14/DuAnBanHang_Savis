﻿using App_Data.Models;
using App_Data.ViewModels.Voucher;
using App_View.IServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System.Net.Http;

namespace App_View.Services
{
    public class VoucherServices : IVoucherServices
    {
        private readonly HttpClient _httpClient;
        public VoucherServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddVoucherAsync(VoucherDTO item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/Voucher/CreateVoucher", item);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi xảy ra: {e}");
                return false;
            }
        }

        public async Task<bool> EditVoucher(VoucherDTO item)
        {
            ///api/Voucher/UpdateVoucher
            try
            {
                var reponse = await _httpClient.PutAsJsonAsync($"/api/Voucher/UpdateVoucher", item);
                if (reponse.IsSuccessStatusCode)
                {
                    return await reponse.Content.ReadAsAsync<bool>();
                }
                else
                {
                    throw new Exception("Lỗi");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi xảy ra: {e}");
                return false;
            }
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            try
            {
                string apiUrl = "https://localhost:7165/api/Voucher/GetVoucher";
                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apidata = await response.Content.ReadAsStringAsync();
                    List<Voucher> voucherslst = JsonConvert.DeserializeObject<List<Voucher>>(apidata);
                    return voucherslst;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GetAllVoucher thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return null;
            }

        }

        public async Task<Voucher> GetVoucherAsync(string item)
        {
            try
            {
                string apiUrl = $"https://localhost:7165/api/Voucher/GetVoucherByMa?ma={item}";
                var httpclient = new HttpClient();
                var response = await httpclient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string apidata = await response.Content.ReadAsStringAsync();
                    Voucher voucher = JsonConvert.DeserializeObject<Voucher>(apidata);
                    return voucher;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API GetVoucher thất bại với mã trạng thái: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return null;
            }

        }

        public async Task<VoucherDTO> GetVoucherDTOById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<VoucherDTO>($"/api/Voucher/GetVoucherDTOByMa/{id}");
        }

        public async Task<bool> RemoveVoucher(Guid item)
        {
            try
            {
                var httpClient = new HttpClient();
                string apiUrl = $"https://localhost:7165/api/Voucher/{item}";
                var response = await httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Yêu cầu API Voucher xóa thất bại với mã trạng thái: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi xảy ra: {ex}");
                return false;
            }

        }
        public async Task<bool> UpdateVoucherAfterUseIt(Guid id)
        {

            try
            {
                var reponse = await _httpClient.PutAsync($"api/Voucher/UpdateVoucherAfterUseIt/{id}", null);
                if (reponse.IsSuccessStatusCode)
                {
                    return await reponse.Content.ReadAsAsync<bool>();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi xảy ra: {e}");
                return false;
            }
        }
    }
}
