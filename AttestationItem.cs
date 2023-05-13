using System;
using System.Collections.Generic;

namespace ExamsPerformance
{
    public class AttestationItem
    {
        public AttestationItem(Student student, Teacher teacher, Subject subject, DateTime attestationDate, string attestationTypeName, int? mark, string result)
        {
            this.Student = student;
            this.Teacher = teacher;
            this.Subject = subject;
            this.AttestationDate = attestationDate;
            this.AttestationTypeName = attestationTypeName;
            this.Mark = mark;
            this.Result = result;
        }

        public AppContext db = new AppContext();

        public Student Student { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public DateTime AttestationDate { get; set; }
        public string AttestationTypeName { get; set; }
        public int? Mark { get; set; }
        public string Result { get; set; }

        public string GetStudentFIO(Student student)
        {
            return student.StudentFIO;
        }

        public string GetTeacherFIO(Teacher teacher)
        {
            return teacher.TeacherFIO;
        }

        public string GetSubjectName(Subject subject)
        {
            return subject.SubjectName;
        }

    }
}



