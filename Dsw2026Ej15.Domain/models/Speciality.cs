namespace Dsw2026Ej15.Domain.models;

public class Speciality : BaseEntity
{

    public String Name { get; set; }
    public String Description { get; set; }

    
    private Speciality()
    {

    }
    public Speciality(String name, string description, Guid? id = null) : base(id)
    {

        Name = name;
        Description = description;
    }


}
