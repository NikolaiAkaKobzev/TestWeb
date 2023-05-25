namespace WebApi.Services
{
    public class NumbersInStringService : INumbersInStringService
    {
        public double GetNumbersSum(string value)
        {
            return value.Where(Char.IsDigit).Sum(Char.GetNumericValue);
        }
    }
}
