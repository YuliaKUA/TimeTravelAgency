using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTravelAgency.Domain.CustomValidationAttributes
{
    public class IsFileEmpty : ValidationAttribute
    {
        public override bool IsValid(object? file)
        {
            if (file == null)
            {
                return false;
            }
            return true;
        }
    }
}
