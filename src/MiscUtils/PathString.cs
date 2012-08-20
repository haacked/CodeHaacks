using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;

namespace MiscUtils
{
    [Serializable]
    public class PathString : StringEquivalent<PathString>
    {
        public PathString(string pathString) : base(NormalizePath(pathString))
        {
        }

        protected PathString(SerializationInfo info, StreamingContext context) : base(info)
        {
        }

        // So the XmlSerializer can create this.
        protected PathString()
        {
        }

        public override PathString Combine(string path)
        {
            Ensure.ArgumentNotNull(path, "path");

            if (path.StartsWith(@"\", StringComparison.Ordinal))
            {
                path = path.Substring(1);
            }

            try
            {
                return Path.Combine(Value, path);
            }
            catch (ArgumentException)
            {
                return AppendPath(Value, path);
            }
        }

        private static string AppendPath(string source, string addition)
        {
            if (source.Last() != Path.DirectorySeparatorChar) source += "/";
            if (addition.First() == '/') addition = addition.Substring(1);
            return source + addition;
        }

        public static implicit operator PathString(string value)
        {
            return value == null ? null : new PathString(value);
        }

        public static implicit operator string(PathString path)
        {
            return path == null ? null : path.Value;
        }

        private static string NormalizePath(string path)
        {
            return path == null ? null : path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public override bool Equals(string other)
        {
            return other != null && Value.Equals(other, StringComparison.OrdinalIgnoreCase);
        }
    }
}