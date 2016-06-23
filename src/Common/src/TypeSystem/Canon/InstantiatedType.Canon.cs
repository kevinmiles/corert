﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Internal.TypeSystem
{
    // Implements canonicalization for generic instantiations
    partial class InstantiatedType
    {
        public override bool IsCanonicalSubtype(CanonicalFormKind policy)
        {
            foreach (TypeDesc t in Instantiation)
            {
                if (t.IsCanonicalSubtype(policy))
                {
                    return true;
                }
            }

            return false;
        }

        protected override TypeDesc ConvertToCanonFormImpl(CanonicalFormKind kind)
        {
            bool needsChange;
            Instantiation canonInstantiation = CanonUtilites.ConvertInstantiationToCanonForm(Context, Instantiation, kind, out needsChange);
            if (needsChange)
            {
                MetadataType openType = (MetadataType)GetTypeDefinition();
                return Context.GetInstantiatedType(openType, canonInstantiation);
            }

            return this;
        }
    }
}