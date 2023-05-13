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
    public partial class AddWindow : Window
    {
        public AppContext db = new AppContext();
        int studentId, teacherId, subjectId;

        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool correct = false;
            bool markIsInt = false;
            string studentFIO = StudentFIOComboBox.Text.Trim();
            string teacherFIO = TeacherFIOComboBox.Text.Trim();
            string subjectName = SubjectComboBox.Text.Trim();
            string attestationType = AttestationTypeComboBox.Text.Trim();         
            string result = ResultComboBox.Text.Trim();

            string markStr = MarkTextBox.Text.Trim();
            int? mark = null;

            bool requiredFields = CheckRequiredFields(studentFIO, teacherFIO, subjectName, attestationType);

            if (requiredFields == true)
            {

                if (markStr == string.Empty && result != "неявка" && result != string.Empty)
                {
                    correct = false;
                    MarkTextBox.Clear();
                    MarkTextBox.ToolTip = "Не указано количество баллов";
                    MessageBox.Show("Количество баллов не указывается только в случае неявки");
                }
                else if (markStr == string.Empty && result == string.Empty)
                {
                    correct = true;
                }
                else if (markStr != string.Empty)
                {
                    int number;
                    markIsInt = int.TryParse(markStr, out number);

                    if (markIsInt == false)
                    {
                        MarkTextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                        MarkTextBox.Background = Brushes.Pink;
                    }
                    else
                    {
                        mark = number;
                        correct = CheckMarkMatchResult(mark, result, correct);
                    }
                }
            }
            else { return; }

            DateTime attestationDate = AttestationDataDataPicker.SelectedDate.Value;

            if (correct == true && (markIsInt == true || mark.HasValue == false))
            {
                ResultComboBox.Background = Brushes.Transparent;

                List<Student> studentsList = db.Student.ToList();
                foreach (var item in studentsList)
                {
                    if (item.StudentFIO == studentFIO)
                    {
                        studentId = item.StudentId;
                    }
                }

                List<Teacher> teachersList = db.Teacher.ToList();
                foreach (var item in teachersList)
                {
                    if (item.TeacherFIO == teacherFIO)
                    {
                        teacherId = item.TeacherId;
                    }
                }

                List<Subject> subjectsList = db.Subject.ToList();
                foreach (var item in subjectsList)
                {
                    if (item.SubjectName == subjectName)
                    {
                        subjectId = item.SubjectId;
                    }
                }

                //MessageBox.Show($"student {student_id}, teacher {teacher_id}, subject {subject_id}");

                Attestation attestation = new Attestation(studentId, teacherId, subjectId, attestationDate, attestationType, mark, result);
                db.Attestation.Add(attestation);
                db.SaveChanges();

                MessageBox.Show("Новая запись успешно добавлена!");
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Отменить");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<Student> studentsList = db.Student.ToList();
            StudentFIOComboBox.ItemsSource = studentsList;

            List<Teacher> teachersList = db.Teacher.ToList();
            TeacherFIOComboBox.ItemsSource = teachersList;

            List<Subject> subjectsList = db.Subject.ToList();
            SubjectComboBox.ItemsSource = subjectsList;

            List<AttestationType> attestationTypeList = db.AttestationType.ToList();
            AttestationTypeComboBox.ItemsSource = attestationTypeList;

            var resultList = new List<string>() { "неудовлетворительно", "удовлетворительно", "хорошо", "отлично", "неявка" };
            ResultComboBox.ItemsSource = resultList;
        }

        private bool CheckMarkMatchResult(int? mark, string result, bool correct)
        {
            if (mark < 0 || mark > 100)
            {
                correct = false;
                MarkTextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                MarkTextBox.Background = Brushes.Pink;
            }
            else
            {
                MarkTextBox.ToolTip = "Количество баллов от 0 до 100 включительно";
                MarkTextBox.Background = Brushes.Transparent;

                if (mark < 62 && result != "неудовлетворительно")
                {
                    correct = false;
                    ResultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"неудовлетворительно\"";
                    ResultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 61 && mark < 76 && result != "удовлетворительно")
                {
                    correct = false;
                    ResultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"удовлетворительно\"";
                    ResultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 75 && mark < 91 && result != "хорошо")
                {
                    correct = false;
                    ResultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"хорошо\"";
                    ResultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 90 && result != "отлично")
                {
                    correct = false;
                    ResultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"отлично\"";
                    ResultComboBox.Background = Brushes.Pink;
                }

            }
            return correct;
        }

        private bool CheckRequiredFields(string studentFIO, string teacherFIO, string subjectName, string attestationType)
        {
            bool requiredFields = true;
            StudentFIOComboBox.Background = Brushes.Transparent;
            TeacherFIOComboBox.Background = Brushes.Transparent;
            SubjectComboBox.Background = Brushes.Transparent;
            AttestationTypeComboBox.Background = Brushes.Transparent;
            AttestationDataDataPicker.Background = Brushes.Transparent;

            if (studentFIO == string.Empty)
            {
                requiredFields = false;
                StudentFIOComboBox.ToolTip = "Заполните это поле";
                StudentFIOComboBox.Background = Brushes.Pink;
            }
            if (teacherFIO == string.Empty)
            {
                requiredFields = false;
                TeacherFIOComboBox.ToolTip = "Заполните это поле";
                TeacherFIOComboBox.Background = Brushes.Pink; ;
            }
            if (subjectName == string.Empty)
            {
                requiredFields = false;
                SubjectComboBox.ToolTip = "Заполните это поле";
                SubjectComboBox.Background = Brushes.Pink; ;
            }
            if (attestationType == string.Empty)
            {
                requiredFields = false;
                AttestationTypeComboBox.ToolTip = "Заполните это поле";
                AttestationTypeComboBox.Background = Brushes.Pink; ;
            }
            if (AttestationDataDataPicker.SelectedDate.HasValue == false)
            {
                requiredFields = false;
                AttestationDataDataPicker.ToolTip = "Заполните это поле";
                AttestationDataDataPicker.Background = Brushes.Pink;
            }

            return requiredFields;
        }
    }
}


