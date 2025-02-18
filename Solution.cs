using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Rating { get; set; }
}

public class StudentService
{
    private string _filePath;
    private List<Student> _students;

    public StudentService(string filePath)
    {
        _filePath = filePath;
        LoadData();
    }

    private void LoadData()
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

    public void SaveData()
    {
        string json = JsonConvert.SerializeObject(_students, Formatting.Indented);
        File.WriteAllText(_filePath, json);
    }

    public void DisplayStudents()
    {
        foreach (var student in _students)
        {
            Console.WriteLine($"ID: {student.Id}, ФИО: {student.FullName}, Рейтинг: {student.Rating}");
        }
    }

    public void SortByRating(bool ascending = true)
    {
        _students = ascending ? _students.OrderBy(s => s.Rating).ToList() : _students.OrderByDescending(s => s.Rating).ToList();
    }

    public void SortByName()
    {
        _students = _students.OrderBy(s => s.FullName).ToList();
    }

    public void UpdateRatings(string newFilePath)
    {
        if (!File.Exists(newFilePath))
        {
            Console.WriteLine("Файл с новыми баллами не найден.");
            return;
        }

        string json = File.ReadAllText(newFilePath);
        var newScores = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();

        foreach (var newStudent in newScores)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == newStudent.Id);
            if (existingStudent != null)
            {
                existingStudent.Rating += newStudent.Rating;
            }
            else
            {
                _students.Add(newStudent);
            }
        }

        SaveData();
    }
}
