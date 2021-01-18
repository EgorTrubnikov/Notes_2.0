using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Notes_2._0
{
    class ShowNote
    {
        static public void Demonstration(List<SinglNote> Arg, MainWindow main)
        {
            
            main.XNoteShowDate.Text = Arg[0].TimeOfNote;
            if (Arg[0].TimeOfRepeatRead == DateTime.MinValue)
            {
                main.XNoteShowNextDate.Text = "Не установлена";
                main.XNoteShowTheme.Text = Arg[0].TitleOfNote;
                main.XNoteShowNoteBody.Text = Arg[0].BodyOfNote;
                main.XNoteShowPriority.Text = Arg[0].PriorityOfNote;
                main.XNoteShowStatus.Text = Arg[0].StatusOfNote;
            }
            else
            {
                main.XNoteShowNextDate.Text = Arg[0].TimeOfRepeatRead.ToLongDateString();
                main.XNoteShowTheme.Text = Arg[0].TitleOfNote;
                main.XNoteShowNoteBody.Text = Arg[0].BodyOfNote;
                main.XNoteShowPriority.Text = Arg[0].PriorityOfNote;
                main.XNoteShowStatus.Text = Arg[0].StatusOfNote;
            }
        }

        static public void ClearDemonstrationWindow(MainWindow main)
        {
            main.XNoteShowDate.Text = "";
            main.XNoteShowNextDate.Text = ""; ;
            main.XNoteShowTheme.Text = ""; ;
            main.XNoteShowNoteBody.Text = ""; ;
            main.XNoteShowPriority.Text = ""; ;
            main.XNoteShowStatus.Text = "";
        }
    }
}
