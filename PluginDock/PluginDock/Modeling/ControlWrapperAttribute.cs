using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PluginDock.Modeling
{
    /// <summary>Specifies the control wrapper type for the plug-in.</summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    [DebuggerDisplay("Control wrapper: {ControlWrapper}")]
    [ExcludeFromCodeCoverage]
    public sealed class ControlWrapperAttribute : Attribute
    {
        /// <summary>Gets or sets the control wrapper.</summary>
        public ControlWrapper ControlWrapper { get; }

        /// <summary>Initializes a new instance of the <see cref="ControlWrapperAttribute"/> class.</summary>
        /// <param name="controlWrapper">The control wrapper.</param>
        public ControlWrapperAttribute(ControlWrapper controlWrapper)
        {
            ControlWrapper = controlWrapper;
        }
    }
}