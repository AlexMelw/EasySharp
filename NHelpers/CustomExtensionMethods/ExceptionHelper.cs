namespace EasySharp.NHelpers.CustomExtensionMethods
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public static class ExceptionHelper
    {
        public static string FlattenException(this Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }

        public static string GetExceptionFootprints(this Exception exception)
        {
            StackTrace st = new StackTrace(e: exception, fNeedFileInfo: true);
            StackFrame[] frames = st.GetFrames();

            if (object.ReferenceEquals(frames, null))
            {
                return string.Empty;
            }

            var traceStringBuilder = new StringBuilder();

            foreach (var frame in frames)
            {
                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceStringBuilder.AppendLine($"File: {frame.GetFileName()}");
                traceStringBuilder.AppendLine($"Method:{frame.GetMethod().Name}");
                traceStringBuilder.AppendLine($"LineNumber: {frame.GetFileLineNumber()}");
                traceStringBuilder.AppendLine(" --> ");
            }

            return traceStringBuilder.ToString();
        }
    }
}