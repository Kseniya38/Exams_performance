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

namespace Exams_performance
{
    /// <summary>
    /// Логика взаимодействия для CreateDocWindow.xaml
    /// </summary>
    public partial class CreateDocWindow : Window
    {
        public AppContext db = new AppContext();
        public CreateDocWindow()
        {
            InitializeComponent();
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<string> docs_list = new List<string> { "Справка об успеваемости", "Приложение к диплому", "Ведомость по предмету" };
            Docs_ComboBox.ItemsSource = docs_list;

            List<Student> students_list = db.Student.ToList();
            Student_ComboBox.ItemsSource = students_list;

            List<Subject> subjects_list = db.Subject.ToList();
            Subject_ComboBox.ItemsSource = subjects_list;

            List<Group> group_list = db.Group.ToList();
            Group_ComboBox.ItemsSource = group_list;
        }
        

        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Сформировать документ");
        }
    }
}
