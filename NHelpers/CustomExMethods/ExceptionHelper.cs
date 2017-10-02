namespace EasySharp.NHelpers.CustomExMethods
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public static class ExceptionHelper
    {
        /// <summary>
        ///     Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
        /// </summary>
        /// <param name="exception">Source <see cref="Exception" /></param>
        /// <returns>
        ///     <see cref="string" /> that represents the entire stack trace consisting of exception's footprints (File,
        ///     Method, LineNumber)
        /// </returns>
        public static string GetExceptionFootprints(this Exception exception)
        {
            StackTrace stackTrace = new StackTrace(exception, true);
            StackFrame[] frames = stackTrace.GetFrames();

            if (ReferenceEquals(frames, null))
            {
                return string.Empty;
            }

            var traceStringBuilder = new StringBuilder();

            for (var i = 0; i < frames.Length; i++)
            {
                StackFrame frame = frames[i];

                if (frame.GetFileLineNumber() < 1)
                    continue;

                traceStringBuilder.AppendLine($"File: {frame.GetFileName()}");
                traceStringBuilder.AppendLine($"Method: {frame.GetMethod().Name}");
                traceStringBuilder.AppendLine($"LineNumber: {frame.GetFileLineNumber()}");

                if (i == frames.Length - 1)
                    break;

                traceStringBuilder.AppendLine(" ---> ");
            }

            string stackTraceFootprints = traceStringBuilder.ToString();

            if (string.IsNullOrWhiteSpace(stackTraceFootprints))
                return "NO DETECTED FOOTPRINTS";

            return stackTraceFootprints;
        }


        // <summary>
        // Equivalent to Exception.ToString();
        // </summary>
        //public static string FlattenException(this Exception exception)
        //{
        //    var stringBuilder = new StringBuilder();

        //    while (exception != null)
        //    {
        //        stringBuilder.AppendLine(exception.Message);
        //        stringBuilder.AppendLine(exception.StackTrace);

        //        exception = exception.InnerException;
        //    }

        //    return stringBuilder.ToString();
        //}
    }
}