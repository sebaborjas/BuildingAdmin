using Domain.DataTypes;
using Exceptions;
using System.Reflection.Metadata;
namespace Domain;

public class Ticket
{
    private int _id;
    private string _description;
    public DateTime CreationDate { get; private set; } = DateTime.Now.Date;
    private DateTime _attentionDate;
    private DateTime _closingDate;
    private TimeSpan _attentionTime;
    private TimeSpan _closingTime;
    private Apartment _apartment;
    private float _totalCost;
    private User _assignedTo;
    private User _createdBy;
    public Category Category { get; set; }
    public Status Status { get; set; } = Status.Open;


    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                _id = value;
            }
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
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
        get { return _apartment; }
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
        get { return _totalCost; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _totalCost = value;
        }
    }

    public User AssignedTo
    {
        get { return _assignedTo; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _assignedTo = value;
        }
    }

    public User CreatedBy
    {
        get { return _createdBy; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException();
            }
            _createdBy = value;
        }
    }

    public DateTime AttentionDate
    {
        get { return _attentionDate; }
        private set
        {
            if (_attentionDate == default(DateTime) && value >= CreationDate)
            {
                _attentionDate = value;
            }
            else if (_attentionDate != default(DateTime))
            {
                throw new InvalidOperationException("La fecha de atención ya fue establecida");
            }
            else
            {
                throw new InvalidOperationException("La fecha de atención no puede ser anterior a la fecha de creación");
            }
        }
    }

    public DateTime ClosingDate
    {
        get { return _closingDate; }
        private set
        {
            if (_closingDate == default(DateTime) && value >= _attentionDate)
            {
                _closingDate = value;
            }
            else if (_closingDate != default(DateTime))
            {
                throw new InvalidOperationException("La fecha de cierre ya fue establecida");
            }
            else
            {
                throw new InvalidOperationException("La fecha de cierre no puede ser anterior a la fecha de atención");
            }
        }
    }

    public TimeSpan AttentionTime
    {
        get { return _attentionTime; }
        private set { _attentionTime = value; }
    }

    public TimeSpan ClosingTime
    {
        get { return _closingTime; }
        private set { _closingTime = value; }
    }

    public void SetAttention(DateTime attentionDateTime)
    {
        if (_attentionDate == default(DateTime) && attentionDateTime.Date >= CreationDate)
        {
            _attentionDate = attentionDateTime.Date;
            _attentionTime = attentionDateTime.TimeOfDay;
        }
        else if (_attentionDate != default(DateTime))
        {
            throw new InvalidOperationException("La fecha de atención ya fue establecida anteriormente.");
        }
        else
        {
            throw new InvalidOperationException("La fecha de atención no puede ser anterior a la fecha de creación.");
        }
    }

    public void SetClosing(DateTime closingDateTime)
    {
        if (_closingDate == default(DateTime) && closingDateTime >= _attentionDate)
        {
            if (_closingDate == _attentionDate && _closingTime <= _attentionTime)
            {
                throw new ArgumentException("La hora de cierre debe ser posterior a la hora de atención");
            }

            _closingDate = closingDateTime.Date;
            _closingTime = closingDateTime.TimeOfDay;
        }
        else if (_closingDate != default(DateTime))
        {
            throw new InvalidOperationException("La fecha de cierre ya fue establecida anteriormente.");
        }
        else
        {
            throw new InvalidOperationException("La fecha de cierre no puede ser anterior a la fecha de atención.");
        }
    }

    public void ChangeStatus(Status newStatus)
    {
        if (newStatus == Status.Open)
        {
            throw new InvalidOperationException("No se puede cambiar el estado a abierto");
        }
        else if (newStatus == Status.Closed && _closingDate == default(DateTime))
        {
            throw new InvalidOperationException("No se puede cerrar un ticket sin fecha de cierre");
        }
        else if (newStatus == Status.InProgress && _attentionDate == default(DateTime))
        {
            throw new InvalidOperationException("No se puede cambiar el estado a En Progreso sin fecha de atención");
        }
        else
        {
            Status = newStatus;
        }
    }

}
