namespace DocumentValidator.Entities.Interfaces
{
    public interface IDocumentValidator
    {
        bool IsValidDocumentValue(string valueDocument);
        IEnumerable<int> MultiplyDocumentValues(string valueDocument);
    }
}
