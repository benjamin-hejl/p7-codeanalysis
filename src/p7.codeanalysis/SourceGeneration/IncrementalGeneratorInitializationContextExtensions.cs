using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Text;

namespace P7.CodeAnalysis.SourceGeneration
{
    public static class IncrementalGeneratorInitializationContextExtensions
    {
        public static void RegisterSourceOutput<T1, T2>(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<(T1, T2)> source, Action<SourceProductionContext, T1, T2> action)
        {
            context.RegisterSourceOutput(source, (ctx, tpl) => action(ctx, tpl.Item1, tpl.Item2));
        }

        public static void RegisterSourceOutput<T1, T2>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<(T1, T2)> source, Action<SourceProductionContext, T1, T2> action)
        {
            context.RegisterSourceOutput(source, (ctx, tpl) => action(ctx, tpl.Item1, tpl.Item2));
        }

        public static void RegisterSourceOutput<T1, T2, T3>(this IncrementalGeneratorInitializationContext context, IncrementalValueProvider<(T1, T2, T3)> source, Action<SourceProductionContext, T1, T2, T3> action)
        {
            context.RegisterSourceOutput(source, (ctx, tpl) => action(ctx, tpl.Item1, tpl.Item2, tpl.Item3));
        }

        public static void RegisterSourceOutput<T1, T2, T3>(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<(T1, T2, T3)> source, Action<SourceProductionContext, T1, T2, T3> action)
        {
            context.RegisterSourceOutput(source, (ctx, tpl) => action(ctx, tpl.Item1, tpl.Item2, tpl.Item3));
        }
    }
}
