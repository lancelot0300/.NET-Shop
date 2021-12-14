using System.Collections.Generic;
using appv1.DAL.Contexts;
using appv1.DAL.Models;

namespace appv1.Interfaces
{
    public interface IObslugaBazyDanych
    {
        public DziekanatContext Context { get; set; }
        public void DodajZajecia(Zajecia zajecia);
        public void DodajStudenta(Student student);
        void UsunZajecia(int id);

        public List<Student> GetStudents();
        public List<Zajecia> GetCourses();
    }
}