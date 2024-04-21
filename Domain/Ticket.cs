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

            if (string.IsNullOrWhiteSpace(value) || value.Trim().Length < minLength)
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

    public float TotalCost
    {
        get => _totalCost;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _totalCost = value;
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

    public void ProcessTicket(Status newStatus, DateTime? dateTime)
    {
        if (newStatus == Status.Open)
        {
            ProcessOpenStatus();
        }
        else if (newStatus == Status.InProgress)
        {
            ProcessInProgressStatus(dateTime);
        }
        else if (newStatus == Status.Closed)
        {
            ProcessClosedStatus(dateTime);
        }
        else
        {
            throw new ArgumentException("Estado de solicitud no válido");
        }
    }

    private void ProcessOpenStatus()
    {
        if (Status != Status.Open)
        {
            throw new InvalidOperationException("No se puede cambiar el estado a abierto");
        }
    }

    private void ProcessInProgressStatus(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            throw new ArgumentNullException(nameof(dateTime), "La fecha de inicio es requerida");
        }

        AttentionDate = dateTime.Value;
        Status = Status.InProgress;
    }

    private void ProcessClosedStatus(DateTime? dateTime)
    {
        if (Status != Status.InProgress)
        {
            throw new InvalidOperationException("No se puede cambiar el estado a cerrado sin estar en progreso");
        }

        if (dateTime == null)
        {
            throw new ArgumentNullException(nameof(dateTime), "La fecha de cierre es requerida");
        }

        if (dateTime.Value < AttentionDate)
        {
            throw new ArgumentException("La fecha de cierre no puede ser anterior a la fecha de atención");
        }

        ClosingDate = dateTime.Value;
        Status = Status.Closed;
    }
}
