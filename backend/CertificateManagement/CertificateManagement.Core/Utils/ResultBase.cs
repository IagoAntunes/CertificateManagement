namespace CertificateManagement.Core.Utils
{
    public class ResultBase
    {
        private ResultBase(bool isSuccess, Error error)
        {
            if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
                throw new ArgumentException("Invalid error", nameof(error));

            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public Error Error { get; }

        public static ResultBase Success() => new(true, Error.None);
        public static ResultBase Failure(Error error) => new(false, error);
    }


}
