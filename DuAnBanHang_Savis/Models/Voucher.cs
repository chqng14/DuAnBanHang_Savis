﻿using App_Data.ViewModels.Voucher;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App_Data.Models
{
    public class Voucher
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Tên voucher là trường bắt buộc.")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Tên voucher phải có ít nhất 5 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]*$", ErrorMessage = "Tên voucher không được chứa ký tự đặc biệt, ngoại trừ dấu cách.")]
        public string? Ten { get; set; }
        public string? Ma { get; set; }
        [Required(ErrorMessage = "Loại hình khuyến mãi là trường bắt buộc.")]
        public int? LoaiHinhKm { get; set; }

        [CustomMucUuDaiValidation]
        public decimal MucUuDai { get; set; }
        [Required(ErrorMessage = "Điều kiện là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Điều kiện phải là số nguyên không âm.")]
        public int? DieuKien { get; set; }
        [Required(ErrorMessage = "Số lượng là trường bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lần sử dụng phải lớn hơn 0.")]
        public int? SoLuongTon { get; set; }


        [Display(Name = "Ngày bắt đầu")]
        [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
        public DateTime? NgayBatDau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là trường bắt buộc.")]
        [CustomNgayKetThucValidation(ErrorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.")]
        public DateTime? NgayKetThuc { get; set; }

        public int? TrangThai { get; set; }
        public virtual List<Bill> Bills { get; set; }



        //Validate MucUuDai

    }
    public class CustomMucUuDaiValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (VoucherDTO)validationContext.ObjectInstance;

            if (model.LoaiHinhKm == 0)
            {
                if (value is double mucUuDai && mucUuDai <= 0)
                {
                    return new ValidationResult("Số tiền giảm phải lớn hơn 0.");
                }
            }
            else if (model.LoaiHinhKm == 1)
            {
                if (value is double mucUuDai && (mucUuDai <= 0 || mucUuDai > 100))
                {
                    return new ValidationResult("% giảm phải nằm trong khoảng từ 0 đến 100.");
                }
            }
            else if (model.LoaiHinhKm == 2)
            {
                // Kiểm tra mức ưu đãi theo điều kiện riêng cho LoaiHinhUuDai là 2
                // Điều kiện này tương tự với khi LoaiHinhUuDai là 0
                if (value is double mucUuDai && mucUuDai <= 0)
                {
                    return new ValidationResult("Số tiền giảm đãi phải lớn hơn 0.");
                }
            }

            return ValidationResult.Success;
        }
    }
    public class CustomNgayKetThucValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ngayKetThuc = (DateTime)value;
            var model = (VoucherDTO)validationContext.ObjectInstance;

            if (ngayKetThuc <= model.NgayBatDau)
            {
                return new ValidationResult("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu.");
            }

            return ValidationResult.Success;
        }
    }


}
