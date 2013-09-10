namespace Lecture.Domain.Orm
{
  public interface IConnectionStringProvider
  {
    string ConnectionString { get; }
    string ProviderName { get; }
  }
}