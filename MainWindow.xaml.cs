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


namespace Exams_performance
{
    public partial class MainWindow : Window
    {
        public AppContext db = new AppContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        public string student_fio, teacher_fio, subject_name;
        private void GridLoaded(object sender, RoutedEventArgs e) //при загрузке окна подгружаются все данные из таблицы Аттестация бд
        {
            List<Attestation> attestation_data = db.Attestation.ToList();
            List<AttestationItem> attestation_item_list = new List<AttestationItem>();

            for (int i = 0; i < attestation_data.Count; i++)
            {
                student_fio = db.Student.Find(attestation_data[i].student_id).Student_fio;
                teacher_fio = db.Teacher.Find(attestation_data[i].teacher_id).Teacher_fio;
                subject_name = db.Subject.Find(attestation_data[i].subject_id).Subject_name;

                attestation_item_list.Add(new AttestationItem(student_fio, teacher_fio, subject_name, attestation_data[i].attestation_date, attestation_data[i].attestation_type, attestation_data[i].mark, attestation_data[i].result));
            }

            attestations_Grid.ItemsSource = attestation_item_list;
            


            //var qwe = db.Attestation.Local.ToBindingList();
            
        }

        //private void GridMouse_Up(object sender, MouseButtonEventArgs e) // действие при одинарном клике на строку
        //{
        //    AttestationItem selected_attestation = attestations_Grid.SelectedItem as AttestationItem;
        //    //MessageBox.Show(" Студент: " + selected_attestation.student_fio + "\n Преподаватель: " + selected_attestation.teacher_fio + "\n Предмет: " + selected_attestation.subject_name + "\n Дата: " + selected_attestation.attestation_date);
        //}

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Нажали кнопку Добавить новую запись");
            AddWindow add_window = new AddWindow();
            add_window.Show();
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
            MessageBox.Show("Нажали кнопку Сформировать документ");
            CreateDocWindow add_window = new CreateDocWindow();
            add_window.Show();
        }
    }
}
