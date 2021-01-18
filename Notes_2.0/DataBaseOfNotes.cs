using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes_2._0
{
    static class DataBaseOfNotes
    {
        static public List<SinglNote> AllNotesCollection;
        static public List<SinglNote> DateNotesCollection;
        static public List<SinglNote> NoteForShow;
        
        static DataBaseOfNotes()
        {
            AllNotesCollection = new List<SinglNote>();
            DateNotesCollection = new List<SinglNote>();
            NoteForShow = new List<SinglNote>();
        }
    }
}
