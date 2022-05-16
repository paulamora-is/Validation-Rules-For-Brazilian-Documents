using DocumentValidator.Entities;
using DocumentValidator.Entities.Interfaces;
using DocumentValidator.Validators.Consts;
using System.ComponentModel.DataAnnotations;

namespace DocumentValidator.Validators
{
    public class CPFDocumentValidator : IDocumentValidator
    {
        private readonly CPFDocumentEntity _cPFDocument;
        private const int LenghtOfDocumentCPF = 11;

        public CPFDocumentValidator()
        {
            _cPFDocument = new CPFDocumentEntity();
        }

        public IEnumerable<int> MultiplyDocumentValues(string cpfDocumentValue)
        {
            var multiplierForTheFirstDigit = GetMultiplierForTheFirstDigit();
            var resultsOfMultiplicationForTheFirstDigit = new List<int>();

            var multiplierForTheSecondDigit = GetMultiplierForTheSecondDigit();
            var resultsOfMultiplicationForTheSecondDigit = new List<int>();

            if (!IsValidDocumentValue(cpfDocumentValue))
            {
                throw new ValidationException();
            }

            for (int i = 0; i < 9; i++)
            {
               var multipliedValuesForTheFirstDigit = int.Parse(GetTheFirstNineDigits(cpfDocumentValue)[i].ToString()) * multiplierForTheFirstDigit[i];

                resultsOfMultiplicationForTheFirstDigit.Add(multipliedValuesForTheFirstDigit);
            }

            for(int i = 0; i < 10; i++)
            {
                var multipliedValuesForTheSecondDigit = int.Parse(GetTheFirstTenDigits(cpfDocumentValue)[i].ToString()) * multiplierForTheSecondDigit[i];

                resultsOfMultiplicationForTheSecondDigit.Add(multipliedValuesForTheSecondDigit);
            }
            
            var resultsOfMultiplcationAllDigits = resultsOfMultiplicationForTheFirstDigit.Concat(resultsOfMultiplicationForTheSecondDigit).ToList();

            resultsOfMultiplcationAllDigits.ForEach(i => Console.Write("{0}\t", i));
            return resultsOfMultiplcationAllDigits;
        }

        public bool IsValidDocumentValue(string cpfDocumentValue)
        {
            _cPFDocument.CPFValue = cpfDocumentValue;
            var documentWithoutDashsAndDots= CPFDocumentEntity.ReplaceDashAndDot(cpfDocumentValue);

            if (string.IsNullOrEmpty(documentWithoutDashsAndDots))
            {
                return false;
            }
            if (IsRepeatedValue(cpfDocumentValue))
            {
                return false;
            }
            if (documentWithoutDashsAndDots.Length != LenghtOfDocumentCPF)
            {
                return false;
            }
            return true;
        }

        private static string GetTheFirstNineDigits(string cpfDocumentValue)
            => cpfDocumentValue[..9];

        private static string GetTheFirstTenDigits(string cpfDocumentValue)
            => cpfDocumentValue[..10];

        private static int[] GetMultiplierForTheFirstDigit()
            => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static int[] GetMultiplierForTheSecondDigit()
            => new int[10] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        private static bool IsRepeatedValue(string cpfDocumentValue)
        {
           switch (cpfDocumentValue)
            {
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberZero:break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberOne: break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberTwo: break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberThree:break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberFour: break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberFive: break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberSix: break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberSeven:break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberEight:break;
                case InvalidDocumentTypesConst.CPFWithRepetitionsOfTheNumberNine:break;
                default:
                return false;
            }
            return true;

        }
    }
}
