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
        public string document;
        public List<Subject> subjectsList;
        public List<Group> groupList;
        public List<Student> studentsList;
        public List<StudentsInGroup> studentsInGroupList;
        //public List<Attestation> attestationList;        

        public CreateDocWindow()
        {
            InitializeComponent();            
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<string> docsList = new List<string> { "Справка об успеваемости", "Приложение к диплому", "Ведомость по предмету" };
            subjectsList = db.Subject.ToList();
            groupList = db.Group.ToList();
            studentsList = db.Student.ToList();
            studentsInGroupList = db.StudentsInGroup.ToList();
            //attestationList = db.Attestation.ToList();
            docsComboBox.ItemsSource = docsList;
            //docsComboBox.SelectedItem = docsList[0];
            //groupComboBox.SelectedItem = groupList[0];
            LoadGroups();
            LoadStudents();
            LoadSubjects();
            groupComboBox.IsEnabled = true;
            studentComboBox.IsEnabled = true;
            subjectComboBox.IsEnabled = true;
        }
                
        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Сформировать документ");
        }

        private void DocsComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            document = docsComboBox.SelectedItem.ToString();
            switch (document)
            {
                case "Справка об успеваемости":                    
                    LoadGroups();                    
                    LoadStudents();                    
                    groupComboBox.IsEnabled = true;
                    studentComboBox.IsEnabled = true;
                    subjectComboBox.IsEnabled = false;
                    break;

                case "Приложение к диплому":
                    LoadGroups();                    
                    LoadStudents();
                    groupComboBox.IsEnabled = true;
                    studentComboBox.IsEnabled = true;
                    subjectComboBox.IsEnabled = false;
                    break;

                case "Ведомость по предмету":
                    LoadSubjects();
                    groupComboBox.IsEnabled = false;
                    studentComboBox.IsEnabled = false;
                    subjectComboBox.IsEnabled = true;
                    break;
            }
        }

        private void GroupComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group group = groupComboBox.SelectedItem as Group;
            List<Student> studentsInCurrentGroup = new List<Student>();
            foreach (StudentsInGroup record in studentsInGroupList)
            {
                if (record.GroupId == group.GroupId)
                {
                    foreach (Student student in studentsList)
                    {
                        if (record.StudentId == student.StudentId) 
                        { 
                            studentsInCurrentGroup.Add(student);
                        }
                        continue;
                    }
                }
            }
            studentComboBox.ItemsSource = studentsInCurrentGroup;
        }

        private void StudentComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Student student = studentComboBox.SelectedItem as Student;
            foreach (StudentsInGroup record in studentsInGroupList)
            {
                if (record.StudentId == student.StudentId)
                {
                    foreach (Group group in groupList)
                    {
                        if (record.GroupId == group.GroupId) { groupComboBox.SelectedItem = group; }
                    }
                }
            }
        }

        private void LoadGroups() 
        {
            groupComboBox.ItemsSource = groupList;
        }

        private void LoadStudents()
        {
            studentComboBox.ItemsSource = studentsList;
        }

        private void LoadSubjects()
        {
            subjectComboBox.ItemsSource = subjectsList;
        }

    }
}
