namespace ChallengeNubimetrics.Application.Models.Common
{
    public record Result
    {
        public Result()
        {
            HasErrors = false;
            Message = string.Empty;
        }

        public virtual bool HasErrors { get; set; }
        public virtual string Message { get; set; }


        public Result Success(string message)
            => new Result() { HasErrors = false, Message = message };

        public Result Error(string message)
            => new Result() { HasErrors = true, Message = message };

        public Result NotFound()
            => new Result() { HasErrors = true, Message = "No se encontró un registro con la información enviada" };
    }
}
