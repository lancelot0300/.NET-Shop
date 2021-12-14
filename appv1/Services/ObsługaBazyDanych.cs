using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appv1.Interfaces;
using appv1.DAL.Models;
using appv1.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace appv1.Services
{
    public class ObslugaBazyDanych : IObslugaBazyDanych
    {
        public DziekanatContext Context { get; set; }

        public void DodajZajecia(Zajecia zajecia)
        {
            if (zajecia.TerminZajec < DateTime.Now)
                throw new ArgumentException("Time must be in a future");


            Context.Zajecia.Add(zajecia);
            Context.SaveChanges();
        }

        public void DodajStudenta(Student student)
        {
            if (!int.TryParse(student.NumerIndeksu, out int indexNumber))
                throw new ArgumentException("Index number must be a number.");
            if (indexNumber > 1000 || indexNumber <= 0)
                throw new ArgumentException("Index number must positive number, less than 1000");


            Context.Students.Add(student);
            Context.SaveChanges();
        }

        public List<Student> GetStudents()
        {
            List<Student> students = Context.Students.ToList();
            return students;
        }
        public List<Zajecia> GetCourses()
        {
            List<Zajecia> course = Context.Zajecia.ToList();
            return course;
        }

        public void UsunZajecia(int id)
        {
            Zajecia course = Context.Zajecia.Find(id);
            Context.Zajecia.Remove(course);
            Context.SaveChanges();
        }
    }
}