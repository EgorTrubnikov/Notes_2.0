using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_2._0
{
    public class SinglNote: IEquatable<SinglNote>
    {
        // Дата создания заметки
        public string TimeOfNote { get; set; }

        // Название заметки
        public string TitleOfNote { get; set; }

        // Сама заметка
        public string BodyOfNote { get; set; }

        //Приоритет заметки
        public string PriorityOfNote { get; set; }

        // Статус заметки
        public string StatusOfNote { get; set; }

        // Дата повторного прочтения заметки
        public DateTime TimeOfRepeatRead { get; set; }

        // Конструктор c 6 аргументами
        public SinglNote(string TimeOfNote, string TitleOfNote, string BodyOfNote, string PriorityOfNote, string StatusOfNote, DateTime TimeOfRepeatRead)
        {
            this.TimeOfNote = TimeOfNote;
            this.TitleOfNote = TitleOfNote;
            this.BodyOfNote = BodyOfNote;
            this.PriorityOfNote = PriorityOfNote;
            this.StatusOfNote = StatusOfNote;
            this.TimeOfRepeatRead = TimeOfRepeatRead;
        }

        public bool Equals(SinglNote other)
        {
            return this.BodyOfNote == other.BodyOfNote && this.TitleOfNote == other.TitleOfNote && this.TimeOfNote == other.TimeOfNote;
        }

        public class SortByDate : IComparer<SinglNote>
        {
            public int Compare(SinglNote x, SinglNote y)
            {
                SinglNote X = (SinglNote)x;
                SinglNote Y = (SinglNote)y;
                if (Convert.ToDateTime(X.TimeOfNote) == Convert.ToDateTime(Y.TimeOfNote)) return 0;
                else if (Convert.ToDateTime(X.TimeOfNote) > Convert.ToDateTime(Y.TimeOfNote)) return 1;
                else return -1;
            }
        }

        public class SortByTitle : IComparer<SinglNote>
        {
            public int Compare(SinglNote x, SinglNote y)
            {
                SinglNote X = (SinglNote)x;
                SinglNote Y = (SinglNote)y;
                return String.Compare(X.TitleOfNote, Y.TitleOfNote);
            }
        }

        public class SortByPriority : IComparer<SinglNote>
        {
            public int Compare(SinglNote x, SinglNote y)
            {
                SinglNote X = (SinglNote)x;
                SinglNote Y = (SinglNote)y;
                return String.Compare(X.PriorityOfNote, Y.PriorityOfNote);
            }
        }

        public class SortByStatus : IComparer<SinglNote>
        {
            public int Compare(SinglNote x, SinglNote y)
            {
                SinglNote X = (SinglNote)x;
                SinglNote Y = (SinglNote)y;
                return String.Compare(X.StatusOfNote, Y.StatusOfNote);
            }
        }
    }

    
}
