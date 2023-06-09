﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ExamsPerformance
{
    public partial class MainWindow : Window
    {
        public AppContext db = new AppContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        public Student student;
        public Teacher teacher;
        public Subject subject;
        public AttestationItem selectedAttestation;
        public List<AttestationItem> attestationItemList = new List<AttestationItem>();
        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            List<Attestation> attestationData = db.Attestation.ToList();
            
            for (int i = 0; i < attestationData.Count; i++)
            {
                student = db.Student.Find(attestationData[i].StudentId);
                teacher = db.Teacher.Find(attestationData[i].TeacherId);
                subject = db.Subject.Find(attestationData[i].SubjectId);

                attestationItemList.Add(new AttestationItem(student, teacher, subject, attestationData[i].AttestationDate, attestationData[i].AttestationTypeName, attestationData[i].Mark, attestationData[i].Result));
            }

            attestationsGrid.ItemsSource = attestationItemList;
        }

        private void GridMouseUp(object sender, MouseButtonEventArgs e)
        {
            selectedAttestation = attestationsGrid.SelectedItem as AttestationItem;           
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            Hide();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (attestationsGrid.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана запись. Необходимо выбрать в таблице запись, которую вы хотите удалить.", "Ошибка");
                return;
            }

            AttestationItem selectedAttestation = attestationsGrid.SelectedItem as AttestationItem;
            if (MessageBox.Show("Вы уверены, что хотите удалить следующую запись?\nОтменить это действие будет невозможно.\n" + "\n Студент: " + selectedAttestation.Student.StudentFIO + "\n Преподаватель: " + selectedAttestation.Teacher.TeacherFIO + "\n Предмет: " + selectedAttestation.Subject.SubjectName + "\n Дата: " + selectedAttestation.AttestationDate,
                    "Предупреждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Attestation attestation = db.Attestation.Find(selectedAttestation.Student.StudentId, selectedAttestation.Subject.SubjectId, selectedAttestation.AttestationDate);
                MessageBox.Show($"StudentId: {attestation.StudentId}, SubjectId: {attestation.SubjectId}, TeacherId: {attestation.TeacherId}");
                db.Attestation.Remove(attestation);
                db.SaveChanges();
                MessageBox.Show("Запись успешно удалена!", "Сообщение");
            }
            List<Attestation> attestationData = db.Attestation.ToList();
            List<AttestationItem> attestationItemList = new List<AttestationItem>();

            for (int i = 0; i < attestationData.Count; i++)
            {
                student = db.Student.Find(attestationData[i].StudentId);
                teacher = db.Teacher.Find(attestationData[i].TeacherId);
                subject = db.Subject.Find(attestationData[i].SubjectId);

                attestationItemList.Add(new AttestationItem(student, teacher, subject, attestationData[i].AttestationDate, attestationData[i].AttestationTypeName, attestationData[i].Mark, attestationData[i].Result));
            }

            attestationsGrid.ItemsSource = attestationItemList;
            attestationsGrid.Items.Refresh();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {            
            int selectedIndex = attestationsGrid.SelectedIndex;

            if (selectedIndex != -1)
            {
                EditWindow editWindow = new EditWindow(this, selectedIndex);
                editWindow.Show();
            }
            else { MessageBox.Show("Не выбрана запись. Необходимо выбрать в таблице запись, которую вы хотите изменить.", "Ошибка"); }
        }

        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            CreateDocWindow addWindow = new CreateDocWindow();
            addWindow.Show();
        }
        
    }
}
