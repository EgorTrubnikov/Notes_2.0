using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Notes_2._0
{
    /// <summary>
    /// Interaction logic for WindowEditNote.xaml
    /// </summary>
    public partial class WindowEditNote : Window
    {
        event SaveNotes save;

        string editedTimeNewNote;
        string editedNoteTitle;
        string editedNoteBody;
        string editedNotePriority;
        string editedNoteStatus;
        DateTime editedNextDate;
        public WindowEditNote()
        {
            InitializeComponent();

            editedTimeNewNote = DateTime.Now.ToLongDateString();
            XXXXTextBoxThemOfNote.Text = DataBaseOfNotes.NoteForShow[0].TitleOfNote;
            XXXXTextBlockDateOfNote.Text = DateTime.Now.ToLongDateString();
            XXXXTextBoxNote.Text = DataBaseOfNotes.NoteForShow[0].BodyOfNote;
            switch (DataBaseOfNotes.NoteForShow[0].PriorityOfNote)
            {
                case "высокий":
                    XXXXRadioButtonHighPriority.IsChecked = true;
                    break;

                case "без приоритета":
                    XXXXRadioButtonWithoutPriority.IsChecked = true;
                    break;
            }
            switch (DataBaseOfNotes.NoteForShow[0].StatusOfNote)
            {
                case "важно":
                    XXXXRadioButtonImportant.IsChecked = true;
                    break;

                case "в работе":
                    XXXXRadioButtonInWork.IsChecked = true;
                    break;

                case "на память":
                    XXXXRadioButtonForMemory.IsChecked = true;
                    break;

                case "архив":
                    XXXXRadioButtonArchive.IsChecked = true;
                    break;
            }

            XXXXComboBoxNextDay.ItemsSource = CalendarClass.ObsDays;
            XXXXComboBoxNextMonth.ItemsSource = CalendarClass.ObsMonth;
            XXXXComboBoxNextYear.ItemsSource = CalendarClass.ObsYears;

            save += SavedAndLoaded.SavingNotes;
        }

        #region // RadioButtons - приоритет заметки
        private void XXXXRadioButtonHighPriority_Checked(object sender, RoutedEventArgs e)
        {
            if (editedNoteStatus == "архив")
            {
                MessageBox.Show("Статус заметки был выставлен как \"архив\".\nПри выборе варианта приоритета \"высокий\", статус заметки изменился на \"в работе\".", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXXXRadioButtonArchive.IsChecked = false;
                XXXXRadioButtonInWork.IsChecked = true;
            }
            editedNotePriority = "высокий";
        }

        private void XXXXRadioButtonWithoutPriority_Checked(object sender, RoutedEventArgs e)
        {
            editedNotePriority = "без приоритета";
        }
        #endregion

        #region // RadioButtons - статус заметки
        private void XXXXRadioButtonImportant_Checked(object sender, RoutedEventArgs e)
        {
            editedNoteStatus = "важно";
        }

        private void XXXXRadioButtonInWork_Checked(object sender, RoutedEventArgs e)
        {
            editedNoteStatus = "в работе";
        }

        private void XXXXRadioButtonForMemory_Checked(object sender, RoutedEventArgs e)
        {
            if (editedNotePriority == "высокий")
            {
                MessageBox.Show("Приоритет заметки выставлен как \"высокий\".\nЧто бы выставить статус \"на память\", сначала измените значение приоритета.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXXXRadioButtonForMemory.IsChecked = false;
                XXXXRadioButtonInWork.IsChecked = true;
            }

            editedNoteStatus = "на память";
            XXXXComboBoxNextDay.SelectedIndex = -1;
            XXXXComboBoxNextMonth.SelectedIndex = -1;
            XXXXComboBoxNextYear.SelectedIndex = -1;

        }

        private void XXXXRadioButtonArchive_Checked(object sender, RoutedEventArgs e)
        {
            if (editedNotePriority == "высокий")
            {
                MessageBox.Show("Приоритет заметки выставлен как \"высокий\".\nЧто бы выставить статус \"архив\", сначала измените значение приоритета.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXXXRadioButtonArchive.IsChecked = false;
                XXXXRadioButtonInWork.IsChecked = true;
            }
            editedNoteStatus = "архив";
            XXXXComboBoxNextDay.SelectedIndex = -1;
            XXXXComboBoxNextMonth.SelectedIndex = -1;
            XXXXComboBoxNextYear.SelectedIndex = -1;

        }
        #endregion

        #region // Кнопка "Отмена"
        private void XXXXButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                foreach (Window window in App.Current.Windows)
                {
                    if (window is WindowEditNote)
                        window.Close();
                }
                mainWindow.Show();
            }
            catch
            {

            }

        }
        #endregion

        #region// Кнопка "Сохранить"
        private void XXXXButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заголовка заметки, если пустой, в конструктор идёт значение "без названия".
                if (XXXXTextBoxThemOfNote.Text.Length > 0)
                {
                    editedNoteTitle = XXXXTextBoxThemOfNote.Text;
                }
                else
                {
                    editedNoteTitle = "без названия";
                }

                // Проверка тела заметки, обязательно должно быть заполнено.
                if (XXXXTextBoxNote.Text.Length > 0)
                {
                    editedNoteBody = XXXXTextBoxNote.Text;
                }
                else
                {
                    MessageBox.Show("Что бы сохранить заметку, должно быть обязательно заполнено поле \"Заметка\"", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //Инициализация даты следующего обращения к заметке.
                try
                {
                    // Начальные значения даты.
                    int year = 0001;
                    int month = 01;
                    int day = 01;

                    // Инициализация года.
                    if (XXXXComboBoxNextYear.SelectedIndex == -1)
                    {
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        year = (int)XXXXComboBoxNextYear.SelectedValue;
                    }

                    // Инициализация месяца.
                    if (XXXXComboBoxNextMonth.SelectedIndex == -1)
                    {
                        month = DateTime.Now.Month;
                    }
                    else
                    {
                        month = XXXXComboBoxNextMonth.SelectedIndex + 1;
                    }

                    // Инициализация дня.
                    if (XXXXComboBoxNextDay.SelectedIndex == -1) // Если день не выбран - значит либо соответствующий статус, либо нет нужды, все значения по умолчанию.
                    {
                        year = 0001;
                        month = 01;
                        day = 01;
                    }
                    else
                    {
                        day = (int)XXXXComboBoxNextDay.SelectedValue;
                    }

                    // Инициализация даты следующего обращения к заметке.
                    editedNextDate = new DateTime(year, month, day);

                    if (editedNextDate <= DateTime.Now && editedNextDate != DateTime.MinValue) // Если дата следующего обращения к заметке меньше текущей.
                    {
                        editedNextDate = DateTime.Now.AddDays(1);
                    }
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Сохраняем изменения.
                foreach(var show in DataBaseOfNotes.AllNotesCollection)
                {
                    if(show == DataBaseOfNotes.NoteForShow[0])
                    {
                        DataBaseOfNotes.AllNotesCollection.Remove(show);
                        break;
                    }
                }

                DataBaseOfNotes.NoteForShow[0] = new SinglNote(editedTimeNewNote,
                    editedNoteTitle, editedNoteBody, editedNotePriority, editedNoteStatus, editedNextDate);

                DataBaseOfNotes.AllNotesCollection.Add(DataBaseOfNotes.NoteForShow[0]);

                save?.Invoke(DataBaseOfNotes.AllNotesCollection);

                MainWindow mainWindow = new MainWindow();
                    foreach (Window window in App.Current.Windows)
                    {
                        if (window is WindowEditNote)
                            window.Close();
                    }
                    mainWindow.Show();
            }
            catch
            {

            }
        }
        #endregion
    }
}
