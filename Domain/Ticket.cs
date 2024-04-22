using Domain.DataTypes;
using Exceptions;
using System.Reflection.Metadata;
namespace Domain;

public class Ticket
{
    private int _id;
    private string _description;
    public DateTime CreationDate { get; private set; } = DateTime.Now.Date;
    private Apartment _apartment;
    private float _totalCost;
    public User AssignedTo { get; set; }
    private User _createdBy;
    public Category Category { get; set; }
    public Status Status { get; set; } = Status.Open;
    public DateTime AttentionDate { get; private set; }
    public DateTime ClosingDate { get; private set; }
    public float TotalCost { get => _totalCost;}

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _id = value;
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            const int minLength = 10;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException();
            }
            else if (value.Length < minLength)
            {
                throw new ArgumentOutOfRangeException($"La descripción debe tener al menos {minLength} caracteres");
            }

            _description = value;

        }
    }

    public Apartment Apartment
    {
        get => _apartment;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _apartment = value;
        }
    }

    public User CreatedBy
    {
        get => _createdBy;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _createdBy = value;
        }
    }

    public void AttendTicket()
    {
        if (Status != Status.Open)
        {
            throw new InvalidOperationException("No se puede cambiar el estado de un ticket que no está abierto");
        }

        AttentionDate = DateTime.Now;
        Status = Status.InProgress;
    }

    public void CloseTicket(float totalCost)
    {
        if (Status != Status.InProgress)
        {
            throw new InvalidOperationException("No se puede cerrar un ticket sin estar en progreso");
        }

        ClosingDate = DateTime.Now;
        _totalCost = totalCost;
        Status = Status.Closed;
    }
}
