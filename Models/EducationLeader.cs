using System;

namespace Client.Models;

public class EducationLeader : Teacher
{
    public required DateTime DateOfHire { get; set; }

    public override string ToString()
    {
        return $"{base.ToString()} Anställningsdag: {DateOfHire}";
    }

}
