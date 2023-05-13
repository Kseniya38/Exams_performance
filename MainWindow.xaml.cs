using System.Collections.Generic;
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
        private void GridLoaded(object sender, RoutedEventArgs e) 
        {
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
        }
        //var qwe = db.Attestation.Local.ToBindingList();

        //private void GridMouse_Up(object sender, MouseButtonEventArgs e) // действие при одинарном клике на строку
        //{
        //    AttestationItem selected_attestation = attestations_Grid.SelectedItem as AttestationItem;
        //    //MessageBox.Show(" Студент: " + selected_attestation.student_fio + "\n Преподаватель: " + selected_attestation.teacher_fio + "\n Предмет: " + selected_attestation.subject_name + "\n Дата: " + selected_attestation.attestation_date);
        //}

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Нажали кнопку Добавить новую запись");
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
            Hide();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Удалить");
        }

        private void SaveChagesButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажали кнопку Cохранить изменения");
        }

        private void CreateDocButtonClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Нажали кнопку Сформировать документ");
            CreateDocWindow addWindow = new CreateDocWindow();
            addWindow.Show();
        }
    }
}
