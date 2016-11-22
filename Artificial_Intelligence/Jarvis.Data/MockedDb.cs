using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Data
{
    public sealed class MockedDb
    {
        private static readonly Lazy<MockedDb> Lazy =
            new Lazy<MockedDb>(() => new MockedDb());

        private IList<string> _jokes = new List<string>
        {
            "Shortest love story ever:" + Environment.NewLine +
            @"He: You are the "";"" of my code!" + Environment.NewLine +
            "She: Sorry, I have python :D",

            "My Grandpa once said, \"Your generation relies too much on technology!\"\r\nI replied, \"No, your generation relies too much on technology!\"\r\nThen I unplugged his life support."
        };

        private MockedDb()
        {
        }

        public static MockedDb Instance => Lazy.Value;

        public IList<string> Jokes
        {
            get { return this._jokes; }
        }
    }
}
