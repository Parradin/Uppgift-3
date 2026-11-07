using System;

namespace Client.Models;

public class Person
{
    public required string PersonalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNr { get; set; }
    public required Address Address { get; set; }

}
