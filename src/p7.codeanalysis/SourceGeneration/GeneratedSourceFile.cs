using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Text;

namespace P7.CodeAnalysis.SourceGeneration
{
    public readonly struct GeneratedSourceFile
    {
        public String HintName { get; }
        public SourceText Content { get; }
    }
}