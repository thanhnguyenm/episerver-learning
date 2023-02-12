using EPiServer.Forms.Core.PostSubmissionActor;
using EPiServer.Forms.Core.PostSubmissionActor.Internal;
using EPiServer.Forms.Helpers.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShop.web.Business.Forms
{
    public class ResetFormElements : PostSubmissionActorBase, ISyncOrderedSubmissionActor
    {
        public int Order => int.MaxValue;

        public override object Run(object input)
        {
            var submissionJson = Newtonsoft.Json.JsonConvert.SerializeObject(this.SubmissionData.Data);
            /// Actor which wants to cancel the submission process should return this object by set CancelSubmit to true.
            /// And set ErrorMessage to show the reason for cancellation on UI.
            var result = new SubmissionActorResult { CancelSubmit = false, ErrorMessage = string.Empty };


            var formContainerBlock = this.FormIdentity.GetFormBlock();
            var allFormsElements = formContainerBlock.Form.Steps.SelectMany(st => st.Elements);
            foreach(var ele in allFormsElements)
            {
                ele.Value = string.Empty;
            }

            return result;
        }

    }
}