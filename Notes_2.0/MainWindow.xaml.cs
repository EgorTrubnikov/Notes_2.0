using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Notes_2._0
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SavedAndLoaded.LoadingNotes(DataBaseOfNotes.AllNotesCollection);
            XListBoxAllNotes.ItemsSource = DataBaseOfNotes.AllNotesCollection;

            foreach (var e in DataBaseOfNotes.AllNotesCollection)
            {
                if(e.TimeOfRepeatRead == DateTime.Now || (e.TimeOfRepeatRead < DateTime.Now && e.TimeOfRepeatRead != DateTime.MinValue))
                {
                    if(DataBaseOfNotes.DateNotesCollection.Contains(e) == false)
                    {
                        DataBaseOfNotes.DateNotesCollection.Add(e);
                    }
                    
                }
            }

            XListBoxToSeeOnDate.ItemsSource = DataBaseOfNotes.DateNotesCollection;

            if (DataBaseOfNotes.NoteForShow.Count != 0)
            {
                ShowNote.Demonstration(DataBaseOfNotes.NoteForShow, this);
            }
        }

        //Кнопка Новая заметка
        private void XAddNote_Click(object sender, RoutedEventArgs e)
        {
            WindowNewNote windowNewNote = new WindowNewNote();
            foreach (Window window in App.Current.Windows)
            {
                if (window is MainWindow)
                    window.Close();
            }
            windowNewNote.Show();
            
        }

        //Кнопка Удалить заметку
        private void XDelNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBaseOfNotes.AllNotesCollection.Remove(DataBaseOfNotes.NoteForShow[0]);
                XListBoxAllNotes.Items.Refresh();
            }
            catch
            {
                DataBaseOfNotes.NoteForShow.Clear();
                ShowNote.ClearDemonstrationWindow(this);
                SavedAndLoaded.SavingNotes(DataBaseOfNotes.AllNotesCollection);
            }

        }

        // Кнопка Редактировать заметку
        private void XEditNote_Click(object sender, RoutedEventArgs e)
        {

        }


        // Обработка выбора Радиобаттонов
        private void XRBAddNote_Checked(object sender, RoutedEventArgs e)
        {
            DataBaseOfNotes.AllNotesCollection.Sort(new SinglNote.SortByDate());
            XListBoxAllNotes.Items.Refresh();

        }

        private void XRBTheme_Checked(object sender, RoutedEventArgs e)
        {
            DataBaseOfNotes.AllNotesCollection.Sort(new SinglNote.SortByTitle());
            XListBoxAllNotes.Items.Refresh();

        }

        private void XRBPriority_Checked(object sender, RoutedEventArgs e)
        {
            DataBaseOfNotes.AllNotesCollection.Sort(new SinglNote.SortByPriority());
            XListBoxAllNotes.Items.Refresh();
        }

        private void XRBStatus_Checked(object sender, RoutedEventArgs e)
        {
            DataBaseOfNotes.AllNotesCollection.Sort(new SinglNote.SortByStatus());
            XListBoxAllNotes.Items.Refresh();
        }

        // Выбор заметки из общего списка заметок
        private void XListBoxAllNotes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBaseOfNotes.NoteForShow.Clear();
            DataBaseOfNotes.NoteForShow.Add(XListBoxAllNotes.SelectedValue as SinglNote);
            ShowNote.Demonstration(DataBaseOfNotes.NoteForShow, this);

            try
            {
                XListBoxToSeeOnDate.SelectedValue = null;
            }
            catch
            {

            }
        }

        // Выбор заметки из списка заметок Просмотр на дату
        private void XListBoxToSeeOnDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBaseOfNotes.NoteForShow.Clear();
            DataBaseOfNotes.NoteForShow.Add(XListBoxToSeeOnDate.SelectedValue as SinglNote);
            ShowNote.Demonstration(DataBaseOfNotes.NoteForShow, this);

            try
            {
                XListBoxAllNotes.SelectedValue = null;
            }
            catch
            {

            }
        }
    }
}
