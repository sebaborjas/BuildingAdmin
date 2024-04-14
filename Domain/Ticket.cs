using Domain.DataTypes;
using Exceptions;
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
    public Category Category { get; set; }

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
}
