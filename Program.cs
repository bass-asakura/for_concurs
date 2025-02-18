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
        _students = new List<Student>();
        CreateFile();
    }

    private void CreateFile()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            _students = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
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
            _students = _students.OrderByDescending(s => s.Rating).ThenBy(s => s.FullName).ToList();
        }
        else 
        {
            _students = _students.OrderBy(s => s.Rating).ThenBy(s => s.FullName).ToList();
        }
    }

    public void SortByName()
    {
        _students = _students.OrderBy(s => s.FullName).ToList();
    }

    public void UpdateRatings(string newFilePath)
    {
        if (!File.Exists(newFilePath))
        {
            Console.WriteLine("Не существующий файл");
            return;
        }
        
        string json = File.ReadAllText(newFilePath);
        var newScores = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();

        foreach (var newStudent in newScores)
        {
            var oldStudent = _students.FirstOrDefault(s => s.Id == newStudent.Id);

            if (oldStudent != null)
            {
                oldStudent.Rating = newStudent.Rating;
            }
            else
            {
                _students.Add(newStudent);
            }
        }

        string json_2 = JsonConvert.SerializeObject(_students, Formatting.Indented);
        File.WriteAllText(_filePath, json_2);
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

                    Console.WriteLine("Введите путь к файлу");
                    string vvod_in_case_4 = Console.ReadLine() ?? "";

                    students.UpdateRatings($@"{vvod_in_case_4}");
                    
                    break;
                case "5":
                    return;
            }
        }
    }
    
}   