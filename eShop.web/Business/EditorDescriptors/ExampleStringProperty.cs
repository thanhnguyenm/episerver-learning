using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using System;
using System.Collections.Generic;

namespace eShop.web.Business.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(String), UIHint = "ExampleString")]
    public class ExampleStringProperty : EditorDescriptor
    {
        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            ClientEditingClass = "nitecoui/editors/ExampleString";
            base.ModifyMetadata(metadata, attributes);
        }
    }
}