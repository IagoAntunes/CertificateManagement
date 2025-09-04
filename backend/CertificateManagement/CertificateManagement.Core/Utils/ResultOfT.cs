namespace CertificateManagement.Core.Utils
{
    public class ResultOfT<T>
    {
        private ResultOfT(bool isSuccess, T? value, Error error)
        {
            if ((isSuccess && error != Error.None) || (!isSuccess && error == Error.None))
                throw new ArgumentException("Invalid error", nameof(error));

            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T? Value { get; }
        public Error Error { get; }

        public static ResultOfT<T> Success(T value) => new(true, value, Error.None);
        public static ResultOfT<T> Failure(Error error) => new(false, default, error);

        public static implicit operator ResultOfT<T>(T value) => Success(value);
    }

}
