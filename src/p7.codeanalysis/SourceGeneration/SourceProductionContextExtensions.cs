using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.Text;

namespace P7.CodeAnalysis.SourceGeneration
{
    public static class SourceProductionContextExtensions
    {
        public static void AddSources(this SourceProductionContext context, IEnumerable<GeneratedSourceFile> sourceFiles)
        {
            foreach (GeneratedSourceFile sourceFile in sourceFiles)
            {
                context.AddSource(sourceFile);
            }
        }

        public static void AddSource(this SourceProductionContext context, GeneratedSourceFile sourceFile)
        {
            context.AddSource(sourceFile.HintName, sourceFile.Content);
        }

        public static void AddSources(this SourceProductionContext context, IEnumerable<KeyValuePair<String, SourceText>> sourceFileInfos)
        {
            foreach (KeyValuePair<String, SourceText> sourceFileInfo in sourceFileInfos)
            {
                context.AddSource(sourceFileInfo);
            }
        }

        public static void AddSource(this SourceProductionContext context, KeyValuePair<String, SourceText> sourceFileInfo)
        {
            context.AddSource(sourceFileInfo.Key, sourceFileInfo.Value);
        }

        public static void ReportDiagnostics(this SourceProductionContext context, IEnumerable<Diagnostic> diagnostics)
        {
            foreach (Diagnostic diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}