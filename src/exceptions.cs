using System;

namespace Bluebird {

    namespace Exceptions {

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

        [Serializable]
        public class CommandNotFoundException : Exception {
            public CommandNotFoundException() { }
            public CommandNotFoundException(string why) : base(why) { }
        }
    }
}