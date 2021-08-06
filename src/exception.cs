using System;

namespace bluebird {

    namespace exception {

        [Serializable]
        public class CrashCommandException : Exception {
            public CrashCommandException() { }
            public CrashCommandException(string why) : base(why) { }
        }

        [Serializable]
        public class InvalidDataEnteredException : Exception {
            public InvalidDataEnteredException() { }
            public InvalidDataEnteredException (string why) : base(why) { }
        }
    }
}