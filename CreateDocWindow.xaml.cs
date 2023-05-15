using System;
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
        public List<Attestation> attestationList;

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
            attestationList = db.Attestation.ToList();
            docsComboBox.ItemsSource = docsList;            
            LoadGroups();
            LoadStudents();
            LoadSubjects();
            groupComboBox.IsEnabled = true;
            studentComboBox.IsEnabled = true;
            subjectComboBox.IsEnabled = true;
        }
                
        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            if (docsComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите документ, который хотите создать.", "Ошибка");
                return;
            }
            document = docsComboBox.SelectedItem.ToString();
            Group group;
            Student student;
            
            switch (document)
            {
                case "Справка об успеваемости":
                    subjectComboBox.IsEnabled = false;
                    group = groupComboBox.SelectedItem as Group;
                    student = studentComboBox.SelectedItem as Student;

                    if (group != null && student != null)
                    {
                        string[] header = new string[] { "\n                  СПРАВКА ОБ УСПЕВАЕМОСТИ\n", $" Студент: {student.StudentFIO}", $" Группа: {group.GroupId}\n", string.Format("{0,-22}{1,-10}{2,-5}{3,-22}{4,-12:d}{5,-20}", "ПРЕДМЕТ", "ФОРМАТ", "БАЛЛ", "ОЦЕНКА", "ДАТА", "ПРЕПОДАВАТЕЛЬ") };
                        List<string> body = new List<string>();

                        foreach (Attestation attestation in attestationList)
                        {
                            if (attestation.StudentId == student.StudentId)
                            {
                                Subject currentSubject = db.Subject.Find(attestation.SubjectId);
                                Teacher currentTeacher = db.Teacher.Find(attestation.TeacherId);
                                body.Add(string.Format("{0,-22}{1,-10}{2,-5}{3,-22}{4,-12:d}{5,-20}", currentSubject.SubjectName, attestation.AttestationTypeName, attestation.Mark.ToString(), attestation.Result, attestation.AttestationDate.Date.ToString("d"), currentTeacher.TeacherFIO));
                            }                            
                        }
                        if (body.Count != 0)
                        {
                            body.Add($"\n Дата выдачи: {DateTime.Now.Date:d}");
                            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Справка об успеваемости {group.GroupId} {student.StudentFIO}.txt");
                            File.AppendAllLines(path, header);
                            File.AppendAllLines(path, body);
                            File.OpenRead(path);
                            MessageBox.Show($"Документ \nСправка об успеваемости {group.GroupId} {student.StudentFIO}.txt \nуспешно создан или изменен на Рабочем столе.", "Сообщение");
                        }
                        else { MessageBox.Show("Не найдено аттестаций для указанного студента.", "Сообщение"); }
                    }
                    else { MessageBox.Show("Не все данные выбраны. Для этого документа нужно выбрать и группу, и студента.", "Ошибка"); }
                    break;

                case "Приложение к диплому":
                    subjectComboBox.IsEnabled = false;
                    group = groupComboBox.SelectedItem as Group;
                    student = studentComboBox.SelectedItem as Student;
                    if (group != null && student != null)
                    {
                        //содержит фио студента, название предмета и результат (без баллов)
                        string[] header = new string[] { "\n                  ПРИЛОЖЕНИЕ К ДИПЛОМУ\n", $" Студент: {student.StudentFIO}", $" Группа: {group.GroupId}\n", string.Format("{0,-22}{1,-10}{2,-22}", "ПРЕДМЕТ", "ФОРМАТ", "ОЦЕНКА") };
                        List<string> body = new List<string>();

                        foreach (Attestation attestation in attestationList)
                        {
                            if (attestation.StudentId == student.StudentId)
                            {
                                Subject currentSubject = db.Subject.Find(attestation.SubjectId);                                
                                body.Add(string.Format("{0,-22}{1,-10}{2,-22}", currentSubject.SubjectName, attestation.AttestationTypeName, attestation.Result));
                            }
                        }
                        if (body.Count != 0)
                        {
                            body.Add($"\n Дата выдачи: {DateTime.Now.Date:d}");
                            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Приложение к диплому {group.GroupId} {student.StudentFIO}.txt");                            
                            File.AppendAllLines(path, header);
                            File.AppendAllLines(path, body);
                            File.OpenRead(path);
                            MessageBox.Show($"Документ \nПриложение к диплому {group.GroupId} {student.StudentFIO}.txt \nуспешно создан или изменен на Рабочем столе.", "Сообщение");
                        }
                        else { MessageBox.Show("Не найдено аттестаций для указанного студента.", "Сообщение"); }
                    }
                    else { MessageBox.Show("Не все данные выбраны. Для этого документа нужно выбрать и группу, и студента.", "Ошибка"); }
                    break;

                case "Ведомость по предмету":
                    groupComboBox.IsEnabled = false;
                    studentComboBox.IsEnabled = false;
                    Subject subject = subjectComboBox.SelectedItem as Subject;
                    if (subject != null)
                    {
                        string[] header = new string[] { "\n                  ВЕДОМОСТЬ ПО ПРЕДМЕТУ\n", $" Предмет: {subject.SubjectName}\n", string.Format("{0,-10}{1,-40}{2,-10}{3,-5}{4,-22}{5,-12:d}{6,-20}", "ГРУППА", "СТУДЕНТ", "ФОРМАТ", "БАЛЛ", "ОЦЕНКА", "ДАТА", "ПРЕПОДАВАТЕЛЬ") };
                        List<string> body = new List<string>();

                        foreach (Attestation attestation in attestationList)
                        {
                            if (attestation.SubjectId == subject.SubjectId)
                            {
                                Student currentStudent = db.Student.Find(attestation.StudentId);
                                Teacher currentTeacher = db.Teacher.Find(attestation.TeacherId);
                                Group currentGroup = new Group();
                                foreach (StudentsInGroup record in studentsInGroupList)
                                {
                                    if (currentStudent.StudentId == record.StudentId)
                                    {
                                        currentGroup = db.Group.Find(record.GroupId);
                                    }
                                }
                                body.Add(string.Format("{0,-10}{1,-40}{2,-10}{3,-5}{4,-22}{5,-12:d}{6,-20}", currentGroup.GroupId, currentStudent.StudentFIO, attestation.AttestationTypeName, attestation.Mark.ToString(), attestation.Result, attestation.AttestationDate.Date.ToString("d"), currentTeacher.TeacherFIO));
                            }
                        }
                        if (body.Count != 0)
                        {
                            body.Add($"\n Дата выдачи: {DateTime.Now.Date:d}");
                            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Ведомость по предмету {subject.SubjectName}.txt");
                            File.AppendAllLines(path, header);
                            File.AppendAllLines(path, body);
                            File.OpenRead(path);
                            MessageBox.Show($"Документ \nВедомость по предмету {subject.SubjectName}.txt \nуспешно создан или изменен на Рабочем столе.", "Сообщение");
                        }
                        else { MessageBox.Show("Не найдено аттестаций для указанного предмета.", "Сообщение"); }
                    }
                    else { MessageBox.Show("Не все данные выбраны. Для этого документа нужно выбрать предмет.", "Ошибка"); }
                    break;
            }
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
            if (groupComboBox.SelectedItem != null)
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
        }

        private void StudentComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (studentComboBox.SelectedItem != null)
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
