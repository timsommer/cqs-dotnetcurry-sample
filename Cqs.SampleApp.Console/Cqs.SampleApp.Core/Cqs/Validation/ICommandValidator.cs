namespace Cqs.SampleApp.Core.Cqs.Validation
{
    public interface ICommandValidator<T> where T : ICommand
    {
        void Validate(T request, ValidationBag bag);
    }
}