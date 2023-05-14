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
    public partial class EditWindow : Window
    {
        public AppContext db = new AppContext();
        public int studentId, teacherId, subjectId;
        public AttestationItem selectedRow;
        public MainWindow mainWindow;
        public EditWindow()
        {
            InitializeComponent();
        }

        public EditWindow(MainWindow mainWindow, int selectedIndex)
        {
            InitializeComponent();

            selectedRow = mainWindow.attestationsGrid.SelectedItem as AttestationItem;
            this.mainWindow = mainWindow;

            List<Student> studentsList = db.Student.ToList();
            studentEditComboBox.ItemsSource = studentsList;
            int studentIndex = -1;
            for (int i = 0; i < studentsList.Count; i++)
            {
                if (selectedRow.Student.StudentId == studentsList[i].StudentId) { studentIndex = i; }
            }
            if (studentIndex != -1) { studentEditComboBox.SelectedItem = studentsList[studentIndex]; }

            List<Teacher> teachersList = db.Teacher.ToList();
            teacherEditComboBox.ItemsSource = teachersList;
            int teacherIndex = -1;
            for (int i = 0; i < teachersList.Count; i++)
            {
                if (selectedRow.Teacher.TeacherId == teachersList[i].TeacherId) { teacherIndex = i; }
            }
            if (teacherIndex != -1) { teacherEditComboBox.SelectedItem = teachersList[teacherIndex]; }

            List<Subject> subjectsList = db.Subject.ToList();
            subjectEditComboBox.ItemsSource = subjectsList;
            int subjectIndex = -1;
            for (int i = 0; i < subjectsList.Count; i++)
            {
                if (selectedRow.Subject.SubjectId == subjectsList[i].SubjectId) { subjectIndex = i; }
            }
            if (subjectIndex != -1) { subjectEditComboBox.SelectedItem = subjectsList[subjectIndex]; }

            List<AttestationType> attestationTypeList = db.AttestationType.ToList();
            attestationTypeEditComboBox.ItemsSource = attestationTypeList;
            int attestationTypeIndex = -1;
            for (int i = 0; i < attestationTypeList.Count; i++)
            {
                if (string.Equals(selectedRow.AttestationTypeName, attestationTypeList[i].AttestationTypeName)) { attestationTypeIndex = i; }
            }
            if (attestationTypeIndex != -1) { attestationTypeEditComboBox.SelectedItem = attestationTypeList[attestationTypeIndex]; }


            List<string> resultList = new List<string>() { "неудовлетворительно", "удовлетворительно", "хорошо", "отлично", "неявка" };
            resultEditComboBox.ItemsSource = resultList;

            attestationDateEditDatePicker.SelectedDate = selectedRow.AttestationDate;
            markEditTextBox.Text = selectedRow.Mark.ToString();
            resultEditComboBox.SelectedItem = selectedRow.Result.ToString();

        }
        private void SaveChangesButtonClick(object sender, RoutedEventArgs e)
        {
            bool correct = true;
            bool markIsInt = false;
            string attestationTypeName = attestationTypeEditComboBox.Text.Trim();
            string result = resultEditComboBox.Text.Trim();

            Student student = studentEditComboBox.SelectedItem as Student;
            Teacher teacher = teacherEditComboBox.SelectedItem as Teacher;
            Subject subject = subjectEditComboBox.SelectedItem as Subject;
            
            string markStr = markEditTextBox.Text.Trim();
            int? mark = null;

            if (markStr == string.Empty && result != "неявка" && result != string.Empty)
            {
                correct = false;
                markEditTextBox.Clear();
                markEditTextBox.ToolTip = "Не указано количество баллов";
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
                    markEditTextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                    markEditTextBox.Background = Brushes.Pink;
                }
                else
                {
                    mark = number;
                    correct = CheckMarkMatchResult(mark, result, correct);
                }
            }

            DateTime attestationDate = attestationDateEditDatePicker.SelectedDate.Value;

            //MessageBox.Show($"New data:\nStudentFIO: {student.StudentFIO},\nTeacherFIO: {teacher.TeacherFIO},\nSubjectName: {subject.SubjectName},\nAttestDate: {attestationDate},\nAttestType: {attestationTypeName},\nMark: {mark},\nResult: {result}");

            if (correct == true && (markIsInt == true || mark.HasValue == false))
            {
                resultEditComboBox.Background = Brushes.Transparent;

                Attestation startAttestation = db.Attestation.Find(selectedRow.Student.StudentId, selectedRow.Subject.SubjectId, selectedRow.AttestationDate);
                db.Attestation.Remove(startAttestation);
                db.SaveChanges();

                Attestation changedAttestation = new Attestation(student.StudentId, teacher.TeacherId, subject.SubjectId, attestationDate, attestationTypeName, mark, result);
                db.Attestation.Add(changedAttestation);
                db.SaveChanges();

                MessageBox.Show("Запись успешно изменена!", "Сообщение");

                List<Attestation> attestationData = db.Attestation.ToList();
                List<AttestationItem> attestationItemList = new List<AttestationItem>();

                for (int i = 0; i < attestationData.Count; i++)
                {
                    student = db.Student.Find(attestationData[i].StudentId);
                    teacher = db.Teacher.Find(attestationData[i].TeacherId);
                    subject = db.Subject.Find(attestationData[i].SubjectId);

                    attestationItemList.Add(new AttestationItem(student, teacher, subject, attestationData[i].AttestationDate, attestationData[i].AttestationTypeName, attestationData[i].Mark, attestationData[i].Result));
                }

                mainWindow.attestationsGrid.ItemsSource = attestationItemList;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {            
            mainWindow.attestationsGrid.Items.Refresh();
            Hide();
        }

        private bool CheckMarkMatchResult(int? mark, string result, bool correct)
        {
            if (mark < 0 || mark > 100)
            {
                correct = false;
                markEditTextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                markEditTextBox.Background = Brushes.Pink;
            }
            else
            {
                markEditTextBox.ToolTip = "Количество баллов от 0 до 100 включительно";
                markEditTextBox.Background = Brushes.Transparent;

                if (mark < 62 && result != "неудовлетворительно")
                {
                    correct = false;
                    resultEditComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"неудовлетворительно\"";
                    resultEditComboBox.Background = Brushes.Pink;
                }
                else if (mark > 61 && mark < 76 && result != "удовлетворительно")
                {
                    correct = false;
                    resultEditComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"удовлетворительно\"";
                    resultEditComboBox.Background = Brushes.Pink;
                }
                else if (mark > 75 && mark < 91 && result != "хорошо")
                {
                    correct = false;
                    resultEditComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"хорошо\"";
                    resultEditComboBox.Background = Brushes.Pink;
                }
                else if (mark > 90 && result != "отлично")
                {
                    correct = false;
                    resultEditComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"отлично\"";
                    resultEditComboBox.Background = Brushes.Pink;
                }
            }

            return correct;
        }
    }
}
