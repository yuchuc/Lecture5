namespace Lecture.Domain.Entities
{
  public class User
  {
    public virtual int Id { get; set; }
    public virtual string Email { get; set; }
    public virtual int Age { get; set; }

    protected virtual bool Equals(User other)
    {
      return string.Equals(Email, other.Email);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      var other = obj as User;
      return other != null && Equals(other);
    }

    public override int GetHashCode()
    {
      return Email.GetHashCode();
    }

    public static bool operator ==(User left, User right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(User left, User right)
    {
      return !Equals(left, right);
    }
  }
}