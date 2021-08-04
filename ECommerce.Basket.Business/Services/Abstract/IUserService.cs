using ECommerce.Basket.Data.Entities;
using ECommerce.Basket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<TokenModel> Login(User user);
    }
}
