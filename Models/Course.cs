using System;

namespace Client.Models;

public class Course
{
    public required string CourseNr { get; set; }
    public required string CourseTitel { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public bool IsDistance { get; set; }

    public bool IsCourseDateValid()
    {
        if (EndDate < StartDate)
        {
            return false;
        }
        return true;
    }
    public override string ToString()
    {
        string courseType;

        if (IsDistance)
        {
            courseType = "Distans";
        }
        else
        {
            courseType = "Klassrum";
        }
        return $"Kursnummer: {CourseNr}, Ämne: {CourseTitel}, Start: {StartDate}, Slut: {EndDate}, Längd: {LengthInDays} dagar, Typ: {courseType}";

    }

    public int LengthInDays
    {
        get
        {
            return (EndDate - StartDate).Days;
        }
    }
}
