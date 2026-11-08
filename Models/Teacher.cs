using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models;

public class Teacher : Person
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    public required string KnowledgeArea { get; set; }

    public List<Course> Courses { get; set; } = new();

    public override string ToString()
    {
        string CourseTitles = "";
        int count = 0;

        foreach (Course course in Courses)
        {
            if (count > 0)
            {
                CourseTitles += ", ";
            }

            CourseTitles += course.CourseTitel;
            count++;
        }
        return $"Personnummer: {PersonalId}, Förnamn: {FirstName}, Efternamn: {LastName}, Epost: {Email}, Telefonnummer: {PhoneNr}, Adress: {Address.StreetAdress}, Stad: {Address.City}, Postnummer: {Address.PostalCode}, Kunskapområden: {KnowledgeArea}, Ansvariga Kurser: {CourseTitles}";
    }
}
