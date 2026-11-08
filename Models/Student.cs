using System;
using System.Text.Json.Serialization;
namespace Client.Models;

public class Student : Person
{
    [JsonPropertyName("_id")]
    public string? Id { get; set; }

    public override string ToString()
    {
        return $"Personnummer: {PersonalId}, Förnamn: {FirstName}, Efternamn: {LastName}, Epost: {Email}, TelefonNummer: {PhoneNr}, Adress: {Address.StreetAdress}, Stad: {Address.City}, Postnummer: {Address.PostalCode}";
    }
}
