using appv1.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appv1.DAL.Contexts
{ 
    public static class DziekanatDatabaseInitializer
    {
        public static void Initialize(DziekanatContext context)
        {
            context.Database.EnsureCreated();

            InitializeStudents(context);
            InitializeCourses(context);
        }

        private static void InitializeStudents(DziekanatContext context)
        {
            if (context.Students.Any())
            {
                return;
            }
            var studenci = new Student[]
            { 
                new Student { NumerIndeksu = "111", Imie = "Adam", Nazwisko = "Nowak" },
                new Student { NumerIndeksu = "222", Imie = "Andrzej", Nazwisko = "Duda" },
                new Student { NumerIndeksu = "333", Imie = "Anna", Nazwisko = "Rożek" },
                new Student { NumerIndeksu = "444", Imie = "Justyna", Nazwisko = "Dzik" },
                new Student { NumerIndeksu = "555", Imie = "Michał", Nazwisko = "Lis" },
                new Student { NumerIndeksu = "666", Imie = "Daria", Nazwisko = "Nowak" },
                new Student { NumerIndeksu = "777", Imie = "Mateusz", Nazwisko = "Mostowiak" }
            };

            foreach (Student s in studenci)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();
        }

        private static void InitializeCourses(DziekanatContext context)
        {
            if (context.Zajecia.Any())
            {
                return;
            }

            var zajecia = new Zajecia[]
            {
                new Zajecia{NazwaZajec = "Programowanie .NET",      TerminZajec = DateTime.Now.AddDays(2)},
                new Zajecia{NazwaZajec = "Programowanie C#",        TerminZajec = DateTime.Now.AddDays(2).AddHours(4)},
                new Zajecia{NazwaZajec = "Programowanie Java",      TerminZajec = DateTime.Now.AddDays(1).AddHours(2)},
                new Zajecia{NazwaZajec = "Bazy danych",             TerminZajec = DateTime.Now.AddDays(3).AddHours(1)},
                new Zajecia{NazwaZajec = "Wzorce projektowe",       TerminZajec = DateTime.Now.AddDays(5).AddHours(6)},
                new Zajecia{NazwaZajec = "Zarządzanie projektem",   TerminZajec = DateTime.Now.AddDays(7).AddHours(3)},
                new Zajecia{NazwaZajec = "UX",                      TerminZajec = DateTime.Now.AddDays(4).AddHours(3)},
                new Zajecia{NazwaZajec = "Microsoft",               TerminZajec = DateTime.Now.AddDays(1).AddHours(2)}
            };
            foreach (Zajecia c in zajecia)
            {
                context.Zajecia.Add(c);
            }
            context.SaveChanges();
        }
    }

}