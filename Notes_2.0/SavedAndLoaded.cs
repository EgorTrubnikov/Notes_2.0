using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Notes_2._0
{
    class SavedAndLoaded
    {
        static public void SavingNotes(List<SinglNote> arg)
        {
            if(DataBaseOfNotes.AllNotesCollection.Count != 0)
            {
                JObject AllNotesMainTree = new JObject();
                JArray Notes = new JArray();
                for(int i = 0; i < DataBaseOfNotes.AllNotesCollection.Count; i++)
                {
                    JObject singlNote = new JObject();
                    singlNote["TimeOfNote"] = DataBaseOfNotes.AllNotesCollection[i].TimeOfNote;
                    singlNote["TitleOfNote"] = DataBaseOfNotes.AllNotesCollection[i].TitleOfNote;
                    singlNote["BodyOfNote"] = DataBaseOfNotes.AllNotesCollection[i].BodyOfNote;
                    singlNote["PriorityOfNote"] = DataBaseOfNotes.AllNotesCollection[i].PriorityOfNote;
                    singlNote["StatusOfNote"] = DataBaseOfNotes.AllNotesCollection[i].StatusOfNote;
                    singlNote["TimeOfRepeatRead"] = DataBaseOfNotes.AllNotesCollection[i].TimeOfRepeatRead.ToLongDateString();

                    Notes.Add(singlNote);
                }
                AllNotesMainTree["AllSavesNotes:"] = Notes;
                string jSon = AllNotesMainTree.ToString();
                File.WriteAllText("_allSavedNotes.json", jSon);
            }
        }

        static public void LoadingNotes(List<SinglNote> arg)
        {
            if (File.Exists("_allSavedNotes.json") == true)
            {
                string jSon = File.ReadAllText("_allSavedNotes.json");
                var jArray = JObject.Parse(jSon)["AllSavesNotes:"].ToArray();
                foreach(var e in jArray)
                {
                    SinglNote note = new SinglNote(
                        e["TimeOfNote"].ToString(),
                        e["TitleOfNote"].ToString(),
                        e["BodyOfNote"].ToString(),
                        e["PriorityOfNote"].ToString(),
                        e["StatusOfNote"].ToString(),
                        Convert.ToDateTime(e["TimeOfRepeatRead"].ToString()));

                        if(DataBaseOfNotes.AllNotesCollection.Contains(note))
                        {
                            continue;
                        }
                        else
                        {
                            DataBaseOfNotes.AllNotesCollection.Add(note);
                        }
                }
            }

        }
    }
}
