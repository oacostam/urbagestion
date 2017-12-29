namespace Urbagestion.Model.Interfaces
{
    public interface IHasIdentity
    {
        int Id { get; }

        bool IsActive { get; set; }
    }
}
