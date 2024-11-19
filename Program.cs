using System;
using System.Collections.Generic;
using System.IO;

//// Розробити програму для обліку кімнат в гуртожитку. Кожна кімната
//може бути двомісною або трьохмісною. Необхідно зберігати інформацію
//про тип кімнати та всіх її мешканців: ПІБ, дата народження, факультет,
//група, форма навчання(державне замовлення чи за кошти фізичних осіб).
//Комендант гуртожитку повинен формувати звіт у вигляді текстового
//файлу про кожну кімнату(тип кімнати, кількість мешканців, квартплата
//для кожного мешканця та інформація про всіх мешканців). Поставлене
//завдання реалізувати за допомогою шаблону проектування Прототип.

// Клас, що представляє мешканця кімнати
public class Resident : ICloneable
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Faculty { get; set; }
    public string Group { get; set; }
    public string EducationForm { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

// Клас, що представляє кімнату
public abstract class RoomPrototype
{
    public string RoomType { get; protected set; }
    public int Capacity { get; protected set; }
    public List<Resident> Residents { get; protected set; }

    public abstract RoomPrototype Clone();
    public abstract string GenerateReport();
}

// Конкретний прототип двомісної кімнати
public class DoubleRoom : RoomPrototype
{
    public DoubleRoom()
    {
        RoomType = "Double";
        Capacity = 2;
        Residents = new List<Resident>();
    }

    public override RoomPrototype Clone()
    {
        return (RoomPrototype)MemberwiseClone();
    }

    public override string GenerateReport()
    {
        string report = $"Room Type: {RoomType}\n";
        report += $"Capacity: {Capacity}\n";

        foreach (var resident in Residents)
        {
            report += $"Resident:\n";
            report += $"  Full Name: {resident.FullName}\n";
            report += $"  Date of Birth: {resident.DateOfBirth}\n";
            report += $"  Faculty: {resident.Faculty}\n";
            report += $"  Group: {resident.Group}\n";
            report += $"  Education Form: {resident.EducationForm}\n";
        }

        report += "\n";

        return report;
    }
}

// Конкретний прототип трьохмісної кімнати
public class TripleRoom : RoomPrototype
{
    public TripleRoom()
    {
        RoomType = "Triple";
        Capacity = 3;
        Residents = new List<Resident>();
    }

    public override RoomPrototype Clone()
    {
        return (RoomPrototype)MemberwiseClone();
    }

    public override string GenerateReport()
    {
        string report = $"Room Type: {RoomType}\n";
        report += $"Capacity: {Capacity}\n";

        foreach (var resident in Residents)
        {
            report += $"Resident:\n";
            report += $"  Full Name: {resident.FullName}\n";
            report += $"  Date of Birth: {resident.DateOfBirth}\n";
            report += $"  Faculty: {resident.Faculty}\n";
            report += $"  Group: {resident.Group}\n";
            report += $"  Education Form: {resident.EducationForm}\n";
        }

        report += "\n";

        return report;
    }
}

// Клас, що представляє гуртожиток
public class Dormitory
{
    private Dictionary<int, RoomPrototype> rooms; //ключ – ном кімнати а значення – об'єкт 

    public Dormitory()
    {
        rooms = new Dictionary<int, RoomPrototype>();
    }

    public RoomPrototype GetRoom(int roomNumber)
    {
        return rooms[roomNumber].Clone();
    }

    public void SetRoom(int roomNumber, RoomPrototype room)
    {
        rooms[roomNumber] = room;
    }
}

// Клас, що представляє коменданта гуртожитку
public class DormitoryCommandant
{
    private Dormitory dormitory;

    public DormitoryCommandant(Dormitory dormitory)
    {
        this.dormitory = dormitory;
    }

    public void GenerateRoomReport(int roomNumber, string outputFile)
    {
        RoomPrototype room = dormitory.GetRoom(roomNumber);
        string report = room.GenerateReport();

        using (StreamWriter writer = new StreamWriter(outputFile, true))
        {
            writer.WriteLine(report);
        }
    }
}

// Приклад використання
public class Program
{
    public static void Main(string[] args)
    {
        // Створення прототипів кімнат
        DoubleRoom doubleRoomPrototype = new DoubleRoom();
        TripleRoom tripleRoomPrototype = new TripleRoom();

        // Створення гуртожитку
        Dormitory dormitory = new Dormitory();

        // Додавання кімнат до гуртожитку
        dormitory.SetRoom(101, doubleRoomPrototype);
        dormitory.SetRoom(102, tripleRoomPrototype);

        // Додавання мешканців до кімнат
        RoomPrototype room101 = dormitory.GetRoom(101);
        room101.Residents.Add(new Resident
        {
            FullName = "John Doe",
            DateOfBirth = new DateTime(1990, 5, 15),
            Faculty = "Engineering",
            Group = "A1",
            EducationForm = "State-funded"
        });
        room101.Residents.Add(new Resident
        {
            FullName = "Jane Smith",
            DateOfBirth = new DateTime(1992, 9, 20),
            Faculty = "Business",
            Group = "B2",
            EducationForm = "Private"
        });

        RoomPrototype room102 = dormitory.GetRoom(102);
        room102.Residents.Add(new Resident
        {
            FullName = "Adam Johnson",
            DateOfBirth = new DateTime(1991, 3, 10),
            Faculty = "Medicine",
            Group = "C3",
            EducationForm = "State-funded"
        });
        room102.Residents.Add(new Resident
        {
            FullName = "Emily Davis",
            DateOfBirth = new DateTime(1993, 7, 25),
            Faculty = "Art",
            Group = "D4",
            EducationForm = "Private"
        });
        room102.Residents.Add(new Resident
        {
            FullName = "Michael Wilson",
            DateOfBirth = new DateTime(1990, 8, 5),
            Faculty = "Science",
            Group = "E5",
            EducationForm = "State-funded"
        });

        // Створення коменданта гуртожитку
        DormitoryCommandant commandant = new DormitoryCommandant(dormitory);

        // Генерація звіту для кімнати 101
        commandant.GenerateRoomReport(101, "Room101Report.txt");

        // Генерація звіту для кімнати 102
        commandant.GenerateRoomReport(102, "Room102Report.txt");

        Console.ReadKey();
    }
}