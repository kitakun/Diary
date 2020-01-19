namespace Kitakun.TagDiary.Web.Exceptions
{
    using System;

    public class NotFoundWebException : Exception
    {
        public override string Message { get; }

        public NotFoundWebException(string text)
        {
            Message = text;
        }
    }
}
