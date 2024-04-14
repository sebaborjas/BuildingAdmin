using Exceptions;
namespace Domain;

public class Ticket
{
    private int _id;
    private string _description;
    public DateTime CreationDate { get; private set; } = DateTime.Now.Date;
    private DateTime _attentionDate;
    private DateTime _closingDate;

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

    public void SetAttentionDate(DateTime attentionDate)
    {
        if (_attentionDate == default(DateTime) && attentionDate >= CreationDate)
        {
            _attentionDate = attentionDate;
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

    public void SetClosingDate(DateTime closingDate)
    {
        if (_closingDate == default(DateTime) && closingDate >= _attentionDate)
        {
            _closingDate = closingDate;
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


}
