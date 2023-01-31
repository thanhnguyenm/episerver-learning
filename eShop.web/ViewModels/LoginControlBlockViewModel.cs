using eShop.web.Models.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.ViewModels
{
    public class LoginControlBlockViewModel : BlockViewModel<LoginControlBlock>
    {
        public LoginControlBlockViewModel(LoginControlBlock loginControlBlock) : base(loginControlBlock)
        {
            
        }
    }
}