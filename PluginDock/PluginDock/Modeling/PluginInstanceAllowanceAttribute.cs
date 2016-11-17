using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PluginDock.Modeling
{
    /// <summary>Specifies whether or not the plug-in allows multiple instance or one.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    [DebuggerDisplay("Instance allowance: {InstanceAllowance}")]
    [ExcludeFromCodeCoverage]
    public sealed class PluginInstanceAllowanceAttribute : Attribute
    {
        /// <summary>Gets or sets the allowance mode.</summary>
        public InstanceAllowance InstanceAllowance { get; }

        /// <summary>Initializes a new instance of the <see cref="PluginInstanceAllowanceAttribute"/> class.</summary>
        /// <param name="instanceAllowance">The instance allowance.</param>
        public PluginInstanceAllowanceAttribute(InstanceAllowance instanceAllowance)
        {
            InstanceAllowance = instanceAllowance;
        }
    }
}