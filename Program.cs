using DocumentValidator.Validators;

class Program
{
    static void Main(string[] args)
    {
        var validator = new CPFDocumentValidator();
        validator.SumDocumentValues("84979038549");
    }
}