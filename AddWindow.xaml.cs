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
    public partial class AddWindow : Window
    {
        public AppContext db = new AppContext();
        int student_id, teacher_id, subject_id;

        public AddWindow()
        {
            InitializeComponent();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            bool correct = false;
            bool mark_is_int = false;
            string student_fio = StudentFIO_ComboBox.Text.Trim();
            string teacher_fio = TeacherFIO_ComboBox.Text.Trim();
            string subject_name = Subject_ComboBox.Text.Trim();
            string attestation_type = Attestation_type_ComboBox.Text.Trim();         
            string result = Result_ComboBox.Text.Trim();

            string mark_str = Mark_TextBox.Text.Trim();
            int? mark = null;

            bool required_fields = CheckRequiredFields(student_fio, teacher_fio, subject_name, attestation_type);

            if (required_fields == true)
            {

                if (mark_str == string.Empty && result != "неявка" && result != string.Empty)
                {
                    correct = false;
                    Mark_TextBox.Clear();
                    Mark_TextBox.ToolTip = "Не указано количество баллов";
                    MessageBox.Show("Количество баллов не указывается только в случае неявки");
                }
                else if (mark_str == string.Empty && result == string.Empty)
                {
                    correct = true;
                }
                else if (mark_str != string.Empty)
                {
                    int number;
                    mark_is_int = int.TryParse(mark_str, out number);

                    if (mark_is_int == false)
                    {
                        Mark_TextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                        Mark_TextBox.Background = Brushes.Pink;
                    }
                    else
                    {
                        mark = number;
                        correct = CheckMarkMatchResult(mark, result, correct);
                    }
                }
            }
            else { return; }

            DateTime attestation_date = AttestationData_DataPicker.SelectedDate.Value;

            if (correct == true && (mark_is_int == true || mark.HasValue == false))
            {
                Result_ComboBox.Background = Brushes.Transparent;

                List<Student> students_list = db.Student.ToList();
                foreach (var item in students_list)
                {
                    if (item.Student_fio == student_fio)
                    {
                        student_id = item.student_id;
                    }
                }

                List<Teacher> teachers_list = db.Teacher.ToList();
                foreach (var item in teachers_list)
                {
                    if (item.Teacher_fio == teacher_fio)
                    {
                        teacher_id = item.teacher_id;
                    }
                }

                List<Subject> subjects_list = db.Subject.ToList();
                foreach (var item in subjects_list)
                {
                    if (item.Subject_name == subject_name)
                    {
                        subject_id = item.subject_id;
                    }
                }

                //MessageBox.Show($"student {student_id}, teacher {teacher_id}, subject {subject_id}");

                Attestation attestation = new Attestation(student_id, teacher_id, subject_id, attestation_date, attestation_type, mark, result);
                db.Attestation.Add(attestation);
                db.SaveChanges();

                MessageBox.Show("Новая запись успешно добавлена!");
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Отменить");
            MainWindow main_window = new MainWindow();
            main_window.Show();
            Hide();
        }

        private void DataLoadingToComboBoxes(object sender, RoutedEventArgs e)
        {
            List<Student> students_list = db.Student.ToList();
            StudentFIO_ComboBox.ItemsSource = students_list;

            List<Teacher> teachers_list = db.Teacher.ToList();
            TeacherFIO_ComboBox.ItemsSource = teachers_list;

            List<Subject> subjects_list = db.Subject.ToList();
            Subject_ComboBox.ItemsSource = subjects_list;

            List<Attestation_type> attestation_type_list = db.Attestation_type.ToList();
            Attestation_type_ComboBox.ItemsSource = attestation_type_list;

            var result_list = new List<string>() { "неудовлетворительно", "удовлетворительно", "хорошо", "отлично", "неявка" };
            Result_ComboBox.ItemsSource = result_list;
        }

        private bool CheckMarkMatchResult(int? mark, string result, bool correct)
        {
            if (mark < 0 || mark > 100)
            {
                correct = false;
                Mark_TextBox.ToolTip = "Количество баллов должно быть от 0 до 100 включительно";
                Mark_TextBox.Background = Brushes.Pink;
            }
            else
            {
                Mark_TextBox.ToolTip = "Количество баллов от 0 до 100 включительно";
                Mark_TextBox.Background = Brushes.Transparent;

                if (mark < 62 && result != "неудовлетворительно")
                {
                    correct = false;
                    Result_ComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"неудовлетворительно\"";
                    Result_ComboBox.Background = Brushes.Pink;
                }
                else if (mark > 61 && mark < 76 && result != "удовлетворительно")
                {
                    correct = false;
                    Result_ComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"удовлетворительно\"";
                    Result_ComboBox.Background = Brushes.Pink;
                }
                else if (mark > 75 && mark < 91 && result != "хорошо")
                {
                    correct = false;
                    Result_ComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"хорошо\"";
                    Result_ComboBox.Background = Brushes.Pink;
                }
                else if (mark > 90 && result != "отлично")
                {
                    correct = false;
                    Result_ComboBox.ToolTip = "Этому количеству баллов соответствует оценка \"отлично\"";
                    Result_ComboBox.Background = Brushes.Pink;
                }

            }
            return correct;
        }

        private bool CheckRequiredFields(string student_fio, string teacher_fio, string subject_name, string attestation_type)
        {
            bool required_fields = true;
            StudentFIO_ComboBox.Background = Brushes.Transparent;
            TeacherFIO_ComboBox.Background = Brushes.Transparent;
            Subject_ComboBox.Background = Brushes.Transparent;
            Attestation_type_ComboBox.Background = Brushes.Transparent;
            AttestationData_DataPicker.Background = Brushes.Transparent;

            if (student_fio == string.Empty)
            {
                required_fields = false;
                StudentFIO_ComboBox.ToolTip = "Заполните это поле";
                StudentFIO_ComboBox.Background = Brushes.Pink;
            }
            if (teacher_fio == string.Empty)
            {
                required_fields = false;
                TeacherFIO_ComboBox.ToolTip = "Заполните это поле";
                TeacherFIO_ComboBox.Background = Brushes.Pink; ;
            }
            if (subject_name == string.Empty)
            {
                required_fields = false;
                Subject_ComboBox.ToolTip = "Заполните это поле";
                Subject_ComboBox.Background = Brushes.Pink; ;
            }
            if (attestation_type == string.Empty)
            {
                required_fields = false;
                Attestation_type_ComboBox.ToolTip = "Заполните это поле";
                Attestation_type_ComboBox.Background = Brushes.Pink; ;
            }
            if (AttestationData_DataPicker.SelectedDate.HasValue == false)
            {
                required_fields = false;
                AttestationData_DataPicker.ToolTip = "Заполните это поле";
                AttestationData_DataPicker.Background = Brushes.Pink;
            }

            return required_fields;
        }
    }
}


