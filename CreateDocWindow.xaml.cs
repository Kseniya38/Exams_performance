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

namespace ExamsPerformance
{
    public partial class CreateDocWindow : Window
    {
        public AppContext db = new AppContext();
        public CreateDocWindow()
        {
            InitializeComponent();
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<string> docsList = new List<string> { "Справка об успеваемости", "Приложение к диплому", "Ведомость по предмету" };
            DocsComboBox.ItemsSource = docsList;

            List<Student> studentsList = db.Student.ToList();
            StudentComboBox.ItemsSource = studentsList;

            List<Subject> subjectsList = db.Subject.ToList();
            SubjectComboBox.ItemsSource = subjectsList;

            List<Group> groupList = db.Group.ToList();
            GroupComboBox.ItemsSource = groupList;
        }
        

        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Сформировать документ");
        }
    }
}
