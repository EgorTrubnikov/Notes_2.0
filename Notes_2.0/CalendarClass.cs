using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_2._0
{
    static public class CalendarClass
    {
        static public ObservableCollection<int> ObsDays;
        static public ObservableCollection<string> ObsMonth;
        static public ObservableCollection<int> ObsYears;

        static CalendarClass()
        {
            ObsDays = new ObservableCollection<int>
            { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31};

            ObsMonth = new ObservableCollection<string>
            { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"};

            ObsYears = new ObservableCollection<int>
            {2021, 2022, 2023, 2024, 2025};

        }
    }
}
