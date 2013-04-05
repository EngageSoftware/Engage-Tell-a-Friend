﻿// <copyright file="PureAttribute.cs" company="JetBrains s.r.o.">
// Copyright 2007-2012 JetBrains s.r.o.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace JetBrains.Annotations
{
    using System;

    /// <summary>Indicates that a method does not make any observable state changes.
    /// The same as <c>System.Diagnostics.Contracts.PureAttribute</c></summary>
    /// <example>
    ///   <code>
    /// [Pure]
    /// private int Multiply(int x, int y)
    /// {
    /// return x*y;
    /// }
    /// public void Foo()
    /// {
    /// const int a=2, b=2;
    /// Multiply(a, b); // Waring: Return value of pure method is not used
    /// }
    ///   </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class PureAttribute : Attribute
    {
    }
}