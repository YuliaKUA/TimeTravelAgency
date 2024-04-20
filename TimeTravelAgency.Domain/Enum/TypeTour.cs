using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TimeTravelAgency.Domain.Enum
{
    public enum TypeTour
    {
        [Display(Name = "Палеозой")]
        Paleozoic = 0,
        [Display(Name = "Мезозой")]
        Mesozoic = 1,
        [Display(Name = "Палеолит")]
        Paleolithic = 2,
        [Display(Name = "Мезолит")]
        Mesolithic = 3,
        [Display(Name = "Неолит")]
        Neolithic = 4,
        [Display(Name = "Энеолит")]
        Eneolithic = 5,
        [Display(Name = "Бронзовый век")]
        BronzeAge = 6,
        [Display(Name = "Ранний железный век")]
        EarlyIronAge = 7,
        [Display(Name = "Средние века")]
        MiddleAges = 8,
        [Display(Name = "Новое время")]
        NewTime = 9,
        [Display(Name = "Лето человечества")]
        SummerOfHumanity = 10,
        [Display(Name = "Марсианская империя")]
        MartianEmpire = 11,
        [Display(Name = "Вторжение машин")]
        InvasionOfMachines = 12,
        [Display(Name = "Войны машин и астероморфов")]
        WarsMachinesAndAsteromorphs = 13,
        [Display(Name = "Галактическое единство")]
        GalacticUnity = 14,
        [Display(Name = "Все грядущие дни")]
        AllDaysToCome = 15,

    }
}
