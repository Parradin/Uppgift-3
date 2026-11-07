using System;

namespace Client.Models;

public class Student : Person
{

    public override string ToString()
    {
        return $"Personnummer: {PersonalId}, Förnamn: {FirstName}, Efternamn: {LastName}, Epost: {Email}, TelefonNummer: {PhoneNr}, Adress: {Address.StreetAdress}, Stad: {Address.City}, Postnummer: {Address.PostalCode}";
    }
}
