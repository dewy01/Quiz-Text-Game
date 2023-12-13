﻿using System;
using System.Collections.Generic;
using System.IO;
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

namespace Quiz_Gui_Game
{
    /// <summary>
    /// Interaction logic for DeleteQuizContent.xaml
    /// </summary>
    public partial class DeleteQuizContent : Page
    {
        private int selectedFileIndex = -1;
        private List<QuizInfo> quizzes;
        private string folderPath = "../../Data/";
        public string[] files;

        public DeleteQuizContent()
        {
            InitializeComponent();
            LoadQuizzes();
            UpdateUI();
        }

        private void LoadQuizzes()
        {
            try
            {
                files = Directory.GetFiles(folderPath);
                quizzes = new List<QuizInfo>();

                foreach (var file in files)
                {
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Substring(folderPath.Length));
                    quizzes.Add(new QuizInfo { QuizName = fileName, FilePath = file });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading quizzes: {ex.Message}");
                quizzes = new List<QuizInfo>();
            }
        }

        private void UpdateUI()
        {
            QuizzesListBox.ItemsSource = null;
            QuizzesListBox.ItemsSource = quizzes;
            
        }

        private void QuizzesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFileIndex = QuizzesListBox.SelectedIndex;
        }

        private void DeleteQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFileIndex >= 0 && selectedFileIndex < quizzes.Count)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this quiz?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string filePathToDelete = files[selectedFileIndex];

                    try
                    {
                        System.IO.File.Delete(filePathToDelete);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting file: {ex.Message}");
                    }

                    LoadQuizzes();
                    UpdateUI();
                }
            }
            else
            {
                MessageBox.Show("Please select a quiz to delete.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }

}
