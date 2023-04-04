﻿// This source code is fork from https://github.com/dotnet/corefx/blob/master/src/System.ComponentModel.Annotations/src/System/ComponentModel/DataAnnotations/ValidationAttributeStore.cs
// The .NET Foundation licenses the original file of this forked file to you under the MIT license.
// See the LICENSE file for the original file of this forked file: https://github.com/dotnet/corefx/blob/master/LICENSE.TXT

using System.ComponentModel.DataAnnotations;

namespace Ark.SharedLib.Api.Validators.LocalizedDataAnnotationsValidator.Internals.ValidationAttributeStores;

internal class StoreItem
{
    internal StoreItem(IEnumerable<Attribute> attributes)
    {
        ValidationAttributes = attributes.OfType<ValidationAttribute>();
        DisplayAttribute = attributes.OfType<DisplayAttribute>().SingleOrDefault();
    }

    internal IEnumerable<ValidationAttribute> ValidationAttributes { get; }

    internal DisplayAttribute DisplayAttribute { get; }
}