using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DILearn
{
    public class FooService
    {
        private readonly IMessageWriter _messageWriter;

        // コンストラクタでIMessageWriterを受け取る
        public FooService(IMessageWriter messageWriter)
        {
            this._messageWriter = messageWriter;
        }

        public void Execute()
        {
            // Foo!と書き込む
            _messageWriter.Write("Foo!");
        }
    }
}
