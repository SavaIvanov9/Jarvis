namespace Jarvis.Data
{
    using System;
    using System.Collections.Generic;

    public sealed class FakeDb
    {
        private static readonly Lazy<FakeDb> Lazy =
            new Lazy<FakeDb>(() => new FakeDb());

        private IList<string> _jokes = new List<string>
        {
            "Shortest love story ever:" + Environment.NewLine +
            @"He: You are the semicolon ("";"") of my code!" + Environment.NewLine +
            "She: Sorry, I have python :D",

            "My Grandpa once said, \"Your generation relies too much on technology!\"\r\nI replied, \"No, your generation relies too much on technology!\"\r\nThen I unplugged his life support.",

            "How did the programmer die in the shower?\r\n He read the shampoo bottle instructions: Lather. Rinse. Repeat.",

            "Knock, knock.”\r\n“Who’s there?”\r\nvery long pause….\r\n“Java.”",

            "Programming is like sex: One mistake and you have to support it for the rest of your life. "
        };

        private FakeDb()
        {
        }

        public static FakeDb Instance => Lazy.Value;

        public IList<string> Jokes
        {
            get { return this._jokes; }
        }
    }
}
