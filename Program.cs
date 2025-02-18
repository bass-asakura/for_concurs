using Newtonsoft.Json;

namespace cdealy_na_easy;

class Student
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public int Rating { get; set; }
}

class StudentUse
{
    private string _filePath;
    private List<Student> _students;

    public StudentUse(string filePath)
    {
        _filePath = filePath;
        CreateFile();
    }

    private void CreateFile()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
        }
        else
        {
            _students = new List<Student>();
        }
    }
    
    public void DisplayStudents()
    {
        foreach (var student in _students)
        {
            Console.WriteLine($"ID - {student.Id}, ФИО - {student.FullName}, Рейтинг - {student.Rating}");
        }
    }

    public void SortByRating(bool bol)
    {
        if (bol == true) 
        {
            _students = _students.OrderByDescending(s => s.Rating).ToList();
        }
        else 
        {
            _students = _students.OrderBy(s => s.Rating).ToList();
        }
    }

    public void SortByName()
    {
        _students = _students.OrderBy(s => s.FullName).ToList();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Play();
    }
    
    public static void Play()
    {
        while(true)
        {
            StudentUse students = new StudentUse(@"C:\vs code\cdealy_na_easy\info\Data_Example.json");

            Console.WriteLine("1 - Показать таблицу");
            Console.WriteLine("2 - Отсортировать по рейтингу");
            Console.WriteLine("3 - Отсортировать по имени");
            Console.WriteLine("4 - Внести изменения");
            Console.WriteLine("5 - Выход");
            
            var vvod = Console.ReadLine() ?? "";

            switch(vvod)
            {
                case "1":

                    students.DisplayStudents();
                    break;

                case "2":

                    Console.WriteLine("1 - По убыванию\n2 - По возрастанию");

                    var vvod_in_case_2 = Console.ReadLine() ?? "";

                        if (vvod_in_case_2 == "1")
                        {
                            students.SortByRating(true);
                        }

                        else
                        {
                            students.SortByRating(false);
                        }

                    students.DisplayStudents();
                    break;

                case "3":

                    students.SortByName();
                    students.DisplayStudents();
                    break;

                case "4":
                    break;
                case "5":
                    return;
            }
        }
    }
    
}   