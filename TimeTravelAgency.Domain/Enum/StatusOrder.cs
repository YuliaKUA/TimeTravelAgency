using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimeTravelAgency.Domain.Enum
{
    public enum StatusOrder
    {
        [Display(Name = "Создан")]
        Сreated = 0,
        [Display(Name = "Ожидает оплату")]
        AwaitingPayment = 1,
        [Display(Name = "Оплачен")]
        Paid = 2,
        [Display(Name = "Подтвержден")]
        Confirmed = 3,
        [Display(Name = "Отменен")]
        Cancelled = 4,
    }
}
