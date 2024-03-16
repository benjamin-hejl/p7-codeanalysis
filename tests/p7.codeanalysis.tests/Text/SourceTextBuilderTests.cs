using P7.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P7.CodeAnalysis.Tests.Text
{
    public class SourceTextBuilderTests
    {
        [Fact]
        public void NewInstance_Should_ProduceEmptyText()
        {
            SourceTextBuilder builder = new SourceTextBuilder();
            builder.ToString().Should().BeEmpty();
        }

        [Fact]
        public void AppendLine_Should_AppendLineToTheOutput()
        {
            SourceTextBuilder builder = new SourceTextBuilder();
            builder.AppendLine("hello world");
            builder.AppendLine("hello world");
            builder.AppendLine("hello world");
            builder.ToString().Should().Be(
                "hello world" + Environment.NewLine +
                "hello world" + Environment.NewLine +
                "hello world" + Environment.NewLine);
        }

        [Fact]
        public void IndentedBlock_Should_AppendOpeningToTheOutput()
        {
            SourceTextBuilder builder = new SourceTextBuilder();
            builder.IndentedBlock("opening", "closing");
            builder.ToString().Should().Be(
                "opening" + Environment.NewLine);
        }

        [Fact]
        public void DisposingIndentedBlock_Should_ApppendClosingToTheOutput()
        {
            SourceTextBuilder builder = new SourceTextBuilder();
            IDisposable block = builder.IndentedBlock("opening", "closing");
            builder.ToString().Should().Be(
                "opening" + Environment.NewLine);
            block.Dispose();
            builder.ToString().Should().Be(
                "opening" + Environment.NewLine +
                "closing" + Environment.NewLine);
        }

        [Fact]
        public void TextAppendedWithinIndentedBlock_Should_BeIndented()
        {
            SourceTextBuilder builder = new SourceTextBuilder();
            using (builder.IndentedBlock("opening", "closing"))
            {
                builder.AppendLine("first level");
                using (builder.IndentedBlock("1.1", "/1.1"))
                {
                    builder.AppendLine("second level");
                }
            }

            builder.ToString().Should().Be(
                "opening" + Environment.NewLine +
                "    first level" + Environment.NewLine +
                "    1.1" + Environment.NewLine +
                "        second level" + Environment.NewLine +
                "    /1.1" + Environment.NewLine +
                "closing" + Environment.NewLine);
        }
    }
}