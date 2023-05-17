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
        public Student student;
        public Teacher teacher;
        public Subject subject;
        public AttestationType attestationType;

        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            bool markIsInt = false;
            student = studentFIOComboBox.SelectedItem as Student;
            teacher = teacherFIOComboBox.SelectedItem as Teacher;
            subject = subjectComboBox.SelectedItem as Subject;
            attestationType = attestationTypeComboBox.SelectedItem as AttestationType;
            string studentFIO = studentFIOComboBox.Text.Trim();
            string teacherFIO = teacherFIOComboBox.Text.Trim();
            string subjectName = subjectComboBox.Text.Trim();
            string attestationTypeName = attestationTypeComboBox.Text.Trim();
            string result = resultComboBox.Text.Trim();

            string markStr = markTextBox.Text.Trim();
            int? mark = null;

            bool requiredFields = CheckRequiredFields(studentFIO, teacherFIO, subjectName, attestationTypeName);



            if (requiredFields == true)
            {
                if (markStr == string.Empty && result != "неявка" && result != string.Empty)
                {
                    correct = false;
                    markTextBox.Clear();
                    markTextBox.ToolTip = "Не указано количество баллов";
                    MessageBox.Show("Количество баллов не указывается только в случае неявки");
                    return;
                }
                else if (markStr == string.Empty && result == string.Empty)
                {
                    markTextBox.Background = Brushes.Transparent;
                    correct = true;
                }
                else if (markStr != string.Empty)
                {
                    int number;
                    markIsInt = int.TryParse(markStr, out number);

                    if (markIsInt == false && markStr.Length != 0)
                    {
                        markTextBox.ToolTip = "Количество баллов должно быть числом от 0 до 100 включительно";
                        markTextBox.Background = Brushes.Pink;
                        return;
                    }
                    else
                    {
                        mark = number;
                        correct = CheckMarkMatchResult(mark, result, correct);
                    }
                }
            }
            else { return; }

            DateTime attestationDate = attestationDateDatePicker.SelectedDate.Value;

            if (correct == true && (markIsInt == true || mark.HasValue == false))
            {
                resultComboBox.Background = Brushes.Transparent;

                Attestation attestation = new Attestation(student.StudentId, teacher.TeacherId, subject.SubjectId, attestationDate, attestationType.AttestationTypeName, mark, result);
                db.Attestation.Add(attestation);
                db.SaveChanges();

                MessageBox.Show("Новая запись успешно добавлена!", "Сообщение");
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<Student> studentsList = db.Student.ToList();
            studentFIOComboBox.ItemsSource = studentsList;

            List<Teacher> teachersList = db.Teacher.ToList();
            teacherFIOComboBox.ItemsSource = teachersList;

            List<Subject> subjectsList = db.Subject.ToList();
            subjectComboBox.ItemsSource = subjectsList;

            List<AttestationType> attestationTypeList = db.AttestationType.ToList();
            attestationTypeComboBox.ItemsSource = attestationTypeList;

            var resultList = new List<string>() { "неудовлетворительно", "удовлетворительно", "хорошо", "отлично", "неявка" };
            resultComboBox.ItemsSource = resultList;
        }

        private bool CheckMarkMatchResult(int? mark, string result, bool correct)
        {
            if (mark < 0 || mark > 100)
            {
                correct = false;
                markTextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                markTextBox.Background = Brushes.Pink;
            }
            else
            {
                markTextBox.ToolTip = "Количество баллов от 0 до 100 включительно";
                markTextBox.Background = Brushes.Transparent;

                if (mark < 62 && result != "неудовлетворительно")
                {
                    correct = false;
                    resultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"неудовлетворительно\"";
                    resultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 61 && mark < 76 && result != "удовлетворительно")
                {
                    correct = false;
                    resultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"удовлетворительно\"";
                    resultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 75 && mark < 91 && result != "хорошо")
                {
                    correct = false;
                    resultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"хорошо\"";
                    resultComboBox.Background = Brushes.Pink;
                }
                else if (mark > 90 && result != "отлично")
                {
                    correct = false;
                    resultComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"отлично\"";
                    resultComboBox.Background = Brushes.Pink;
                }
            }
            return correct;
        }

        private bool CheckRequiredFields(string studentFIO, string teacherFIO, string subjectName, string attestationTypeName)
        {
            bool requiredFields = true;
            studentFIOComboBox.Background = Brushes.Transparent;
            teacherFIOComboBox.Background = Brushes.Transparent;
            subjectComboBox.Background = Brushes.Transparent;
            attestationTypeComboBox.Background = Brushes.Transparent;
            attestationDateDatePicker.Background = Brushes.Transparent;

            if (studentFIO == string.Empty)
            {
                requiredFields = false;
                studentFIOComboBox.ToolTip = "Заполните это поле";
                studentFIOComboBox.Background = Brushes.Pink;
            }
            if (teacherFIO == string.Empty)
            {
                requiredFields = false;
                teacherFIOComboBox.ToolTip = "Заполните это поле";
                teacherFIOComboBox.Background = Brushes.Pink; ;
            }
            if (subjectName == string.Empty)
            {
                requiredFields = false;
                subjectComboBox.ToolTip = "Заполните это поле";
                subjectComboBox.Background = Brushes.Pink; ;
            }
            if (attestationTypeName == string.Empty)
            {
                requiredFields = false;
                attestationTypeComboBox.ToolTip = "Заполните это поле";
                attestationTypeComboBox.Background = Brushes.Pink; ;
            }
            if (attestationDateDatePicker.SelectedDate.HasValue == false)
            {
                requiredFields = false;
                attestationDateDatePicker.ToolTip = "Заполните это поле";
                attestationDateDatePicker.Background = Brushes.Pink;
            }

            return requiredFields;
        }

        private void MarkTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            string markStr = markTextBox.Text.Trim();
            bool markIsInt;

            if (markStr != string.Empty)
            {
                int number;
                markIsInt = int.TryParse(markStr, out number);

                if (markIsInt == false && markStr.Length != 0)
                {
                    markTextBox.ToolTip = "Количество баллов должно быть числом от 0 до 100 включительно";
                    markTextBox.Background = Brushes.Pink;
                    return;
                }
            }
            else { markTextBox.Background = Brushes.Transparent; }
        }
    }
}


