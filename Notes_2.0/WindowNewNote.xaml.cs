﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public delegate void SaveNotes(List<SinglNote> arg);
    /// <summary>
    /// Логика взаимодействия для WindowNewNote.xaml
    /// </summary>
    public partial class WindowNewNote : Window
    {
        string newTimeNewNote;
        string newNoteTitle;
        string newNoteBody;
        string newNotePriority;
        string newNoteStatus;
        DateTime newNextDate;

        event SaveNotes save;
        public WindowNewNote()
        {
            InitializeComponent();
            newTimeNewNote = DateTime.Now.ToLongDateString();
            XXDateOfNote.Text = newTimeNewNote;
            XXNextDay.ItemsSource = CalendarClass.ObsDays;
            XXNextMonth.ItemsSource = CalendarClass.ObsMonth;
            XXNextYear.ItemsSource = CalendarClass.ObsYears;
            save += SavedAndLoaded.SavingNotes;
        }

        #region // RadioButtons - приоритет заметки
        private void XXHighPriority_Checked(object sender, RoutedEventArgs e)
        {
            if(newNoteStatus == "архив")
            {
                MessageBox.Show("Статус заметки был выставлен как \"архив\".\nПри выборе варианта приоритета \"высокий\", статус заметки изменился на \"в работе\".", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXArchive.IsChecked = false;
                XXInWork.IsChecked = true;
            }
            newNotePriority = "высокий";
        }

        private void XXWithoutPriority_Checked(object sender, RoutedEventArgs e)
        {
            newNotePriority = "без приоритета";
        }
        #endregion

        #region // RadioButtons - статус заметки
        private void XXImportant_Checked(object sender, RoutedEventArgs e)
        {
            newNoteStatus = "важно";
        }

        private void XXInWork_Checked(object sender, RoutedEventArgs e)
        {
            newNoteStatus = "в работе";
        }

        private void XXForMemory_Checked(object sender, RoutedEventArgs e)
        {
            if (newNotePriority == "высокий")
            {
                MessageBox.Show("Приоритет заметки выставлен как \"высокий\".\nЧто бы выставить статус \"на память\", сначала измените значение приоритета.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXForMemory.IsChecked = false;
                XXInWork.IsChecked = true;
            }

            newNoteStatus = "на память";
            XXNextDay.SelectedIndex = -1;
            XXNextMonth.SelectedIndex = -1;
            XXNextYear.SelectedIndex = -1;
        }

        private void XXArchive_Checked(object sender, RoutedEventArgs e)
        {
            if(newNotePriority == "высокий")
            {
                MessageBox.Show("Приоритет заметки выставлен как \"высокий\".\nЧто бы выставить статус \"архив\", сначала измените значение приоритета.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                XXArchive.IsChecked = false;
                XXInWork.IsChecked = true;
            }
            newNoteStatus = "архив";
            XXNextDay.SelectedIndex = -1;
            XXNextMonth.SelectedIndex = -1;
            XXNextYear.SelectedIndex = -1;
        }
        #endregion

        #region // Кнопка "Отмена"
        private void XXCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainWindow = new MainWindow();
                foreach (Window window in App.Current.Windows)
                {
                    if (window is WindowNewNote)
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
        private void XXOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заголовка заметки, если пустой, в конструктор идёт значение "без названия".
                if (XXThemOfNote.Text.Length > 0)
                {
                    newNoteTitle = XXThemOfNote.Text;
                }
                else
                {
                    newNoteTitle = "без названия";
                }

                // Проверка тела заметки, обязательно должно быть заполнено.
                if (XXNewNote.Text.Length > 0)
                {
                    newNoteBody = XXNewNote.Text;
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
                    if (XXNextYear.SelectedIndex == -1)
                    {
                        year = DateTime.Now.Year;
                    }
                    else
                    {
                        year = (int)XXNextYear.SelectedValue;
                    }

                    // Инициализация месяца.
                    if (XXNextMonth.SelectedIndex == -1)
                    {
                        month = DateTime.Now.Month;
                    }
                    else
                    {
                        month = XXNextMonth.SelectedIndex + 1;
                    }

                    // Инициализация дня.
                    if (XXNextDay.SelectedIndex == -1) // Если день не выбран - значит либо соответствующий статус, либо нет нужды, все значения по умолчанию.
                    {
                        year = 0001;
                        month = 01;
                        day = 01;
                    }
                    else
                    {
                        day = (int)XXNextDay.SelectedValue;
                    }

                    // Инициализация даты следующего обращения к заметке.
                    newNextDate = new DateTime(year, month, day);

                    if (newNextDate <= DateTime.Now && newNextDate != DateTime.MinValue) // Если дата следующего обращения к заметке меньше текущей.
                    {
                        newNextDate = DateTime.Now.AddDays(1);
                    }
                }
                catch (Exception z)
                {
                    MessageBox.Show(z.Message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Создаём экземпляр заметки и добавляем его в общую коллекцию заметок.
                DataBaseOfNotes.AllNotesCollection.Add(new SinglNote(newTimeNewNote, newNoteTitle,
                    newNoteBody, newNotePriority, newNoteStatus, newNextDate));

                save?.Invoke(DataBaseOfNotes.AllNotesCollection);

                // Возврат в главное окно программы.
                MainWindow mainWindow = new MainWindow();
                foreach (Window window in App.Current.Windows)
                {
                    if (window is WindowNewNote)
                        window.Close();
                }
                mainWindow.Show();

            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// Выбор дня даты следующего просмотра заметки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XXNextDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка статуса заметки и сброс даты если статусы "Архив" и "На память".
            if(XXForMemory.IsChecked == true)
            {
                XXArchive.IsChecked = true;
                XXForMemory.IsChecked = true;
            }
            if (XXArchive.IsChecked == true)
            {
                XXForMemory.IsChecked = true;
                XXArchive.IsChecked = true;
            }
        }

        /// <summary>
        /// Выбор месяца даты следующего просмотра заметки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XXNextMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка статуса заметки и сброс даты если статусы "Архив" и "На память".
            if (XXForMemory.IsChecked == true)
            {
                XXArchive.IsChecked = true;
                XXForMemory.IsChecked = true;
            }
            if (XXArchive.IsChecked == true)
            {
                XXForMemory.IsChecked = true;
                XXArchive.IsChecked = true;
            }
        }

        /// <summary>
        /// Выбор года даты следующего просмотра заметки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void XXNextYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверка статуса заметки и сброс даты если статусы "Архив" и "На память".
            if (XXForMemory.IsChecked == true)
            {
                XXArchive.IsChecked = true;
                XXForMemory.IsChecked = true;
            }
            if (XXArchive.IsChecked == true)
            {
                XXForMemory.IsChecked = true;
                XXArchive.IsChecked = true;
            }
        }
    }
}
