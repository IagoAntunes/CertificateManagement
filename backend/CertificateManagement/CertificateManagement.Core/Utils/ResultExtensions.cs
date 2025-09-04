namespace CertificateManagement.Core.Utils
{
    using Microsoft.AspNetCore.Mvc;

    public static class ResultExtensions
    {
        public static TOut Match<TOut>(this ResultBase result, Func<TOut> onSuccess, Func<Error, TOut> onFailure)
            => result.IsSuccess ? onSuccess() : onFailure(result.Error);

        public static TOut Match<T, TOut>(this ResultOfT<T> result, Func<T, TOut> onSuccess, Func<Error, TOut> onFailure)
            => result.IsSuccess ? onSuccess(result.Value!) : onFailure(result.Error);

        public static IActionResult ToActionResult(this ResultBase result, Func<IActionResult>? onSuccess = null)
            => result.Match(
                onSuccess: onSuccess ?? (() => new NoContentResult()),
                onFailure: error => ToProblem(error));

        public static IActionResult ToActionResult<T>(this ResultOfT<T> result, Func<T, IActionResult>? onSuccess = null)
            => result.Match<T, IActionResult>(
                onSuccess: value => onSuccess?.Invoke(value) ?? new OkObjectResult(value),
                onFailure: error => ToProblem(error));

        private static IActionResult ToProblem(Error error)
            => new ObjectResult(new ProblemDetails
            {
                Title = "Request failed",
                Detail = error.Description,
                Status = error.StatusCode
            })
            {
                StatusCode = error.StatusCode
            };
    }

}
