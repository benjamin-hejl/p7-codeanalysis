using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Text;

namespace P7.CodeAnalysis.Text
{
    public class SourceTextBuilder
    {
        private class IndentedBlockHandle : IDisposable
        {
            private readonly String _openText;
            private readonly String _closeText;
            private readonly SourceTextBuilder _textBuilder;

            public IndentedBlockHandle(SourceTextBuilder textBuilder, String openText, String closeText)
            {
                _textBuilder = textBuilder;
                _openText = openText;
                _closeText = closeText;
            }

            public IDisposable Open()
            {
                _textBuilder.AppendLine(_openText);
                _textBuilder._indentation++;
                return this;
            }

            public void Close()
            {
                _textBuilder._indentation--;
                _textBuilder.AppendLine(_closeText);
            }

            void IDisposable.Dispose()
            {
                Close();
            }
        }

        private readonly Dictionary<(String, String), IndentedBlockHandle> _blockCache = new Dictionary<(String, String), IndentedBlockHandle>();

        private static readonly Char DefaultIndentChar = ' ';
        private static readonly Int32 DefaultIndentSize = 4;
        private static readonly String DefaultLineEnd = Environment.NewLine;

        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private readonly Char _indentChar;
        private readonly Int32 _indentSize;
        private readonly String _lineEnd;
        private Int32 _indentation = 0;


        public SourceTextBuilder() : this(DefaultIndentChar, DefaultIndentSize, DefaultLineEnd)
        { }

        public SourceTextBuilder(Char indentChar, Int32 indentSize) : this(indentChar, indentSize, DefaultLineEnd)
        { }

        public SourceTextBuilder(Char indentChar, Int32 indentSize, String lineEnd)
        {
            if (lineEnd is null)
            {
                throw new ArgumentNullException(nameof(lineEnd));
            }

            _indentChar = indentChar;
            _indentSize = indentSize;
            _lineEnd = lineEnd;
        }

        public SourceTextBuilder AppendLine(String text)
        {
            return AppendLine(0, text);
        }

        public SourceTextBuilder AppendLine(Int32 indentation, String text)
        {
            AppendIndentation(indentation);
            AppendText(text);
            AppendLineEnd();
            return this;
        }

        public SourceTextBuilder Clear()
        {
            _stringBuilder.Clear();
            return this;
        }

        public IDisposable CodeBlock()
        {
            return IndentedBlock("{", "}");
        }

        public IDisposable CodeBlock(String preamble)
        {
            return AppendLine(preamble).CodeBlock();
        }

        public IDisposable IndentedBlock(String open, String close)
        {
            return GetBlock(open, close).Open();
        }

        private IndentedBlockHandle GetBlock(String open, String close)
        {
            if (!_blockCache.TryGetValue((open, close), out IndentedBlockHandle? block))
            {
                block = new IndentedBlockHandle(this, open, close);
                _blockCache.Add((open, close), block);
            }
            return block;
        }

        public SourceText ToSourceText()
        {
            return SourceText.From(ToString(), Encoding.UTF8);
        }

        public override String ToString()
        {
            return _stringBuilder.ToString();
        }

        private void AppendIndentation(Int32 indentation)
        {
            indentation += _indentation;
            if (indentation > 0)
            {
                _stringBuilder.Append(_indentChar, indentation * _indentSize);
            }
        }

        private void AppendText(String text)
        {
            _stringBuilder.Append(text);
        }

        private void AppendLineEnd()
        {
            _stringBuilder.Append(_lineEnd);
        }
    }
}