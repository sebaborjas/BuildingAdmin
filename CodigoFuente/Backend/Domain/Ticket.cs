using Domain.DataTypes;
using System.Reflection.Metadata;
namespace Domain;

public class Ticket
{
    private int _id;
    private string _description;

    private Apartment _apartment;
    private float _totalCost;
    private Manager _createdBy;

    public Category Category { get; set; }
    public MaintenanceOperator AssignedTo { get; set; }
    public Status Status { get; set; } = Status.Open;
    public DateTime CreationDate { get; private set; } = DateTime.Now.Date;
    public DateTime AttentionDate { get; private set; }
    public DateTime ClosingDate { get; private set; }

    public float TotalCost
    {
        get => _totalCost;
        private set
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            _totalCost = value;
        }
    }   

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException();

            _id = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            const int minLength = 10;

            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException();

            else if (value.Length < minLength) throw new ArgumentOutOfRangeException($"La descripción debe tener al menos {minLength} caracteres");

            _description = value;

        }
    }

    public Apartment Apartment
    {
        get => _apartment;
        set
        {
            if (value == null) throw new ArgumentNullException();

            _apartment = value;
        }
    }

    public Manager CreatedBy
    {
        get => _createdBy;
        set
        {
            if (value == null) throw new ArgumentNullException();

            _createdBy = value;
        }
    }

    public void AttendTicket()
    {
        if (Status != Status.Open) throw new InvalidOperationException("No se puede cambiar el estado de un ticket que no está abierto");

        AttentionDate = DateTime.Now;
        Status = Status.InProgress;
    }

    public void CloseTicket(float totalCost)
    {
        if (Status != Status.InProgress) throw new InvalidOperationException("No se puede cerrar un ticket sin estar en progreso");

        ClosingDate = DateTime.Now;
        _totalCost = totalCost;
        Status = Status.Closed;
    }

    public override bool Equals(object obj)
    {
       if(obj is Ticket ticket)
        {
           return ticket.Id == Id;
       }
       return false;
    }
}
