namespace Urbagestion.Model.Interfaces
{
    public interface IHasIdentity
    {
        int Id { get; set; }

        bool IsActive { get; set; }
    }
}
